using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.ApplicationEvents.GameOfLife;
using Microsoft.Practices.Unity;

namespace AppVerse.Desktop.LodgeModule.ViewModels
{
  public   class LodgeViewModel : BaseViewModel
    {
        #region Private members
        #endregion

        #region Constant
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public LodgeViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }
        #endregion

        #region Methods

        protected override void Initialize()
        {
            //AppEventAggregator.GetEvent<GameCompleteEvent>().Subscribe(GameStopEventHandler);
        }
        
        public bool IsGameBoardVisisble
        {
            get
            {
                return true;
            }
            set
            {
              //  SetProperty(ref _isGameBoardVisisble, value);
            }
        }
        


        #endregion

    }
}
