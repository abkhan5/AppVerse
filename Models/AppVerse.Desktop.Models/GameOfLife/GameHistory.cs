#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using System.Collections.ObjectModel;

#endregion

namespace AppVerse.Desktop.Models.GameOfLife
{
   public  class GameHistory : DataModelBase
    {
        public Board GameBoard { get; set; }

        public int TotalRows { get; set; }

        public int ToatlColumns { get; set; }


        public int TotalGenerations { get; set; }
    }
}
