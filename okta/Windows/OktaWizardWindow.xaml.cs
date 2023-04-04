using Okta.Sdk.Api;
using Okta.Wizard.Config;
using Okta.Wizard.Wpf.Controls;
using Okta.Wizard.Wpf.ViewModels;
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
using System.Windows.Shapes;

namespace Okta.Wizard.Wpf
{
    /// <summary>
    /// Interaction logic for OrgCreationWindow.xaml
    /// </summary>
    public partial class OktaWizardWindow : Window
    {
        OrganizationRequestViewModel viewModel;
        string originalButtonText;
        public OktaWizardWindow() : this(Di.Get<IOktaWizard>(App.InitializeDi), Di.Get<ILogger>())
        {
        }

        public OktaWizardWindow(IOktaWizard oktaWizard, ILogger logger)
        {
            this.OktaWizard = oktaWizard;
            this.OktaWizard.ProjectArguments = App.ProjectArguments;
            this.Logger = logger;
            this.viewModel = new OrganizationRequestViewModel(() => OrganizationRequestButtonClick());
            DataContext = viewModel;
            InitializeComponent();
            this.originalButtonText = this.viewModel.ButtonText;
            this.WireWizardEventListeners();
            this.OktaWizard.RunAsync(new OktaWizardRunArguments(App.ProjectArguments));
        }

        protected IOktaWizard OktaWizard { get; set; }

        protected ILogger Logger { get; set; }

        private async void OrganizationRequestButtonClick()
        {
            Working();
            OrganizationRequestControl.HideWarnings();

            bool isValid = this.viewModel.GetIsValid(out string[] missingProperties);
            if (!isValid)
            {
                OrganizationRequestControl.ShowWarnings(missingProperties);
            }
            if (isValid)
            {
                try
                {
                    OktaWizardRunArguments arguments = new OktaWizardRunArguments
                    {
                        GetOrganizationRequest = () => viewModel.GetOrganizationRequest(),
                        SdkConfig = await OktaWizard.SdkConfigurationExistsAsync() ? await OktaWizard.LoadSdkConfigAsync() : null,
                        ProjectArguments = App.ProjectArguments
                    };
                    await this.OktaWizard.RunAsync(arguments);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            StopWorking();
        }

        private void WireWizardEventListeners()
        {
            OktaWizard.OnCreateNewOrgRequired += (sender, args) => ShowRegister();

            OktaWizard.OnNewOrgVerificationPending += (sender, args) => ShowActivate();

            OktaWizard.OnNewOrgVerificationComplete += (sender, args) => PendingRegistration.Delete();

            OktaWizard.OnCreateApplicationStarted += (sender, args) => ShowConfigure();

            OktaWizard.OnRunComplete += (sender, args) => Close();

            OktaWizard.OnError += (sender, args) => MessageBox.Show(args.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void HideControls()
        {
            OrganizationRequestControl.Visibility = Visibility.Hidden;
            ActivationPendingControl.Visibility = Visibility.Hidden;
            ConfiguringApplicationControl.Visibility = Visibility.Hidden;
        }

        private void ShowRegister()
        {
            HideControls();
            HeaderLabel.Content = "Register";
            OrganizationRequestControl.Visibility = Visibility.Visible;
        }

        private void ShowActivate()
        {
            HideControls();
            HeaderLabel.Content = "Activate";
            ActivationPendingControl.Visibility = Visibility.Visible;
        }

        private void ShowConfigure()
        {
            HideControls();
            HeaderLabel.Content = "Configure";
            ConfiguringApplicationControl.Visibility = Visibility.Visible;
        }

        private void Working()
        {
            OrganizationRequestControl.SetButtonText("Working...");
            OrganizationRequestControl.IsEnabled = false;
        }

        private void StopWorking()
        {
            OrganizationRequestControl.SetButtonText(originalButtonText);
            OrganizationRequestControl.IsEnabled = true;
        }
    }
}
