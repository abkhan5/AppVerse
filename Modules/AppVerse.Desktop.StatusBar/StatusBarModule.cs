#region Namespace
using Appverse.Desktop.Common;
using Appverse.Desktop.VisualControls;
using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

#endregion

namespace AppVerse.Desktop.StatusBar
{
   public class StatusBarModule : BaseModule
    {
        List<string> _resources = new List<string>
                                          {
                                              "pack://application:,,,/AppVerse.Desktop.StatusBar;component/MappingDictionary.xaml",
                                            };

        public StatusBarModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {

        }



        public override void RegisterResources()
        {
            Extensions.RegisterResources(_resources);

            _regionManager.Regions[RegionNames.MainRegion].Add(_unityContainer.Resolve<ViewModels.StatusBarViewModel>(), ModuleNames.StatusBar);
        }
    }
}
