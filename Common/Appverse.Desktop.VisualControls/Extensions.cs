#region Namespace
using System;
using System.Collections.Generic;
using System.Windows;

#endregion
namespace Appverse.Desktop.VisualControls
{
    public static class Extensions
    {     
        public static void RegisterResources(IEnumerable<string> Source)
        {
            foreach (var source in Source)
            {
                SetResource(source);
            }
        }

        internal static void SetResource(string source)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri(string.Format(source));
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
