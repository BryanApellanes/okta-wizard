using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    internal class HomePath
    {
        internal static string Resolve(params string[] pathSegments)
        {
            if (pathSegments.Length == 0)
            {
                return string.Empty;
            }

            if (!pathSegments[0].StartsWith("~"))
            {
                return Path.Combine(pathSegments);
            }

            if (!TryGetHomePath(out string homePath))
            {
                return string.Empty;
            }

            var newSegments =
                new string[] { pathSegments[0].Replace("~", homePath) }
                .Concat(pathSegments.Skip(1))
                .ToArray();

            return Path.Combine(newSegments);
        }

        internal static bool TryGetHomePath(out string homePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                homePath = Environment.GetEnvironmentVariable("USERPROFILE") ??
                           Path.Combine(Environment.GetEnvironmentVariable("HOMEDRIVE"), Environment.GetEnvironmentVariable("HOMEPATH"));
            }
            else
            {
                homePath = Environment.GetEnvironmentVariable("HOME");
            }

            return !string.IsNullOrEmpty(homePath);
        }
    }
}
