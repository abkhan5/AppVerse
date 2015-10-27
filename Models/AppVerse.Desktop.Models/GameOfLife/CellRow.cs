#region Namespace
using System.Collections.ObjectModel;
using System.Linq;
#endregion

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class CellRow :ObservableCollection<Cell>
    {
        public Cell this[int row,int column]
        {
            get
            {
                var cell = this.FirstOrDefault(cellRowItem =>
                  cellRowItem.CellCordinate.Row == row &&
                  cellRowItem.CellCordinate.Column == column);
                return cell;
            }
        }
    }
}
