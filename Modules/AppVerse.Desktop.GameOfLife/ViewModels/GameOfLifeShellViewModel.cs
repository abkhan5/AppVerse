#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;
#endregion

namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class GameOfLifeShellViewModel : BaseViewModel
    {

        #region Private members

        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public GameOfLifeShellViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }
        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            GameOfLifeInformationView = _unityContainer.Resolve<GameOfLifeInformationViewModel>();
            GameView = _unityContainer.Resolve<GameDetailsViewModel>();
        }

        #endregion
        #region Properties
        public BaseViewModel GameOfLifeInformationView { get; set; }

        public BaseViewModel GameView { get; set; }

        #endregion
    }
}
