using AppVerse.Desktop.ApplicationConfguration;
using AppVerse.Desktop.GameOfLife;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;

namespace AppVerse.Desktop.ApplicationShell
{
    public  class AppverseBootStrapper : UnityBootstrapper
    {

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(GameOfLifeModule));
            moduleCatalog.AddModule(typeof(ApplicationConfigurationModule));

        }

        protected override DependencyObject CreateShell()
        {
            // Use the container to create an instance of the shell.
            AppverseShellView view = this.Container.TryResolve<AppverseShellView>();
            return view;
        }


        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    return new ConfigurationModuleCatalog();
        //}
        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
    }
}
