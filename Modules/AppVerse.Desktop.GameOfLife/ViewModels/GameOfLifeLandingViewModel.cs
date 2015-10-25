#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;

#endregion
namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class GameOfLifeLandingViewModel : BaseViewModel
    {

        #region Private memebers

        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public GameOfLifeLandingViewModel(IUnityContainer unityContainer):base(unityContainer)
        {

        }
        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            Title = "Conway's Game of Life";
            OverView = "The game is a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input. One interacts with the Game of Life by creating an initial configuration and observing how it evolves or, for advanced players, by creating patterns with particular properties.";
            GameView = _unityContainer.Resolve<GameViewModel>();
        }

        #endregion
        #region Properties

        public string Title { get; set; }


        public string OverView { get; set; }


        public BaseViewModel GameView { get; set; }
        #endregion
    }
}
