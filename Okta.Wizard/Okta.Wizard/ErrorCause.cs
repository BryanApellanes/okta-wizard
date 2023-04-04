﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class ErrorCause
    {
        public string ErrorSummary { get; set; }
        public string Reason { get; set; }
        public string Location { get; set; }
        public string LocationType { get; set; }
        public string Domain { get; set; }
    }
}
