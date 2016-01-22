#region Namespace
using Appverse.Desktop.Common;
using Appverse.Desktop.VisualControls;
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.NavigationModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
#endregion

namespace AppVerse.Desktop.NavigationModule
{
    public   class NavigationModule : BaseModule
    {


        List<string> _resources = new List<string>
                                          {
                                              "pack://application:,,,/AppVerse.Desktop.NavigationModule;component/MappingDictionary.xaml",
                                            };

        public NavigationModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {

        }



        public override void RegisterResources()
        {
            Extensions.RegisterResources(_resources);

            _regionManager.Regions[RegionNames.NavigationRegion].Add(_unityContainer.Resolve<NavigationShellViewModel>(), ModuleNames.NavigationBar);

        }
    }
}
