using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public abstract class LogEventWriter
    {
        public const string LOG_DIRECTORY = "~/.okta/wizard/logs";

        public LogEventWriter(ILogger logger = null)
        {
            Logger = logger ?? new FileLogger(this.GetType().FullName, HomePath.Resolve(LOG_DIRECTORY));
        }

        protected ILogger Logger { get; set; }

        protected Task InfoAsync(string message)
        {
            return Task.Run(() => Logger.Info(message));
        }

        protected Task WarnAsync(string message)
        {
            return Task.Run(() => Logger.Warn(message));
        }

        protected Task ErrorAsync(string message, Exception ex)
        {
            return Task.Run(() => Logger.Error(message, ex));
        }

        protected Task FatalAsync(string message, Exception ex)
        {
            return Task.Run(() => Logger.Fatal(message, ex));
        }


        protected void Info(string message)
        {
            Task.Run(() => Logger.Info(message));
        }

        protected void Warn(string message)
        {
            Task.Run(() => Logger.Warn(message));
        }

        protected void Error(string message, Exception ex)
        {
            Task.Run(() => Logger.Error(message, ex));
        }

        protected void Fatal(string message, Exception ex)
        {
            Task.Run(() => Logger.Fatal(message, ex));
        }
    }
}
