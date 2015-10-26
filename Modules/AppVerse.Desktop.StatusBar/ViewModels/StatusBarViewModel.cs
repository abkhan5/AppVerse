using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Desktop.StatusBar.ViewModels
{
  public   class StatusBarViewModel : BaseViewModel
    {
        #region Private members




        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public StatusBarViewModel(IUnityContainer unityContainer):base(unityContainer)
        {
        }

        protected override void Initialize()
        {

        }
        #endregion


    }
}
