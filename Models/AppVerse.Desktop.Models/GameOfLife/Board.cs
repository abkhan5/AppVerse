using System.Collections.ObjectModel;

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Board
    {
        public Board()
        {
            Cells = new ObservableCollection<Cell>();
        }

        public ObservableCollection<Cell> Cells { get; }


        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
