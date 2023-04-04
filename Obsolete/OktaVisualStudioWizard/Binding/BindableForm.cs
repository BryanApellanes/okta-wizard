// <copyright file="BindableForm.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Okta.VisualStudio.Wizard.Controls;
using Okta.Wizard.Binding;

namespace Okta.VisualStudio.Wizard.Forms
{
    public partial class BindableForm : Form
    {
        public BindableForm()
        {
            InitializeComponent();
        }

        private const int GWL_STYLE = -16;
        private const int WS_CLIPSIBLINGS = 1 << 26;

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        protected override void OnLoad(EventArgs e)
        {
            int style = (int)((long)GetWindowLong32(new HandleRef(this, this.Handle), GWL_STYLE));
            SetWindowLongPtr32(new HandleRef(this, this.Handle), GWL_STYLE, new HandleRef(null, (IntPtr)(style & ~WS_CLIPSIBLINGS)));

            base.OnLoad(e);
        }

        public void SelectControlTaggedWith(string controlTag)
        {
            Control control = FindControlsTagged(controlTag).FirstOrDefault();
            if (control != null)
            {
                if (control is BindableControl bindableControl)
                {
                    bindableControl.SelectControlTaggedWith(controlTag);
                }
                else
                {
                    control.Select();
                }
            }
        }

        public void FocusOnControlTaggedWith(string controlTag)
        {
            Control control = FindControlsTagged(controlTag).FirstOrDefault();
            if (control != null)
            {
                if (control is BindableControl bindableControl)
                {
                    bindableControl.FocusOnControlTaggedWith(controlTag);
                }
                else
                {
                    control.Focus();
                }
            }
        }

        public void BindObservable(Observable dataSource)
        {
            ControlExtensions.BindChildren(this, dataSource);
        }

        public void BindTaggedControl(Observable sourceObservable, string controlTaggedWith)
        {
            BindTaggedControl(sourceObservable, controlTaggedWith, controlTaggedWith, controlTaggedWith);
        }

        public void BindTaggedControl(Observable sourceObservable, string controlTaggedWith, string sourceProperty, string targetProperty)
        {
            foreach (Control control in FindControlsTagged(controlTaggedWith))
            {
                if (control is BindableControl bindableControl)
                {
                    bindableControl.BindModel(sourceObservable);
                }
                else
                {
                    control.BindObservable(sourceObservable, sourceProperty, targetProperty);
                }
            }
        }

        public void SetControlProperty(Control control, string propertyName, object value)
        {
            control.SetControlProperty(propertyName, value);
        }

        public void SetObjectProperty(object obj, string propertyName, object value)
        {
            obj.SetObjectProperty(propertyName, value);
        }

        protected IEnumerable<Control> FindControlsTagged(string tag)
        {
            foreach (Control control in Controls)
            {
                if (control.Tag != null && control.Tag.Equals(tag))
                {
                    yield return control;
                }
            }
        }
    }
}
