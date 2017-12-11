using NMBM.Services.Authentication;
using NMBM.ViewModels;
using NMBM.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using NMBM.Services.Burials;
using NMBM.Services.Cemetery;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NMBM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var unityContainer = new UnityContainer();

            unityContainer.RegisterType<IAuthService, AuthService>();
            unityContainer.RegisterInstance(typeof(LoginViewModel));//optional

            unityContainer.RegisterType<ICemeteryService, CemeteryService>();
            unityContainer.RegisterType<IBurialService, BurialService>();
            unityContainer.RegisterInstance(typeof(BurialsViewModel));//optional



            var unityServiceLocator = new UnityServiceLocator(unityContainer);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
            MainPage = new NavigationPage(new Login());
        }

    }
}
