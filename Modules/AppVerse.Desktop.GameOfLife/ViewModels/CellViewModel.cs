#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.Models.GameOfLife;
using Microsoft.Practices.Unity;

#endregion

namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class CellViewModel : BaseViewModel
    {

        #region Private members
        private Cell _cellModel;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public CellViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }
        #endregion


        public void SetupCells(int rowNumber, int columnNumber)
        {
          //  CellModel.CellCordinate = new Coordinates() { Column = columnNumber, Row = rowNumber };

        }

        protected override void Initialize()
        {
            var cell = this._unityContainer.Resolve<Cell>();
            CellModel = cell;
        }

        public Cell CellModel
        {
            get { return _cellModel; }
            set
            {
                _cellModel = value;
            }
        }
    }
}
