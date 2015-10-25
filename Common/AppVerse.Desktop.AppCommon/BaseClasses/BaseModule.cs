using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace AppVerse.Desktop.AppCommon.BaseClasses
{
    public abstract class BaseModule : IModule
    {
        protected const string ApplicationPath = "pack://application:,,,/";

        List<string> _resources = new List<string>
                                          {
                                              "pack://application:,,,/AppVerse.Desktop.GameOfLife;component/MappingDictionary.xaml",
                                            };

        public void Initialize()
        {
            RegisterResources();
        }

        protected readonly IRegionManager _regionManager;
       protected readonly IUnityContainer _unityContainer;

     
        public BaseModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }
        public abstract void RegisterResources();
    }
}
