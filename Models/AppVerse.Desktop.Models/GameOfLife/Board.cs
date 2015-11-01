#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using System.Collections.ObjectModel;

#endregion
namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Board : DataModelBase
    {
        public Board()
        {
            Cells = new ObservableCollection<CellRow>();
            Clear();
        }

        public ObservableCollection<CellRow> Cells { get; }


        public int Rows { get; set; }
        public int Columns { get; set; }

        public int GenerationNumber { get; set; }


        public void ConfigureBoard(int numberOfRows, int numberOfColumns)
        {
            Clear();
            Rows = numberOfRows;
            Columns = numberOfColumns;
            Cells.Clear();
            GenerationNumber = 0;
            SetupBoard();
        }

        public void RelateCellNeighbours()
        {
            var cells = Cells.CellsInBoard();
            var cellNeighbour = Cells.CellsInBoard();

            foreach (var cellItem in cells)
            {
                cellItem.SetupNeighbours(cellNeighbour);
            }

        }


        private void SetupBoard()
        {
            for (int r = 0; r < Rows; r++)
            {
                var cellRow = new CellRow();
                Cells.Add(cellRow);

                for (int c = 0; c < Columns; c++)
                {
                    var cell = new Cell(r, c, Rows, Columns);
                    cellRow.Add(cell);
                }
            }
        }
        public void Clear()
        {

            Rows = 0;
            Columns = 0;
            Cells.Clear();
        }
    }
}
