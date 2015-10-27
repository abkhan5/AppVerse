using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
