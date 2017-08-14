using DiceBagApp.Services;
using DiceBagApp.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace DiceBagApp
{
    class ViewModelLocator
    {
        private UnityContainer _unityContainer;

        public BaseViewModel BaseViewModel
        {
            get
            {
                return _unityContainer.Resolve<BaseViewModel>();
            }
        }


        public ViewModelLocator(){
            _unityContainer = new UnityContainer();

            _unityContainer.RegisterType<IDiceService, DiceService>();

            _unityContainer.RegisterType<BaseViewModel>(new ContainerControlledLifetimeManager());

            UnityServiceLocator unityServiceLocator = new UnityServiceLocator(_unityContainer);

            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }
    }
}
