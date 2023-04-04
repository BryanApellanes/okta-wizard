using Okta.Wizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaVisualStudioWizard.Tests
{
    public class TestProjectData : ProjectData
    {
        public OktaApplicationType OktaApplicationType{ get; set; }

        public override OktaApplicationType GetOktaApplicationType()
        {
            return OktaApplicationType;
        }
    }
}
