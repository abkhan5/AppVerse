using System.Collections.Generic;

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class NeighbouringCoordinates : List<Coordinates>
    {

        public void Add(int row, int column)
        {
            Add(new Coordinates(row, column));
        }

    }
}
