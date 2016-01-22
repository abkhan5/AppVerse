using AppVerse.Desktop.AppCommon.BaseClasses;
using AppVerse.Desktop.Models.Navigation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Desktop.NavigationModule.ViewModels
{
  public  class NavigationShellViewModel: BaseViewModel
    {


        #region Private member

        private NavigationList _navigationItem;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unityContainer"></param>
        public NavigationShellViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            Title = "Available Modules";
        }

        #endregion


        protected override void Initialize()
        {
            NavigationItems = new ObservableCollection<NavigationList>();
        }
        public ObservableCollection<NavigationList> NavigationItems { get; set; }

        public NavigationList NavigationItem
        {
            get
            {
                return _navigationItem;
            }
            set
            {
                SetProperty(ref _navigationItem, value);
            }
        }


        public string Title { get; set; }


    }
}
