using Microsoft.Practices.ServiceLocation;

namespace NMBM.ViewModels.ViewModelLocator
{
   public class ViewModelLocator
    {
        public LoginViewModel LoginViewModel
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public BurialsViewModel BurialsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BurialsViewModel>(); }
        }

        public AboutViewModel AboutViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AboutViewModel>(); }
        }

        public UndertakerViewModel UndertakerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<UndertakerViewModel>(); }
        }


    }
}
