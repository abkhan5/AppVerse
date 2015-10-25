using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;

namespace AppVerse.Desktop.GameOfLife.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        #region Private members

        private BoardViewModel _boardViewModel;
        private int _numberOfRows;

        private int _numberOfColumns;

        #endregion



        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public GameViewModel(IUnityContainer unityContainer):base(unityContainer)
        {

        }
        #endregion
        #region Methods

        protected override void Initialize()
        {
            BoardView = _unityContainer.Resolve<BoardViewModel>();
        }

        #endregion
        #region Properties

        public BoardViewModel BoardView
        {
            get { return _boardViewModel; }
            set
            {
                SetProperty(ref _boardViewModel, value);
            }
        }
            public int NumberOfRows
        {
            get
            {

                return _numberOfRows;
            }

            set
            {
                SetProperty(ref _numberOfRows, value);
            }
        }
        public int NumberOfColumns
        {
            get
            {

                return _numberOfColumns;
            }

            set
            {
                SetProperty(ref _numberOfColumns, value);
            }
        }

        #endregion

    }
}
