#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace AppVerse.Desktop.Models.Navigation
{
    public  class NavigationList :List<NavigationItem>
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
