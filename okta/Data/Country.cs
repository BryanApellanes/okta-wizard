using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Wpf.Data
{
    public class Country
    {
        public Country(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
