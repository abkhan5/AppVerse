using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Cell : BindableBase
    {
        #region Private members
        int _lastRow, _lastColumn;
        private LifeState _cellState;

        #endregion
        #region Constructor

        public Cell(int row, int columns, int lastRow, int lastColumn)
        {
            CellCordinate = new Coordinates(row, columns);
            _lastColumn = lastColumn;
            _lastRow = lastRow;
            Neighbours = new NeighbouringCoordinates();
            SetupNeighbours();
            _cellState = LifeState.Dead;
            CalculatedState = LifeState.Dead;
        }
        #endregion


        #region Methods

        private void SetupNeighbours()
        {
            AddToNeighbour(CellCordinate.Row - 1, CellCordinate.Column - 1);
            AddToNeighbour(CellCordinate.Row - 1, CellCordinate.Column);
            AddToNeighbour(CellCordinate.Row - 1, CellCordinate.Column + 1);
            AddToNeighbour(CellCordinate.Row, CellCordinate.Column - 1);
            AddToNeighbour(CellCordinate.Row, CellCordinate.Column + 1);
            AddToNeighbour(CellCordinate.Row + 1, CellCordinate.Column - 1);
            AddToNeighbour(CellCordinate.Row + 1, CellCordinate.Column);
            AddToNeighbour(CellCordinate.Row + 1, CellCordinate.Column + 1);

        }
        private void AddToNeighbour(int row, int column)
        {
            if (row < 0 || column < 0)
            {
                return;
            }
            if (row > _lastRow || column > _lastColumn)
            {
                return;
            }
            Neighbours.Add(row, column);
        }


        #endregion



        #region Properties

        public NeighbouringCoordinates Neighbours { get; }



        public LifeState State
        {
            get
            {

                return _cellState;
            }
            set
            {
                SetProperty(ref this._cellState, value);
                CalculatedState = _cellState;
            }
        }

        public LifeState CalculatedState { get; set; }

        public Coordinates CellCordinate { get; }

        public void InvertLifeState()
        {
            switch (State)
            {
                case LifeState.Alive:
                    State = LifeState.Dead;
                    break;

                case LifeState.Dead:
                    State = LifeState.Alive;

                    break;

            }
        }

        public void SetupNeighbours(IEnumerable<Cell> cells)
        {
            var neighbouringCells = new List<Cell>();
            foreach (var neighbour in Neighbours)
            {
                var neighbouringCell = cells.FirstOrDefault(cellItem => neighbour == cellItem);
                if (neighbouringCell==null)
                {
                    continue;
                }
                neighbouringCells.Add(neighbouringCell);
            }
            NeighbouringCells = neighbouringCells;
        }


        public List<Cell> NeighbouringCells { get; private set; }
        #endregion
    }
}
