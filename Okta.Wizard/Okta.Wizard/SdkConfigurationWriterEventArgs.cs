using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class SdkConfigurationWriterEventArgs : EventArgs
    {
        public SdkConfigurationWriterEventArgs(ISdkConfigurationWriter sdkConfigurer, SdkConfigurationWriterStatus status, Exception exception = null)
        {
            this.SdkConfigurer = sdkConfigurer;
            this.Status = status;
            this.Exception = exception;
        }
        public Exception Exception { get; private set; }
        public ISdkConfigurationWriter SdkConfigurer { get; private set; }
        public SdkConfigurationWriterStatus Status { get; private set; }
    }
}
