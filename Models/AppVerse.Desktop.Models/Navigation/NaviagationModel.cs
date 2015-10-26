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


    
  public class NavigationItem 
    {

        public string ModuleName { get; set; }

        public string ModuleDescription { get; set; }

        public BaseViewModel ModuleShell { get; set; }


    }
}
