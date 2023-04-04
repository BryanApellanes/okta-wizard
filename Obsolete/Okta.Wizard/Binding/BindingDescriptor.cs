// <copyright file="BindingDescriptor.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Reflection;
using Okta.Wizard.Binding;

namespace Okta.Wizard
{
    /// <summary>
    /// A class that describes a binding.
    /// </summary>
    public class BindingDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindingDescriptor"/> class.
        /// </summary>
        public BindingDescriptor()
        {
            SetTargetValue = () =>
            {
                Type targetType = Target.GetType();
                PropertyInfo propertyInfo = targetType.GetProperty(TargetProperty);
                propertyInfo.SetValue(Target, Source.GetProperty(SourceProperty));
            };
        }

        /// <summary>
        /// Gets or sets the source of data to bind.
        /// </summary>
        /// <value>
        /// The source of data to bind.
        /// </value>
        public Observable Source { get; set; }

        /// <summary>
        /// Gets or sets the target whose properties are set and tracked.
        /// </summary>
        /// <value>
        /// The target whose properties are set and tracked.
        /// </value>
        public object Target { get; set; }

        /// <summary>
        /// Gets or sets the name of the property on the source participating in this binding.
        /// </summary>
        /// <value>
        /// The name of the property on the source participating in this binding.
        /// </value>
        public string SourceProperty { get; set; }

        /// <summary>
        /// Gets or sets the name of the property on the target participating in this binding.
        /// </summary>
        /// <value>
        /// The name of the property on the target participating in this binding.
        /// </value>
        public string TargetProperty { get; set; }

        /// <summary>
        /// Gets or sets the action used to set the target value.
        /// </summary>
        /// <value>
        /// The action used to set the target value.
        /// </value>
        public Action SetTargetValue { get; set; }

        /// <summary>
        /// Sets the source property to the value from the target.
        /// </summary>
        public void SetSourceValue()
        {
            Source.SetProperty(SourceProperty, GetTargetPropertyValue());
        }

        /// <summary>
        /// Gets the source property value.
        /// </summary>
        /// <returns>object</returns>
        public object GetSourcePropertyValue()
        {
            return Source.GetProperty(SourceProperty);
        }

        /// <summary>
        /// Gets the target property value.
        /// </summary>
        /// <returns>object</returns>
        private object GetTargetPropertyValue()
        {
            Type targetType = Target.GetType();
            PropertyInfo propertyInfo = targetType.GetProperty(TargetProperty);
            if (propertyInfo == null)
            {
                return null;
            }

            return propertyInfo.GetValue(Target);
        }
    }
}
