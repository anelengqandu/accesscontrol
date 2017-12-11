using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Connectivity.Plugin;
using NMBM.Entities;
using NMBM.Models;
using NMBM.Services.Burials;
using NMBM.Views;
using SQLite;
using Xamarin.Forms;
using XamarinForms.SQLite.SQLite;

namespace NMBM.ViewModels
{
   public class UndertakerViewModel : INotifyPropertyChanged
    {
        private readonly IBurialService _apiBurial;
        private BurialModelOutPut _burialOutPut;
        private SupervisorModel _supervisor;
        private long _burialApplication;
        private readonly SQLiteConnection _sqLiteConnection;
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

        private bool _startEnabled = true;
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
        public UndertakerViewModel( BurialModelOutPut burialOutPut, IBurialService apiBurial)
        {
            _burialOutPut = burialOutPut;
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
            _sqLiteConnection.CreateTable<Burial>();
            if (_burialOutPut.BurialApplication!=0)
            {
                _burialApplication = _burialOutPut.BurialApplication;
                _supervisor = new SupervisorModel
                {
                    CemeteryId = burialOutPut.CemeteryId,
                    SupervisorId = burialOutPut.SupervisorId,
                    Name= burialOutPut.Name,
                    Surname = burialOutPut.Surname
                   
                };

            }
            
            _apiBurial = apiBurial;
        }

        public ICommand ContinueCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        await UpdateBurialCounter(_burialApplication);
                        await Application.Current.MainPage.Navigation.PushAsync(new Burials(_supervisor, _apiBurial));
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage.DisplayAlert("Error Network Connection",
                                "You need to be connected to the internet and restart the application",
                                "OK");
                        });
                    }
                });
            }
        }
        private async Task UpdateBurialCounter(long burialApplication)
        {
            await Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;
                    StartEnabled = false;
                    var results =await  _apiBurial.UpdateBurialCounterAsync(burialApplication);
                    IsBusy = false;
                    StartEnabled = true;

                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("Error Message",
                  ex.Message,
                   "OK");
                        IsBusy = false;
                        StartEnabled = true;
                    });
                }
            });

        }

        public ICommand LogOutCommand => new Command(LogOut);

        private static async void LogOut()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
               await Application.Current.MainPage.DisplayAlert("Error on logout",
                  ex.Message,
                   "OK");
            }
        }
        public ICommand ConfirmCommand
        {
            get
            {
                return new Command(async () =>
                {
                  
                    var answer =  await Application.Current.MainPage.DisplayAlert("Confirmation", "Are you sure you want to confirm the submission?", "Yes", "No");
                    if (answer)
                    {
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            await UpdateBurialCounter(_burialApplication);
                            await BurialConfirmation(_burialApplication);
                            await Application.Current.MainPage.Navigation.PushAsync(new Burials(_supervisor, _apiBurial));
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Application.Current.MainPage.DisplayAlert("Error Network Connection",
                                    "You need to be connected to the internet and restart the application",
                                    "OK");
                            });
                        }
                       
                    }
                   

                });
            }
        }


        private async Task BurialConfirmation(long burialApplication)
        {
            await Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;
                    StartEnabled = false;
                    var results = await _apiBurial.BurialConfirmationAsync(burialApplication);
                    await UpdateBurialDataLocaly(burialApplication);
                    IsBusy = false;
                    StartEnabled = true;
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("Error Message",
                  ex.Message,
                   "OK");
                        IsBusy = false;
                        StartEnabled = true;
                    });
                }
            });

        }


        private async Task UpdateBurialDataLocaly(long burialApplication)
        {
            await Task.Run(() =>
            {
                try
                {
                    StartEnabled = false;
                    var optDate = DateTime.Now;  //"2007-01-01 10:00:00"; 
                    _sqLiteConnection.ExecuteScalar<Burial>("UPDATE Burial Set OptDate  = ? WHERE BurialApplication = ?", optDate, burialApplication);
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StartEnabled = true;
                        Application.Current.MainPage.DisplayAlert("Error Message",
                  ex.Message,
                   "OK");
                    });
                }
            });
        }

        public ICommand AboutCommand => new Command(About);

        private static async void About()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
            }
            catch (Exception ex)
            {
               await Application.Current.MainPage.DisplayAlert("Error Message",
                  ex.Message,
                   "OK");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
