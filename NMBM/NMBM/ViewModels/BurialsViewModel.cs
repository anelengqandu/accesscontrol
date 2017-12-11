using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using ZXing.Mobile;


namespace NMBM.ViewModels
{
   public class BurialsViewModel: INotifyPropertyChanged
    {
        private readonly IBurialService _apiBurial;
        MobileBarcodeScanner _scanner;
        private BurialModelOutPut _burialOutPut;
        public List<BurialModel> BurialItems { get; set; }
        public SupervisorModel _supervisor { get; set; }
        private long SupervisorId;
        public Command LoadItemsCommand { get; set; }
        private readonly SQLiteConnection _sqLiteConnection;
        private bool _isBusy;
        private readonly bool _isConnected;
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

        bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        //Our list of objects!
        List<BurialModel> _burialList;
        public List<BurialModel> BurialList
        {
            get { return _burialList; }
            set
            {
                _burialList = value;
                OnPropertyChanged(nameof(BurialList));
            }
        }

        //Refresh command
        Command _refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return _refreshCommand;
            }
        }

        //scanning using imagebutton
        ICommand _onTapGestureRecognizerTapped;
        public ICommand OnTapGestureRecognizerTapped
        {
            get { return _onTapGestureRecognizerTapped; }
        }

        private void OnTapped(object s)
        {
            ScanBarcode(null);

        }

        //on row select scanning
        private void OutputAge(BurialModel person)
        {
            if (person != null)
            {
                ScanBarcode(person.BurialApplication);
            }
        }
        public ICommand OutputAgeCommand { get; private set; }

        public string SelectedItemText { get; private set; }

        public BurialsViewModel(SupervisorModel model, IBurialService apiBurial)
        {
            

            if (CrossConnectivity.Current.IsConnected)
            {
                _apiBurial = apiBurial;
            _supervisor = model;

            SupervisorId = model.SupervisorId;
            _burialList = new List<BurialModel>();
            _refreshCommand = new Command(async () => await RefreshList(model));
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
            _sqLiteConnection.CreateTable<Burial>();
            _onTapGestureRecognizerTapped = new Command(OnTapped);
            OutputAgeCommand = new Command<BurialModel>(OutputAge);
           
            Reload();
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Error Network Connection",
                        "You need to be connected to the internet and restart the application",
                        "OK");
                    IsBusy = false;
                });
            }
        }

        public void Reload()
        {
            Task.Run(async () =>
            {
                IsBusy = true;
                BurialList = await PopulateList(_supervisor);

                IsBusy = false;
            });
        }

        public ICommand LogOutCommand => new Command(LogOut);

        private  async void LogOut()
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

        public ICommand AboutCommand => new Command(About);

        private static async void About()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error on logout",
                          ex.Message,
                          "OK");
            }
        }

        
        private async void ScanBarcode(long? burialAppId)
        {
            _scanner = new MobileBarcodeScanner
            {
                TopText = "Hold the camera up to the barcode\nAbout 6 inches away",
                BottomText = "Wait for the barcode to automatically scan!",
                CancelButtonText = "Return",
                FlashButtonText = "Flash"
            };

            var opt = new MobileBarcodeScanningOptions { DelayBetweenContinuousScans = 3000 };

            opt.AutoRotate = true;
            _scanner.Torch(true);
            _scanner.AutoFocus();
            try
            {
                var results = await _scanner.Scan(opt);
                HandleScanResult(results, burialAppId);

                if (_burialOutPut != null)
                {
                    _burialOutPut.SupervisorId = SupervisorId;
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new Undertaker(_burialOutPut, _apiBurial) {Title = _burialOutPut.Cemetery + " Burials"});
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error on scanner Api",
                          ex.Message,
                          "OK");
            }
           

        }
       
        private void HandleScanResult(ZXing.Result result, long? burialAppId)
        {


            if (result != null && !string.IsNullOrEmpty(result.Text)) // Success
            {
                int number;
                var resultValue = result.Text.TrimStart('0');
                bool isNumber = int.TryParse(resultValue, out number);

                if (isNumber)
                {
                    if (long.Parse(resultValue) == burialAppId || burialAppId == null)
                    {
                        var scannedItem = long.Parse(resultValue);

                        var currentDateTime = DateTime.Now;
                        ////check if burial application expired
                        var expiredApplication = (from burial in _sqLiteConnection.Table<Burial>()
                                                  where burial.BurialApplication.Equals(scannedItem)
                                                  select burial).FirstOrDefault();

                        if (expiredApplication == null)
                        {
                            Application.Current.MainPage.DisplayAlert("Warning",
                                "Either the application expired or does not exist, Contact the municipality.",
                                "OK");
                        }
                        else
                        {
                            if (expiredApplication.OptDate == null) //proceed
                            {
                                var supervisorId = SupervisorId;

                                if (true) //todo: vaidate cemetery check ...
                                {
                                    var qBurials = from row in _sqLiteConnection.Table<Burial>()
                                                   where row.BurialApplication == scannedItem
                                                   select new BurialModelAccessCounterOutput()
                                                   {
                                                       AccessCounter = row.AccessCounter,
                                                       OtpDate = row.OptDate,
                                                       DeceasedDetails = row.DeceasedDetails
                                                   };
                                    var r = qBurials.FirstOrDefault();


                                    if (r == null)
                                    {

                                        Application.Current.MainPage.DisplayAlert("Error", "Error api connection", "OK");
                                      
                                    }
                                    else
                                    {

                                        var burialAppEdit = new BurialModelAccessCounterInput
                                        {
                                            BurialApplication = scannedItem,

                                            CemeterySupervisorId = (int?)supervisorId
                                        };


                                        var query = (from row in _sqLiteConnection.Table<Burial>()
                                                     where row.BurialApplication == burialAppEdit.BurialApplication
                                                     select row).FirstOrDefault();

                                        query.AccessCounter = query.AccessCounter + 1;
                                        _sqLiteConnection.ExecuteScalar<Burial>(
                                            "UPDATE Burial Set AccessCounter  = ? WHERE BurialApplication = ?",
                                            query.AccessCounter, query.BurialApplication);


                                        _burialOutPut = new BurialModelOutPut()
                                        {
                                            Undertaker = query.Undertaker,
                                            AccessCount = query.AccessCounter,
                                            BurialApplication = query.BurialApplication,
                                            CemeteryId = _supervisor.CemeteryId,
                                            DeceasedDetails = query.DeceasedDetails,
                                            SupervisorId = query.CemeterySupervisorId,
                                            Name = _supervisor.Name,
                                            Surname = _supervisor.Surname,
                                            SupervisorName = _supervisor.Fullname,
                                            Cemetery = query.Cemetery

                                        };
                                    }
                                }
                            }
                            else
                            {

                                if (expiredApplication.OptDate == null)
                                {
                       Application.Current.MainPage.DisplayAlert("Warning","Access Code " + expiredApplication.BurialApplication + " expired.","OK");
                                    return;
                                }
                                var otpDate = (DateTime)expiredApplication.OptDate;
                                Application.Current.MainPage.DisplayAlert("Warning",
                                        "Access Code " + expiredApplication.BurialApplication + " expired. " +
                                        expiredApplication.OptDate + " (" +
                                        (currentDateTime - otpDate).Days + " day ago)", "OK");
                            }
                        }
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Error", "access denied", "OK");
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Scanned code not allowed!", "OK");
                }

            }
            else // Canceled
            {
                Reload();
            }

        }

       

        private async Task<List<BurialModel>> PopulateList(SupervisorModel model)
        {
                Device.BeginInvokeOnMainThread(() => IsBusy = true);
                var items = await _apiBurial.GetBurialAsync(model.CemeteryId);
                if (items.objBurial.Count > 0)
                    await AddDataLocaly(items.objBurial.ToList());
                Device.BeginInvokeOnMainThread(() => IsBusy = false);
                return items.objBurial.ToList();
        }

        private async Task AddDataLocaly(List<BurialModel> burials)
        {
            await Task.Run(() =>
            {
                try
                {
                    _sqLiteConnection.ExecuteScalar<Burial>("DELETE FROM Burial");

                    foreach (var burial in burials)
                    {
                        _sqLiteConnection.Insert(new Burial()
                        {
                            BurialApplication = burial.BurialApplication,
                            Undertaker = burial.Undertaker,
                            AccessCounter = burial.AccessCount,
                            BurialDate = burial.BurialDate,
                            CemeteryId = _supervisor.CemeteryId,
                            Cemetery = burial.Cemetery,
                            OptDate = burial.OptDate,
                            DeceasedDetails = burial.DeceasedDetails
                        });
                    }

                    //var qCemetery = (from device in _sqLiteConnection.Table<Cemetery>()
                    //    where device.CemeteryId == _supervisor.CemeteryId
                    //                 select device).FirstOrDefault();
                    //if (qCemetery == null)
                    //{

                    //    _sqLiteConnection.Insert(new Cemetery()
                    //    {
                    //        Lattitude = cemeteryLocation.Latitude,
                    //        Longitude = cemeteryLocation.Longitude,
                    //        CemeteryId = cemetery.cemeteryId,
                    //    });
                    //}

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
            });
        }

        private async Task RefreshList(SupervisorModel model)
        {
          

            IsRefreshing = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                BurialList = await PopulateList(model);
                IsRefreshing = false;
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Error Network Connection",
                        "You need to be connected to the internet and restart the application",
                        "OK");
                    IsBusy = false;
                });

                IsRefreshing = false;
            }
          
                IsRefreshing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
