using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaVisualStudioWizard
{
    internal static class StringExtensions
    {

        private static void GetExeAndArguments(string command, out string exe, out string arguments)
        {
            exe = command;
            arguments = string.Empty;
            string[] split = command.Split(new string[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
            {
                exe = split[0];
                arguments = split[1];
            }
        }

        public static ProcessOutput Run(this string command, int timeout = 600000)
        {
            GetExeAndArguments(command, out string exe, out string arguments);
            ProcessStartInfo startInfo = ProcessStartInfoExtensions.CreateStartInfo(false);
            startInfo.FileName = command;
            startInfo.Arguments = arguments;
            return startInfo.Run(new ProcessOutputCollector(), timeout);
        }
    }
}
