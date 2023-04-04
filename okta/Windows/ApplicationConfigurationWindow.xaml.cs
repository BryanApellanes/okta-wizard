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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Okta.Wizard.Wpf
{
    /// <summary>
    /// Interaction logic for ApplicationConfigurationWindow.xaml
    /// </summary>
    public partial class ApplicationConfigurationWindow : Window
    {
        ConfiguringApplicationViewModel viewModel;
        public ApplicationConfigurationWindow() : this(Di.Get<IOktaWizard>(App.InitializeDi), Di.Get<ILogger>())
        { 
        }

        public ApplicationConfigurationWindow(IOktaWizard oktaWizard, ILogger logger)
        {
            this.OktaWizard = oktaWizard;
            this.OktaWizard.ProjectArguments = App.ProjectArguments;
            this.Logger = logger;
            this.viewModel = new ConfiguringApplicationViewModel();
            this.Loaded += OnLoaded;
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected IOktaWizard OktaWizard { get; set; }

        protected ILogger Logger { get; set; }
    }
}
