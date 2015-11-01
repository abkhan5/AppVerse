namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Coordinates
    {
        public Coordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public int Row { get; set; }
        public int Column { get; set; }

        public static bool operator ==(Coordinates coordinate, Cell cell)
        {
            return cell.CellCordinate.Row == coordinate.Row && cell.CellCordinate.Column == coordinate.Column;

        }


        public static bool operator !=(Coordinates coordinate, Cell cell)
        {
            return !(coordinate == cell);
        }

        public override string ToString()
        {
            return "[" + (Row + 1) + "," + (Column + 1) + "]";
        }
    }
}
