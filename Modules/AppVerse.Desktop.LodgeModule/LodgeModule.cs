using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appverse.Desktop.Common;
using Appverse.Desktop.VisualControls;
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.LodgeModels;
using AppVerse.Desktop.LodgeModels.Model;
using AppVerse.Desktop.LodgeModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace AppVerse.Desktop.LodgeModule
{
   public class LodgeModule : BaseModule
    {
       readonly List<string> _resources = new List<string>
                                          {
                                              "pack://application:,,,/AppVerse.Desktop.LodgeModule;component/MappingDictionary.xaml",
                                            };

        public LodgeModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {

        }



        public override void RegisterResources()
        {
            Extensions.RegisterResources(_resources);
            _regionManager.Regions[RegionNames.MainRegion].Add(_unityContainer.Resolve<LodgeViewModel>(), ModuleNames.Lodge);
            LodgeDataContext context = new LodgeDataContext();
            var res = context.Query<Resident>().ToList();

            _unityContainer.RegisterInstance(context);
        }
    }
}
