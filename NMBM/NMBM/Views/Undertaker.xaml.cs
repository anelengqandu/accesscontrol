using NMBM.Models;
using NMBM.Services.Burials;
using NMBM.ViewModels;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms.SQLite.SQLite;

namespace NMBM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Undertaker : ContentPage
    {
        UndertakerViewModel _undertakerViewModel;
        private readonly IBurialService _apiBurial;
        private readonly SQLiteConnection _sqLiteConnection;

        public Undertaker(BurialModelOutPut burialOutPut, IBurialService apiBurial)
        {

            InitializeComponent();
            _apiBurial = apiBurial;
            BindingContext = _undertakerViewModel = new UndertakerViewModel(burialOutPut, _apiBurial);
        

            lblUndertakerName.Text = burialOutPut.Undertaker;
            lblAccessCounter.Text = burialOutPut.AccessCount.ToString();
            lblDeceasedDetails.Text = burialOutPut.DeceasedDetails;
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
        }

        protected override bool OnBackButtonPressed()
        {

            return true;
        }
        protected override void OnDisappearing()
        {

        }
    }
}