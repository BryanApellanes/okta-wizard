using Okta.Wizard.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Okta.Wizard;

namespace Okta.VisualStudio.Wizard.Controls
{
    public static class ControlExtensions
    {
        private delegate void SetObjectPropertyDelegate(object obj, string propertyName, object value);
        public static void SetObjectProperty(this object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"The specified control of type {type.FullName} does not have a property named {propertyName}.");
            }
            property.SetValue(obj, value);
        }
        
        public static void Show(this Control control)
        {
            control.SetControlProperty("Visible", true);
        }

        public static void Hide(this Control control)
        {
            control.SetControlProperty("Visible", false);
        }

        private delegate void SetPropertyDelegate(Control control, string propertyName, object value);
        /// <summary>
        /// Provides a thread safe way to set a control's property value.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetControlProperty(this Control control, string propertyName, object value)
        {
            Type controlType = control.GetType();
            PropertyInfo property = controlType.GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"The specified control of type {controlType.FullName} does not have a property named {propertyName}.");
            }
            if (control.InvokeRequired)
            {
                SetPropertyDelegate spd = new SetPropertyDelegate(SetControlProperty);
                control.Invoke(spd, new object[] { control, propertyName, value });
            }
            else
            {
                property.SetValue(control, value);
            }
        }

        public static void BindObservable(this Control targetControl, Observable sourceObservable, string sourceProperty, string targetProperty)
        {
            sourceObservable.SetTargetValueDelegate = SetTargetPropertyValue;
            sourceObservable.AddBindings(targetControl);
            sourceObservable.AddBinding(targetControl, sourceProperty, targetProperty);
            sourceObservable.SetTargetValues();

            targetControl.BindChildren(sourceObservable);            
        }

        private delegate void SetObservableTargetValueDelegate(BindingDescriptor bindingDescriptor);

        private static void SetTargetPropertyValue(BindingDescriptor bindingDescriptor)
        {
            if (bindingDescriptor.Target is Control targetControl)
            {
                if (targetControl.InvokeRequired)
                {
                    SetObservableTargetValueDelegate sotvd = new SetObservableTargetValueDelegate(SetTargetPropertyValue);
                    sotvd.Invoke(bindingDescriptor);
                }
                else
                {
                    Type type = targetControl.GetType();
                    PropertyInfo propertyInfo = type.GetProperty(bindingDescriptor.TargetProperty);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(targetControl, bindingDescriptor.GetSourcePropertyValue());
                    }
                }
            }
            else
            {
                bindingDescriptor.SetTargetValue();
            }
        }

        public static void BindChildren(this Control parentControl, Observable observable)
        {
            Type type = observable.GetType();
            foreach (PropertyInfo property in type.GetProperties())
            {
                string targetProperty = "Text";
                if (property.PropertyType == typeof(bool))
                {
                    targetProperty = "Checked";
                }

                if (property.PropertyType != typeof(string) && property.PropertyType != typeof(bool))
                {
                    continue;
                }

                foreach (Control control in parentControl.FindControlsTagged(property.Name))
                {
                    control.SetControlProperty(targetProperty, property.GetValue(observable));

                    BindingDescriptor bindingDescriptor = new BindingDescriptor
                    {
                        Source = observable,
                        Target = control,
                        SourceProperty = property.Name,
                        TargetProperty = targetProperty,
                    };
                    observable.AddBinding(bindingDescriptor);
                    observable.OnSet(property.Name, (o, od) => control.SetControlProperty(targetProperty, od.PropertyValue));
                    if (property.PropertyType == typeof(bool))
                    {
                        control.Click += (s, a) => bindingDescriptor.SetSourceValue();
                    }
                    else
                    {
                        control.KeyUp += (s, a) => bindingDescriptor.SetSourceValue();
                    }
                }
            }
        }

        public static IEnumerable<Control> FindControlsTagged(this Control controlToSearch, string tag)
        {
            foreach (Control control in controlToSearch.Controls)
            {
                if (control.Tag != null && control.Tag.Equals(tag))
                {
                    yield return control;
                }
            }
        }
    }
}
