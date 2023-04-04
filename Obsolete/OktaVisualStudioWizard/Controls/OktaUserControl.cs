using Okta.VisualStudio.Wizard.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Okta.Wizard;

namespace Okta.VisualStudio.Wizard.Controls
{
    /// <summary>
    /// Class used as a base class to other UserControls.  Contains methods for setting 
    /// control attributes in a thread safe way.
    /// </summary>
    public class OktaUserControl : BindableControl
    {
        public OktaUserControl() : base()
        {
            
        }

        protected OktaWizard OktaWizard { get; set; }
    }
}
