#region Namespace
using Appverse.Desktop.Common;
using Appverse.Desktop.VisualControls;
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.GameOfLife.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

#endregion
namespace AppVerse.Desktop.GameOfLife
{
    public class GameOfLifeModule : BaseModule
    {
    
        
        List<string> _resources = new List<string>
                                          {
                                              "pack://application:,,,/AppVerse.Desktop.GameOfLife;component/MappingDictionary.xaml",
                                            };

        public GameOfLifeModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {

        }

       

        public override void RegisterResources()
        {
            Extensions.RegisterResources(_resources);

            // _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, () => _unityContainer.Resolve<GameOfLifeLandingViewModel>());

            _regionManager.Regions[RegionNames.MainRegion].Add(_unityContainer.Resolve<GameOfLifeLandingViewModel>(), ModuleNames.GameOLife);
        }
    }
}
