using Appverse.Desktop.VisualControls;
using System.Windows;

namespace AppVerse.Desktop.ApplicationShell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationConfiguration.ReadMahMetroApps();
            base.OnStartup(e);
            AppverseBootStrapper bootstrapper = new AppverseBootStrapper();
            bootstrapper.Run();
        }

        

    }
}
