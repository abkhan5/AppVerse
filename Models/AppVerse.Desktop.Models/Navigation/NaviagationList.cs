#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion
namespace AppVerse.Desktop.Models.Navigation
{
   public  class NaviagationList :List<NavigationItem>
    {

        public BaseViewModel this[string moduleName]
        {
            get
            {
                var selectedNavigationItem = this.FirstOrDefault(navigationItem => navigationItem.ModuleName == moduleName);

                if (selectedNavigationItem == null)
                {
                    return null;

                }

                return selectedNavigationItem.ModuleShell;
            }
        }
    }
}
