#region Namespace
using AppVerse.Desktop.Services.Interfaces.GameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion
namespace AppVerse.Desktop.Services.GameOfLife
{
    public class LifeRules
    {
        public IEnumerable<ILifeRule> GetRules()
        {
            yield return new LiveLifeRule();

            yield return new OverPopulationLifeRule();

            yield return new RebirthLifeRule();

            yield return new UnderPopulationLifeRule();
        }
    }
}
