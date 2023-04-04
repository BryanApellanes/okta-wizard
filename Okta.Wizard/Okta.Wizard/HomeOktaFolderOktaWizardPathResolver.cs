using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class HomeOktaFolderOktaWizardPathResolver : OktaFolderOktaWizardPathResolver
    {
        public HomeOktaFolderOktaWizardPathResolver() : base(HomePath.Resolve("~"))
        { }
    }
}
