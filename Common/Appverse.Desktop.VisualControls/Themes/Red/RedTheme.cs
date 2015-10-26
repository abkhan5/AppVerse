using System.Collections.Generic;

namespace Appverse.Desktop.VisualControls.Themes.Red
{

    internal static class RedTheme
    {
        internal const string FolderPath = "Themes/Red/";

        private static List<string> DefaultStyles = new List<string>
        {
                    "TextBlockStyles.xaml",

        };

        public static IEnumerable<string> GetFilePaths()
        {
            foreach (var item in DefaultStyles)
            {
                var source = ApplicationConfiguration.FilePath + FolderPath + item;
                yield return source;
            }
        }

    }
}
