// <copyright file="BindableControl.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Okta.Wizard;
using Okta.Wizard.Binding;

namespace Okta.VisualStudio.Wizard.Controls
{
    public class BindableControl : UserControl
    {
        public BindableControl()
        {
            BindingDescriptors = new List<BindingDescriptor>();
        }

        public void BindModel(Observable observable)
        {
            Model = observable ?? throw new ArgumentException("observable not specified");
            BindObservable(observable);
            ModelBound?.Invoke(this, new ModelBoundEventArgs { Control = this, Observable = observable });
        }

        public event EventHandler ModelBound;

        public void BindObservable(Observable dataSource)
        {
            ControlExtensions.BindChildren(this, dataSource);
        }

        public void AddBinding(Observable dataSource, string propertyName)
        {
            AddBinding(dataSource, propertyName, propertyName);
        }

        public void AddBinding(Observable dataSource, string sourceProperty, string targetProperty)
        {
            dataSource.AddBinding(this, sourceProperty, targetProperty);
        }

        public Observable Model { get; set; }

        public void SelectControlTaggedWith(string controlTag)
        {
            foreach (Control control in Controls)
            {
                if (control.Tag.Equals(controlTag))
                {
                    control.Select();
                }
            }
        }

        public void FocusOnControlTaggedWith(string controlTag)
        {
            foreach (Control control in Controls)
            {
                if (control.Tag.Equals(controlTag))
                {
                    control.Focus();
                }
            }
        }

        protected List<BindingDescriptor> BindingDescriptors { get; }

        protected void SetEnabled(Control control, bool enabled)
        {
            control.SetControlProperty("Enabled", enabled);
        }

        protected void SetText(Control control, string text)
        {
            control.SetControlProperty("Text", text);
        }

        protected void SetVisible(Control control, bool visible)
        {
            control.SetControlProperty("Visible", visible);
        }

        private delegate void SetPropertyDelegate(Control control, string propertyName, object value);

        protected void SetProperty(Control control, string propertyName, object value)
        {
            Type controlType = control.GetType();
            PropertyInfo property = controlType.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"The specified control of type {controlType.FullName} does not have a property named {propertyName}.");
            }

            if (control.InvokeRequired)
            {
                SetPropertyDelegate spd = new SetPropertyDelegate(SetProperty);
                control.Invoke(spd, new object[] { control, propertyName, value });
            }
            else
            {
                property.SetValue(control, value);
            }
        }

        protected Image LoadEmbeddedImageResource(string imageResourceName)
        {
            return LoadEmbeddedImageResource(Assembly.GetExecutingAssembly(), imageResourceName);
        }

        private static readonly Dictionary<string, Image> ImageResourceCache = new Dictionary<string, Image>();
        private static readonly object ImageResourceCacheLock = new object();

        protected static Image LoadEmbeddedImageResource(Assembly assembly, string imageResourceName)
        {
            if (!ImageResourceCache.ContainsKey(imageResourceName))
            {
                lock (ImageResourceCacheLock)
                {
                    if (!ImageResourceCache.ContainsKey(imageResourceName))
                    {
                        string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(r => r.Contains(imageResourceName));
                        if (!string.IsNullOrEmpty(resourceName))
                        {
                            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                            {
                                if (stream != null)
                                {
                                    ImageResourceCache.Add(imageResourceName, Image.FromStream(stream));
                                }
                            }
                        }
                    }
                }
            }

            if (ImageResourceCache.ContainsKey(imageResourceName))
            {
                return ImageResourceCache[imageResourceName];
            }

            return null;
        }

        protected void SetImage(PictureBox pictureBox, string resourceName)
        {
            pictureBox.SetControlProperty("Image", LoadEmbeddedImageResource(resourceName));
        }
    }
}
