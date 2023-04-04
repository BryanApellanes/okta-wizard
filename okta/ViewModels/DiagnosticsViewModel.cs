using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Okta.Wizard.Wpf.ViewModels
{
    public class DiagnosticsViewModel
    {
        public string Text { get; set; }
        public string Label { get; set; }

        public string LeftButtonText { get; set; }
        public string CenterButtonText { get; set; }
        public string RightButtonText { get; set; }

        public ICommand LeftButtonClickCommand { get; private set; }
        public ICommand CenterButtonClickCommand { get; private set; }
        public ICommand RightButtonClickCommand { get; private set; }
    }
}
