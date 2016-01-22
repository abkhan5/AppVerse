#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
#endregion

namespace AppVerse.Desktop.Models.Navigation
{
   public class NavigationItem 
    {

        public string ModuleName { get; set; }

        public string ModuleDescription { get; set; }

        public BaseViewModel ModuleShell { get; set; }


    }
}
