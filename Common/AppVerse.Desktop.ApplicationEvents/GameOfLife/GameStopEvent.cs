#region Namespace
using AppVerse.Desktop.Models.GameOfLife;
using Microsoft.Practices.Prism.PubSubEvents;

#endregion

namespace AppVerse.Desktop.ApplicationEvents.GameOfLife
{

    public class GameStopEvent : PubSubEvent<GameHistory>
    {
    }
}
