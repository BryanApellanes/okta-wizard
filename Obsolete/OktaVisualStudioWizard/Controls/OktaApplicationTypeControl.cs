using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okta.Wizard;
using System.Windows.Forms;

namespace Okta.VisualStudio.Wizard.Controls
{
    public partial class OktaApplicationTypeControl : OktaUserControl
    {
        public class OktaApplicationTypeSelection
        {
            public OktaApplicationType OktaApplicationType{ get; set; }
            public PictureBox PictureBox { get; set; }
        }

        private readonly Dictionary<OktaApplicationType, PictureBox> oktaApplicationTypeToPictureBox;

        public OktaApplicationTypeControl()
        {
            InitializeComponent();
            PreLoadImages();
            oktaApplicationTypeToPictureBox = new Dictionary<OktaApplicationType, PictureBox>
            {
                { OktaApplicationType.Native, this.NativePictureBox },
                { OktaApplicationType.SinglePageApplication, this.SinglePageAppPictureBox },
                { OktaApplicationType.Web, this.WebPictureBox },
                { OktaApplicationType.Service, this.ServicePictureBox },
                { OktaApplicationType.Repository, new PictureBox() } // TODO: implement this in next version
            };
            this.NativePictureBox.MouseEnter += (s, a) => Highlight(OktaApplicationType.Native);
            this.NativePictureBox.MouseLeave += (s, a) => Unhighlight(OktaApplicationType.Native);
            this.SinglePageAppPictureBox.MouseEnter += (s, a) => Highlight(OktaApplicationType.SinglePageApplication);
            this.SinglePageAppPictureBox.MouseLeave += (s, a) => Unhighlight(OktaApplicationType.SinglePageApplication);
            this.WebPictureBox.MouseEnter += (s, a) => Highlight(OktaApplicationType.Web);
            this.WebPictureBox.MouseLeave += (s, a) => Unhighlight(OktaApplicationType.Web);
            this.ServicePictureBox.MouseEnter += (s, a) => Highlight(OktaApplicationType.Service);
            this.ServicePictureBox.MouseLeave += (s, a) => Unhighlight(OktaApplicationType.Service);

            this.NativePictureBox.Click += (s, a) => Select(OktaApplicationType.Native);
            this.SinglePageAppPictureBox.Click += (s, a) => Select(OktaApplicationType.SinglePageApplication);
            this.WebPictureBox.Click += (s, a) => Select(OktaApplicationType.Web);
            this.ServicePictureBox.Click += (s, a) => Select(OktaApplicationType.Service);
        }

        public OktaApplicationTypeSelection SelectedOktaApplicationType { get; set; }

        protected void Highlight(OktaApplicationType oktaApplicationType)
        {
            SetImage(oktaApplicationTypeToPictureBox[oktaApplicationType], $"{oktaApplicationType.ToString().ToLowerInvariant()}.png");
        }

        protected void Unhighlight()
        {
            foreach (OktaApplicationType oktaApplicationType in oktaApplicationTypeToPictureBox.Keys)
            {
                Unhighlight(oktaApplicationType);
            }
        }

        protected void Unhighlight(OktaApplicationType oktaApplicationType)
        {
            if (oktaApplicationType != SelectedOktaApplicationType?.OktaApplicationType)
            {
                SetImage(oktaApplicationTypeToPictureBox[oktaApplicationType], $"{oktaApplicationType.ToString().ToLowerInvariant()}_exit.png");
            }
        }

        public void Select(OktaApplicationType oktaApplicationType)
        {
            SelectedOktaApplicationType = new OktaApplicationTypeSelection { OktaApplicationType = oktaApplicationType, PictureBox = oktaApplicationTypeToPictureBox[oktaApplicationType] };
            Unhighlight();
            Highlight(oktaApplicationType);
        }

        private void PreLoadImages()
        {
            _ = Task.Run(() => PreLoadImages("native.png", "native_gray.png", "spa.png", "spa_gray.png", "web.png", "web_gray.png", "service.png", "service_gray.png"));
        }

        private void PreLoadImages(params string[] imageResourceNames)
        {
            foreach (string resource in imageResourceNames)
            {
                LoadEmbeddedImageResource(resource);
            }
        }
    }
}
