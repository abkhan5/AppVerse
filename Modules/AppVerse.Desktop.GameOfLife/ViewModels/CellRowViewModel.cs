#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace AppVerse.Desktop.GameOfLife.ViewModels
{
  public  class CellRowViewModel : BaseViewModel
    {
        #region Private members




        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public CellRowViewModel(IUnityContainer unityContainer):base(unityContainer)
        {

        }

        protected override void Initialize()
        {
            CellsRows = new ObservableCollection<CellViewModel>();
        }
        
        #endregion

        #region Properties
        public ObservableCollection<CellViewModel> CellsRows { get; set; }
        #endregion
    }
}
