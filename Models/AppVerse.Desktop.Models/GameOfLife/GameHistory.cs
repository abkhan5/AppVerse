#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using System.Collections.Generic;

#endregion

namespace AppVerse.Desktop.Models.GameOfLife
{
    public  class GameHistory : DataModelBase
    {

        public GameHistory()
        {
            BoardHistory = new List<Board>();
        }
        public Board GameBoard { get; set; }


        public List<Board> BoardHistory{ get; set; }
        public int TotalRows { get; set; }

        public int ToatlColumns { get; set; }


        public int TotalGenerations { get; set; }
    }
}
