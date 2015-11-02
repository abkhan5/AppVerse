using Appverse.Desktop.VisualControls.Themes.Blue;
using Appverse.Desktop.VisualControls.Themes.Red;
using System.Collections.Generic;

namespace Appverse.Desktop.VisualControls
{
    public static class ApplicationConfiguration
    {

        internal const string FilePath = "pack://application:,,,/Appverse.Desktop.VisualControls;component/";
        internal static List<string> _mahMetroAppAssemplies = new List<string>()
        {

        "pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml",
        "pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml" ,
        "pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml" ,
         "pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml",
          "pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml" };


        private static IEnumerable<string> StyleSelector(AppThemes appThemes)
        {
            IEnumerable<string> styleSource;
            switch (appThemes)
            {
                case AppThemes.Red:
                    styleSource = RedTheme.GetFilePaths();
                    break;

                case AppThemes.Blue:
                default:
                    styleSource = BlueThemes.GetFilePaths();
                    break;
            }

            return styleSource;
        }

        public static void ReadMahMetroApps()
        {
            foreach (var item in _mahMetroAppAssemplies)
            {
                var source = item;
                Extensions.SetResource(source);

            }
        }

        public static void SetDefaultResources(AppThemes appThemes)
        {

            var styleSource = StyleSelector(appThemes);
            foreach (var item in styleSource)
            {
                var source =  item;
                Extensions.SetResource(source);

            }
        }


    }


}
