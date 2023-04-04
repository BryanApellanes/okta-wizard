// <copyright file="Observable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// A component that provides base databinding functionality.
    /// </summary>
    public class Observable
    {
        static Observable()
        {
            DefaultSetTargetValueDelegate = (bindingDescriptor) => bindingDescriptor.SetTargetValue();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Observable"/> class.
        /// </summary>
        public Observable()
        {
            properties = new Dictionary<string, object>();
            bindings = new List<BindingDescriptor>();

            readListeners = new Dictionary<string, List<Action<Observable, OperationDescriptor>>>();
            readingListeners = new Dictionary<string, List<Action<Observable, OperationDescriptor>>>();

            settingListeners = new Dictionary<string, List<Action<Observable, OperationDescriptor>>>();
            setListeners = new Dictionary<string, List<Action<Observable, OperationDescriptor>>>();

            PropertySetting += OnPropertySetting;
            PropertySet += OnPropertySet;
            PropertyRead += OnPropertyRead;
            PropertyReading += OnPropertyReading;

            SetTargetValueDelegate = DefaultSetTargetValueDelegate;

            Initialize();
        }

        private readonly Dictionary<string, List<Action<Observable, OperationDescriptor>>> readingListeners;
        private readonly Dictionary<string, List<Action<Observable, OperationDescriptor>>> readListeners;

        private readonly Dictionary<string, List<Action<Observable, OperationDescriptor>>> settingListeners;
        private readonly Dictionary<string, List<Action<Observable, OperationDescriptor>>> setListeners;
        private readonly Dictionary<string, object> properties;
        private readonly List<BindingDescriptor> bindings;

        /// <summary>
        /// The event that is raised before a property is read.
        /// </summary>
        public event EventHandler PropertyReading;

        /// <summary>
        /// The event that is raised after a property is read.
        /// </summary>
        public event EventHandler PropertyRead;

        /// <summary>
        /// The event that is raised before a property is set.
        /// </summary>
        public event EventHandler PropertySetting;

        /// <summary>
        /// The event that is rased after a property is set.
        /// </summary>
        public event EventHandler PropertySet;

        /// <summary>
        /// Gets or sets the default implementation used to set target values.
        /// </summary>
        /// <value>
        /// The default implementation used to set target values.
        /// </value>
        public static Action<BindingDescriptor> DefaultSetTargetValueDelegate { get; set; }

        /// <summary>
        /// Gets or sets the delegate used to set target values.
        /// </summary>
        /// <value>
        /// The delegate used to set target values.
        /// </value>
        public Action<BindingDescriptor> SetTargetValueDelegate { get; set; }

        /// <summary>
        /// Sets the target values with values from this Observable.
        /// </summary>
        public void SetTargetValues()
        {
            foreach (BindingDescriptor bindingDescriptor in bindings)
            {
                SetTargetValueDelegate(bindingDescriptor);
            }
        }

        /// <summary>
        /// Sets source values by reading values from the target UI component.
        /// </summary>
        public virtual void ReadInTargetValues()
        {
            foreach (BindingDescriptor bindingDescriptor in bindings)
            {
                bindingDescriptor.SetSourceValue();
            }
        }

        /// <summary>
        /// Binds this Observable to the specified instance.
        /// </summary>
        /// <param name="instance">The instance to bind to.</param>
        public void AddBindings(object instance)
        {
            Type observableType = GetType();
            Type instanceType = instance.GetType();
            foreach (PropertyInfo propertyInfo in observableType.GetProperties())
            {
                if (instanceType.GetProperty(propertyInfo.Name) != null)
                {
                    bindings.Add(new BindingDescriptor
                    {
                        Source = this,
                        Target = instance,
                        SourceProperty = propertyInfo.Name,
                        TargetProperty = propertyInfo.Name,
                    });
                }
            }
        }

        /// <summary>
        /// Binds this Observable to the specified property on the specified target.
        /// </summary>
        /// <param name="target">The target to bind to.</param>
        /// <param name="targetPropertyName">The property name on the target to bind to.</param>
        public void AddBinding(object target, string targetPropertyName)
        {
            AddBinding(target, targetPropertyName, targetPropertyName);
        }

        /// <summary>
        /// Binds the source property of this Observable to the specified target property of the specified target.
        /// </summary>
        /// <param name="target">The target to bind to.</param>
        /// <param name="sourceProperty">The property name on the source to bind to.</param>
        /// <param name="targetPropertyName">The property name on the target to bind to.</param>
        public void AddBinding(object target, string sourceProperty, string targetPropertyName)
        {
            Type instanceType = target.GetType();
            PropertyInfo property = instanceType.GetProperty(targetPropertyName);
            if (property != null)
            {
                bindings.Add(new BindingDescriptor
                {
                    Source = this,
                    Target = target,
                    SourceProperty = sourceProperty,
                    TargetProperty = targetPropertyName,
                });
            }
        }

        /// <summary>
        /// Adds a binding descriptor to the bindings tracked by this Observable.
        /// </summary>
        /// <param name="bindingDescriptor">The binding descriptor.</param>
        public void AddBinding(BindingDescriptor bindingDescriptor)
        {
            bindings.Add(bindingDescriptor);
        }

        /// <summary>
        /// Adds a listener for the specified property.  Before the property is read the listener is executed.
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="readingListener">The listener.</param>
        public void OnReading(string propertyName, Action<Observable, OperationDescriptor> readingListener)
        {
            if (!readingListeners.ContainsKey(propertyName))
            {
                readingListeners.Add(propertyName, new List<Action<Observable, OperationDescriptor>>());
            }

            readingListeners[propertyName].Add(readingListener);
        }

        /// <summary>
        /// Adds a listener for the specified property.  After the property is read the listener is executed.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="readListener">The listener.</param>
        public void OnRead(string propertyName, Action<Observable, OperationDescriptor> readListener)
        {
            if (!readListeners.ContainsKey(propertyName))
            {
                readListeners.Add(propertyName, new List<Action<Observable, OperationDescriptor>>());
            }

            readListeners[propertyName].Add(readListener);
        }

        /// <summary>
        /// Adds a listener for the specified property.  Before the property is set the listener is executed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="settingListener">The listener.</param>
        public void OnSetting(string propertyName, Action<Observable, OperationDescriptor> settingListener)
        {
            if (!settingListeners.ContainsKey(propertyName))
            {
                settingListeners.Add(propertyName, new List<Action<Observable, OperationDescriptor>>());
            }

            settingListeners[propertyName].Add(settingListener);
        }

        public void OnSet<T>(string propertyName, Action<T, OperationDescriptor> setListener)
            where T : Observable
        {
            OnSet(propertyName, (observable, operationDescriptor) => setListener((T)observable, operationDescriptor));
        }

        /// <summary>
        /// Adds a listener for the specified property.  After the property is set the listener is executed.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="setListener">The listener.</param>
        public void OnSet(string propertyName, Action<Observable, OperationDescriptor> setListener)
        {
            if (!setListeners.ContainsKey(propertyName))
            {
                setListeners.Add(propertyName, new List<Action<Observable, OperationDescriptor>>());
            }

            setListeners[propertyName].Add(setListener);
        }

        /// <summary>
        /// Gets the value of the specified property casting it to the specified generic type.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>T</returns>
        public T GetProperty<T>(string propertyName)
        {
            object value = GetProperty(propertyName);
            if (value == null)
            {
                return default;
            }

            return (T)value;
        }

        /// <summary>
        /// Gets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>object</returns>
        public object GetProperty(string propertyName)
        {
            object value = null;
            if (properties.ContainsKey(propertyName))
            {
                PropertyReading?.Invoke(this, new ObservableEventArgs { Observable = this, OperationDescriptor = new OperationDescriptor { PropertyName = propertyName, OperationKind = OperationKind.Read } });

                value = properties[propertyName];

                PropertyRead?.Invoke(this, new ObservableEventArgs { Observable = this, OperationDescriptor = new OperationDescriptor { PropertyName = propertyName, PropertyValue = value, OperationKind = OperationKind.Read } });
            }

            return value;
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns>Observable</returns>
        public Observable SetProperty(string propertyName, object value)
        {
            PropertySetting?.Invoke(this, new ObservableEventArgs { Observable = this, OperationDescriptor = new OperationDescriptor { PropertyName = propertyName, PropertyValue = value, OperationKind = OperationKind.Write } });

            if (properties.ContainsKey(propertyName))
            {
                properties[propertyName] = value;
            }
            else
            {
                properties.Add(propertyName, value);
            }

            PropertySet?.Invoke(this, new ObservableEventArgs { Observable = this, OperationDescriptor = new OperationDescriptor { PropertyName = propertyName, PropertyValue = value, OperationKind = OperationKind.Write } });

            return this;
        }

        /// <summary>
        /// Initialize this Observable.
        /// </summary>
        protected void Initialize()
        {
            Type type = GetType();
            foreach (PropertyInfo property in type.GetProperties())
            {
                properties.Add(property.Name, null);
            }
        }

        /// <summary>
        /// Executes listeners that are listening to pre-read events.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments.</param>
        protected void OnPropertyReading(object sender, EventArgs args)
        {
            ObservableEventArgs observableEventArgs = (ObservableEventArgs)args;
            string propertyName = observableEventArgs.OperationDescriptor?.PropertyName;
            FireListeners(readingListeners, observableEventArgs, propertyName);
        }

        /// <summary>
        /// Executes listeners that are listening to post-read events.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments.</param>
        protected void OnPropertyRead(object sender, EventArgs args)
        {
            ObservableEventArgs observableEventArgs = (ObservableEventArgs)args;
            string propertyName = observableEventArgs.OperationDescriptor?.PropertyName;
            FireListeners(readListeners, observableEventArgs, propertyName);
        }

        /// <summary>
        /// Executes listeners that are listening to pre-set events.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments.</param>
        protected void OnPropertySetting(object sender, EventArgs args)
        {
            ObservableEventArgs observableEventArgs = (ObservableEventArgs)args;
            string propertyName = observableEventArgs.OperationDescriptor?.PropertyName;
            FireListeners(settingListeners, observableEventArgs, propertyName);
        }

        /// <summary>
        /// Executes listeners that are listening to post-set events.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments.</param>
        protected void OnPropertySet(object sender, EventArgs args)
        {
            ObservableEventArgs observableEventArgs = (ObservableEventArgs)args;
            string propertyName = observableEventArgs.OperationDescriptor?.PropertyName;
            FireListeners(setListeners, observableEventArgs, propertyName);
        }

        private void FireListeners(Dictionary<string, List<Action<Observable, OperationDescriptor>>> listeners, ObservableEventArgs observableEventArgs, string propertyName)
        {
            if (listeners.ContainsKey(propertyName))
            {
                foreach (Action<Observable, OperationDescriptor> listener in listeners[propertyName])
                {
                    try
                    {
                        listener(this, observableEventArgs.OperationDescriptor);
                    }
                    catch (Exception ex)
                    {
                        Log(1, ex.Message);
                    }
                }
            }
        }

        private void Log(int severity, string message)
        {
            // Basic logging implementation until a more robust solution is wired up
            Console.WriteLine("{0}: {1}", severity, message);
        }

        private void ThrowIfNull(object value, string message)
        {
            if (value == null)
            {
                throw new ArgumentException(message);
            }
        }

        private void ThrowIfNullOrEmpty(string propertyName, string message = null)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(message ?? "propertyName not specified");
            }
        }
    }
}
