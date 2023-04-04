using Okta.Wizard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okta.VisualStudio.Wizard.Forms
{
    public partial class TestUserForm : FlatBindableForm
    {
        public TestUserForm()
        {
            InitializeComponent();
            AcceptButton = OkButton;
        }

        public static void ShowUserCredentials(TestUser testUser, string saveToPath)
        {
            testUser.ToYamlFile(saveToPath);
            TestUserForm testUserForm = new TestUserForm();
            string text = $"User Login: {testUser.UserProfile.Login}\r\nPassword: {testUser.Password}";
            testUserForm.TestUserCredentialsTextBox.Text = $"{text}\r\n\r\nThis information is saved to {saveToPath}";
            testUserForm.ShowDialog();
        }
    }
}
