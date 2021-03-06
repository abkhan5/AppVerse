﻿using AppVerse.Desktop.AppCommon.BaseClasses;
using Microsoft.Practices.Unity;

namespace AppVerse.Desktop.ApplicationShell
{
    public class ApplicationShellViewModel : BaseViewModel
    {

        public ApplicationShellViewModel(IUnityContainer unityContainer):base(unityContainer)
        {
        }
        protected override void Initialize()
        {

            Title = "App Verse";
            IconPath = "mazzen.ico";
        }

        public string Title { get; set; }
        public string IconPath { get; set; }

    }
}
