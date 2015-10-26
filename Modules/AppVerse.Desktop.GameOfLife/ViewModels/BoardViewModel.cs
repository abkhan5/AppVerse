#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;

#endregion
namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public   class BoardViewModel : BaseViewModel
    {
        #region Private members


     

        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public BoardViewModel(IUnityContainer unityContainer):base(unityContainer)
        {

        }

        protected override void Initialize()
        {
            Cells = new ObservableCollection<CellViewModel>();
        }

        internal void ConfigureBoard(int numberOfRows, int numberOfColumns, int numberOfGenerations)
        {
            Cells.Clear();
            SetupCells(numberOfRows, numberOfColumns);
        }

        private void SetupCells(int numberOfRows, int numberOfColumns)
        {
            for (int c = 0; c < numberOfColumns; c++)
            {
                for (int r = 0; r < numberOfRows; r++)
                {                    
                    var cellVm = this._unityContainer.Resolve<CellViewModel>();
                    cellVm.SetupCells(r, c);
                    Cells.Add(cellVm);
                }
            }
        }


        #endregion

        #region Properties
        public ObservableCollection<CellViewModel> Cells { get; set; }
        #endregion
    }
}
