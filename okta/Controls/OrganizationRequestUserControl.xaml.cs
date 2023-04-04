using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Okta.Wizard.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for OrganizationRequestUserControl.xaml
    /// </summary>
    public partial class OrganizationRequestUserControl : UserControl
    {
        public OrganizationRequestUserControl()
        {
            InitializeComponent();
            HideWarnings();
            WireUiEvents();
        }

        public void ShowWarnings(params string[] labelNames)
        {
            HideWarnings();
            foreach(string labelName in labelNames)
            {
                Label? label = this.FindName($"{labelName}Warning") as Label;
                if (label != null)
                {
                    label.Visibility = Visibility.Visible;
                }
            }
        }

        public void SetButtonText(string text)
        {
            Button? button = this.FindName("Button") as Button;
            if (button != null)
            {
                button.Content = text;
            }
        }

        public string GetButtonText()
        {
            Button? button = this.FindName("Button") as Button;
            if(button != null)
            {
                return button.Content?.ToString();
            }
            return string.Empty;
        }

        public void HideWarnings()
        {
            this.HideWarning("FirstName");
            this.HideWarning("LastName");
            this.HideWarning("Email");
        }

        private void HideWarning(string labelName)
        {
            Label? label = this.FindName($"{labelName}Warning") as Label;
            if (label != null)
            {
                label.Visibility = Visibility.Hidden;
            }
        }

        private T FindName<T>(string name)
        {
            return (T)this.FindName(name);
        }

        private void WireUiEvents()
        {
            WireLostFocusEvent("FirstName");
            WireLostFocusEvent("LastName");
            WireLostFocusEvent("Email");
        }

        private void WireLostFocusEvent(string valueName) 
        {
            TextBox valueTextbox = this.FindName<TextBox>($"{valueName}TextBox");
            valueTextbox.LostFocus += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(valueTextbox.Text))
                {
                    this.HideWarning(valueName);
                }
            };
        }
    }
}
