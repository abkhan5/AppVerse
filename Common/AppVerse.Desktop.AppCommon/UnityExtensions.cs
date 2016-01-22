using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Desktop.AppCommon
{
   public static class UnityExtensions
    {

        public static void LoadUnityConfiguration(this IUnityContainer  container)
        {
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(container);

        }
    }
}
