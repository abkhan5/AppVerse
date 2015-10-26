#region Namespace
using Microsoft.Practices.Unity;

#endregion

namespace AppVerse.Desktop.AppCommon.BaseClasses
{
    public abstract class BaseShellViewModel : BaseViewModel
    {


        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public BaseShellViewModel(IUnityContainer unityContainer):base(unityContainer)
        {
            Initialize();
        }
        #endregion
        public abstract string ModuleName { get; set; }


    }
}
