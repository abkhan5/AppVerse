#region Namespace
using Appverse.Desktop.VisualControls;
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.ApplicationConfguration.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
#endregion

namespace AppVerse.Desktop.ApplicationConfguration
{
    public class ApplicationConfigurationModule: BaseModule
    {

        List<string> _resources = new List<string>
                                          {
                                              "pack://application:,,,/AppVerse.Desktop.ApplicationConfguration;component/MappingDictionary.xaml",
                                            };

        public ApplicationConfigurationModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {

        }



        public override void RegisterResources()
        {
            Extensions.RegisterResources(_resources);

            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, () => _unityContainer.Resolve<ApplicationConfigurationViewModel>());

            ApplicationConfiguration.SetDefaultResources(AppThemes.Blue);


        }
    }
}
