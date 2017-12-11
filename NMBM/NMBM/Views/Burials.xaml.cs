using NMBM.Models;
using NMBM.Services.Burials;
using NMBM.ViewModels;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NMBM.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Burials : ContentPage
    {
        private SupervisorModel _model;
        private readonly IBurialService _apiBurial;
        BurialsViewModel _burialViewModel;

        public Burials(SupervisorModel model, IBurialService apiBurial)
        {
            InitializeComponent();
            this._model = model;
            _apiBurial = apiBurial;
            lblFullname.Text = model.Fullname;
            BindingContext = _burialViewModel = new BurialsViewModel(model, apiBurial);
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
