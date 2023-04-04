using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Tests
{
    internal class EnvironmentLock
    {
        /// <summary>
        /// Used to ensure tests that modify environment variables don't conflict with one another.
        /// </summary>
        public static object Object { get; } = new object(); 
    }
}
