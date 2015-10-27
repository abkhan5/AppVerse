using Microsoft.Practices.Prism.Mvvm;

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Cell : BindableBase
    {
        #region Private members
        int _lastRow, _lastColumn;
        private LifeState _cellState;

        #endregion
        #region Constructor

        public Cell(int row, int columns, int lastRow,int lastColumn)
        {
            CellCordinate = new Coordinates(row, columns);
            _lastColumn = lastColumn;
            _lastRow = lastRow;
            Neighbour = new NeighbouringCoordinates();
            SetupNeighbours();
        }
        #endregion


        #region Methods

        private void SetupNeighbours()
        {
            AddToNeighbour(CellCordinate.Row - 1, CellCordinate.Column - 1);
            AddToNeighbour(CellCordinate.Row - 1, CellCordinate.Column );
            AddToNeighbour(CellCordinate.Row - 1, CellCordinate.Column + 1);
            AddToNeighbour(CellCordinate.Row , CellCordinate.Column - 1);
            AddToNeighbour(CellCordinate.Row , CellCordinate.Column + 1);
            AddToNeighbour(CellCordinate.Row + 1, CellCordinate.Column - 1);
            AddToNeighbour(CellCordinate.Row + 1, CellCordinate.Column);
            AddToNeighbour(CellCordinate.Row + 1, CellCordinate.Column + 1);

        }
        private void AddToNeighbour(int row,int column)
        {
            if (row<0||column<0)
            {
                return;
            }
            if (row>_lastRow||column>_lastColumn)
            {
                return;
            }
            Neighbour.Add(row, column);
        }


        #endregion

     

        #region Properties

        public NeighbouringCoordinates Neighbour { get;  }



        public LifeState State
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

        public Coordinates CellCordinate { get;  }

        public void InvertLifeState()
        {
            switch (State)
            {
                case LifeState.Alive:
                    State=LifeState.Dead;
                    break;

                case LifeState.Dead:
                    State = LifeState.Alive;

                    break;

            }
        }
        

        
        #endregion
    }
}
