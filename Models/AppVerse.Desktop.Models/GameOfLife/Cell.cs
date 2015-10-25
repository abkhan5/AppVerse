using Microsoft.Practices.Prism.Mvvm;

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Cell : BindableBase
    {
        #region Private members

        private CellState _cellState;

        #endregion


        #region Properties

        public CellState State
        {
            get
            {

                return _cellState;
            }
            set
            {
                SetProperty(ref this._cellState, value);


            }
        }

        public Coordinates CellCordinate { get; set; }

        #endregion
    }
}
