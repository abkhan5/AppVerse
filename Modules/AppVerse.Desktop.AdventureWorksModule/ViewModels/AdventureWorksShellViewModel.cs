#region Namespace
using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;
#endregion

namespace AppVerse.Desktop.AdventureWorksModule.ViewModels
{
   public  class AdventureWorksShellViewModel : BaseViewModel
    {

        #region Private members

        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public AdventureWorksShellViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {

        }
        #endregion



        #region Methods

        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
        }


        #endregion
    }
}
