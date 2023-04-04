// <copyright file="ApplicationListControl.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Linq;
using System.Windows.Forms;
using Okta.Wizard;

namespace Okta.VisualStudio.Wizard.Controls
{
    public partial class ApplicationListControl : UserControl
    {
        public ApplicationListControl()
        {
            InitializeComponent();
        }

        public string ApplicationName
        {
            get
            {
                if (ApplicationNameComboBox.SelectedItem != null)
                {
                    return ApplicationNameComboBox.SelectedItem.ToString();
                }

                return ApplicationNameComboBox.Text;
            }

            set
            {
                ClientApplication[] applications = new ClientApplication[ApplicationNameComboBox.Items.Count];
                ApplicationNameComboBox.Items.CopyTo(applications, 0);
                ClientApplication selected = applications.Where(item => item.Name.Equals(value)).FirstOrDefault();
                if (selected != null)
                {
                    ApplicationNameComboBox.SelectedItem = selected;
                }
            }
        }

        public Exception Exception
        {
            get;set;
        }

        public bool ExceptionOccurred
        {
            get
            {
                return Exception != null;
            }
        }

        public void SetApplications(ClientApplication[] applications)
        {
            ClientApplications = applications;
            ApplicationNameComboBox.Items.AddRange(ClientApplications);
        }

        protected ClientApplication[] ClientApplications
        {
            get;
            set;
        }
    }
}
