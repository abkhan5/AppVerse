﻿using System.Collections.ObjectModel;

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Board
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
            SetupBoard();
            GenerationNumber = 0;
        }


        private void SetupBoard()
        {
            for (int r = 0; r < Rows; r++)
            {
                var cellRow = new CellRow();
                Cells.Add(cellRow);

                for (int c = 0; c < Columns; c++)
                {
                    var cell = new Cell();
                    cell.CellCordinate = new Coordinates(r, c);
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
