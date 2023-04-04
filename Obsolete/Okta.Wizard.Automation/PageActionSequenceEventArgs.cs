// <copyright file="PageActionSequenceEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;

namespace Okta.Wizard.Automation
{
    /// <summary>
    /// Represents arguments relevant to a PageActionSequence event.
    /// </summary>
    public class PageActionSequenceEventArgs : EventArgs
    {
        public PageActionSequenceEventArgs(PageActionSequence pageActionSequence)
        {
            PageActionSequence = pageActionSequence;
        }

        /// <summary>
        /// Gets the automation page.
        /// </summary>
        public IAutomationPage Page => PageActionSequence.Page;

        /// <summary>
        /// Gets or sets the page action sequence.
        /// </summary>
        public PageActionSequence PageActionSequence { get; set; }

        /// <summary>
        /// Gets or sets the page action result.
        /// </summary>
        public PageActionResult PageActionResult { get; set; }

        /// <summary>
        /// Gets or sets the page action.
        /// </summary>
        public PageAction PageAction { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        public List<PageActionResult> Results { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
