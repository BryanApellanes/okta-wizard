using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class FileLogger : ILogger
    {
        private const int MaxFileSize = 256000;

        FileInfo _currentLogFile;

        public FileLogger() : this(DefaultLogName, HomePath.Resolve(OktaWizardSettings.WIZARD_LOG_FILE_PATH))
        {
        }

        public FileLogger(Type type) : this(type.Name, HomePath.Resolve(OktaWizardSettings.WIZARD_LOG_FILE_PATH))
        { 
        }

        public FileLogger(string logName, string directoryPath)
        {
            this.LogName = logName;
            this.DirectoryPath = directoryPath;
            this._currentLogFile = GetNextFile();      
        }

        public static string DefaultLogName { get => Assembly.GetExecutingAssembly().GetName().Name; }

        public string LogName { get; set; }
        public string DirectoryPath { get; set; }

        public static FileLogger Create(string logDir = ".")
        {
            if (HomePath.TryGetHomePath(out string homeDir))
            {
                logDir = homeDir;
            }
            return new FileLogger(typeof(OktaWizard).Name, logDir);
        }

        public void Error(string message, Exception ex)
        {
            Write($"ERROR::{DateTime.Now}::{LogName}::{message} ({ex.Message})\r\t{ex.StackTrace}");
        }

        public void Fatal(string message, Exception ex)
        {
            Write($"FATAL::{DateTime.Now}::{LogName}::{message} ({ex.Message})\r\t{ex.StackTrace}");
        }

        public void Info(string message)
        {
            Write($"INFO::{DateTime.Now}::{LogName}::{message}");
        }

        public void Warn(string message)
        {
            Write($"WARN::{DateTime.Now}::{LogName}::{message}");
        }

        object _writeLock = new object();
        protected void Write(string message)
        {
            Task.Run(() =>
            {
                lock (_writeLock)
                {
                    StreamWriter writer = null;
                    if(!_currentLogFile.Directory.Exists)
                    {
                        _currentLogFile.Directory.Create();
                    }
                    if(!_currentLogFile.Exists)
                    {
                        writer = _currentLogFile.CreateText();
                    }
                    else
                    {
                        writer = _currentLogFile.AppendText();
                    }
                    using (writer)
                    {
                        writer.WriteLine(message);
                    }
                    _currentLogFile.Refresh();
                    if (_currentLogFile.Length >= MaxFileSize)
                    {
                        _currentLogFile = GetNextFile();
                    }
                }
            });            
        }

        int _num = 0;
        private FileInfo GetNextFile()
        {
            FileInfo result = new FileInfo(Path.Combine(DirectoryPath, $"{LogName}.log"));
            while (result.Exists)
            {
                _num++;
                result = new FileInfo(Path.Combine(DirectoryPath, $"{LogName}_{_num}.log"));
            }
            
            return result;
        }
    }
}
