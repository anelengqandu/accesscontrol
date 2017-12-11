using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Connectivity.Plugin;
using NMBM.Models;
using NMBM.Services.Authentication;
using NMBM.Services.Burials;
using NMBM.Services.Cemetery;
using NMBM.Views;
using Xamarin.Forms;

namespace NMBM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public SigninModel Signin { get; set; }
        private readonly IAuthService _apiServices;
        private readonly ICemeteryService _apiCemetery ;
        private readonly IBurialService _apiBurial;
        private SupervisorModel _objSupervisor;

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        private bool _startEnabled=true;
        public bool StartEnabled
        {
            get { return _startEnabled; }
            set
            {
                if (_startEnabled == value)
                    return;

                _startEnabled = value;
                OnPropertyChanged("StartEnabled");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;

                _password = value;
                OnPropertyChanged("Password");
            }
        }


        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value)
                    return;

                _username = value;
                OnPropertyChanged("Username");
            }
        }


        private string _buttonText = "Sign-in";
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (_buttonText == value)
                    return;

                _buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }

        public LoginViewModel(IAuthService apiServices, ICemeteryService apiCemetery, IBurialService apiBurial)
        {
            _apiServices = apiServices;
            _apiCemetery = apiCemetery;
            _apiBurial = apiBurial;
        }

        public ICommand LoginCommand
        {
           
            get
            {
                return new Command(async () =>
                {
                    
                    Signin = new SigninModel
                    {
                        Username = Username,
                        Password = Password
                    };

                    if (string.IsNullOrEmpty(Username))
                    {
                       
                            await Application.Current.MainPage.DisplayAlert("Error login",
                                "Username field is required",
                                "OK");
                            IsBusy = false;
                            StartEnabled = true;
                            ButtonText = "Sign-in";
                        
                    }
                    else if (string.IsNullOrEmpty(Password))
                    {

                        await Application.Current.MainPage.DisplayAlert("Error login",
                                "Password field is required",
                                "OK");
                            IsBusy = false;
                            StartEnabled = true;
                            ButtonText = "Sign-in";
                    }
                    else
                    {
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            await LoginAsync(Signin);
                            if (_objSupervisor != null)
                                await GetCemeteryAsync(_objSupervisor);

                            await ClearProperties(Signin);
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Application.Current.MainPage.DisplayAlert("Error Network Connection",
                                    "You need to be connected to the internet and restart the application",
                                    "OK");
                                IsBusy = false;
                                StartEnabled = true;
                                ButtonText = "Sign-in";
                            });

                            try
                            {
                                await Application.Current.MainPage.Navigation.PopToRootAsync();
                            }
                            catch (Exception ex)
                            {
                                await Application.Current.MainPage.DisplayAlert("Error",
                                    ex.Message,
                                    "OK");
                            }
                        }
                      
                    }
                });
            }
        }


        private static async Task ClearProperties<T>(T instance)
        {
            await ClearProperties(typeof(T), instance);
        }

        private static async Task ClearProperties(Type classType, object instance)
        {
            foreach (var property in classType.GetRuntimeProperties())
            {
                object value = null;
                try
                {
                    value = property.GetValue(instance, null);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Exception Error",
                        ex.Message,
                        "OK");
                }
                if (value != null && property.PropertyType != typeof(String))
                    await ClearProperties(property.PropertyType, value);
                else if (value != null && (String)value != "")
                    property.SetValue(instance, null);
            }
        }

     

        private async Task LoginAsync(SigninModel model)
        {
             await Task.Run(async () =>
             {
                 try
                 {

                     StartEnabled = false;
                     ButtonText = "Signing-in...";
                     IsBusy = true;
                    
                        
                             var results = await _apiServices.LoginAsync(model.Username, model.Password);
                             if (results.status == "Success" && results.objSupervisor != null)
                             {
                             
                                 var objSupervisor = new SupervisorModel()
                                 {
                                     CemeteryId = results.objSupervisor.CemeteryId,
                                     Name = results.objSupervisor.Name,
                                     Surname = results.objSupervisor.Surname,
                                     SupervisorId = results.objSupervisor.SupervisorId,
                                     UserName = results.objSupervisor.UserName,
                                     SupervisorPassword = results.objSupervisor.SupervisorPassword
                                 };
                                 _objSupervisor = objSupervisor;
                             }
                             else
                             {
                                 Device.BeginInvokeOnMainThread(() =>
                                 {
                                     Application.Current.MainPage.DisplayAlert("Error login",
                                     "Username/Password invalid",
                                     "OK");
                                     IsBusy = false;
                                     StartEnabled = true;
                                     ButtonText = "Sign-in";
                                    
                                 });
                                 _objSupervisor = null;
                             }
                 }
                 catch (Exception ex)
                 {
                     Device.BeginInvokeOnMainThread(() =>
                     {
                      Application.Current.MainPage.DisplayAlert("Error Message",
                   ex.Message,
                    "OK");
                     });
                 }
                 finally
                 {
                     IsBusy = false;
                     StartEnabled = true;
                     ButtonText = "Sign-in";
                 }

             });
        }


        private async Task GetCemeteryAsync(SupervisorModel model)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                var cem = await _apiCemetery.GetCemeteryAsync(model.CemeteryId);
                if (cem.status == "Success")
                {
                    //will be used for cemetry location
                    var objDevice = new CemeteryGeolocationModel
                    {
                        CemeteryName = cem.objDevice.CemeteryLocation.CemeteryName,
                        Latitude = cem.objDevice.CemeteryLocation.Latitude,
                        Longitude = cem.objDevice.CemeteryLocation.Longitude,
                    };
                    Password = string.Empty;
                    Username = string.Empty;
                    await Application.Current.MainPage.Navigation.PushAsync(new Burials(model, _apiBurial));
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Error Network Connection",
                        "You need to be connected to the internet and restart the application if possible",
                        "OK");
                    IsBusy = false;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
