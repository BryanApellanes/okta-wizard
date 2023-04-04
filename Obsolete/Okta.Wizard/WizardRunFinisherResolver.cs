// <copyright file="WizardRunFinisherResolver.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Okta.Wizard
{
    /// <summary>
    /// Component used to resolve wizard run finishers.
    /// </summary>
    public class WizardRunFinisherResolver : IWizardRunFinisherResolver
    {
        private readonly Dictionary<OktaApplicationType, Type> projectFinalizerTypes;
        private readonly Dictionary<OktaApplicationType, IWizardRunFinisher> projectFinalizerInstances;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardRunFinisherResolver"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        public WizardRunFinisherResolver(IUserManager userManager)
        {
            this.UserManager = userManager;
            this.projectFinalizerTypes = new Dictionary<OktaApplicationType, Type>
            {
                { OktaApplicationType.None, typeof(WizardRunFinisher) },
                { OktaApplicationType.Native, typeof(NativeWizardRunFinisher) },
                { OktaApplicationType.SinglePageApplication, typeof(SinglePageApplicationWizardRunFinisher) },
                { OktaApplicationType.Web, typeof(WebWizardRunFinisher) },
                { OktaApplicationType.Service, typeof(ServiceWizardRunFinisher) },
                { OktaApplicationType.Repository, typeof(RepositoryWizardRunFinisher) },
            };
            this.projectFinalizerInstances = new Dictionary<OktaApplicationType, IWizardRunFinisher>();
        }

        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public IUserManager UserManager { get; set; }

        /// <summary>
        /// Sets the type of finisher for the specified Okta application type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        public void SetWizardRunFinisher<T>(OktaApplicationType oktaApplicationType)
            where T : IWizardRunFinisher
        {
            SetWizardRunFinisher(typeof(T), oktaApplicationType);
        }

        /// <summary>
        /// Sets the type of finisher for the specified Okta application type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        public void SetWizardRunFinisher(Type type, OktaApplicationType oktaApplicationType)
        {
            if (projectFinalizerTypes.ContainsKey(oktaApplicationType))
            {
                projectFinalizerTypes[oktaApplicationType] = type;
            }
            else
            {
                projectFinalizerTypes.Add(oktaApplicationType, type);
            }
        }

        /// <summary>
        /// Sets the finisher instance used for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="instance">The instance.</param>
        public void SetWizardRunFinisher(OktaApplicationType oktaApplicationType, IWizardRunFinisher instance)
        {
            this.SetWizardRunFinisher(instance.GetType(), oktaApplicationType);
            if (!this.projectFinalizerInstances.ContainsKey(oktaApplicationType))
            {
                this.projectFinalizerInstances.Add(oktaApplicationType, instance);
            }
            else
            {
                this.projectFinalizerInstances[oktaApplicationType] = instance;
            }
        }

        /// <summary>
        /// Gets a finisher for the specified result.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <param name="useExisting">A value indicating whether to use previously retrieved finisher.</param>
        /// <returns>IWizardRunFinisher</returns>
        public virtual IWizardRunFinisher GetWizardRunFinisher(OktaWizardResult oktaWizardResult, bool useExisting = false)
        {
            OktaApplicationType oktaApplicationType = oktaWizardResult.GetOktaApplicationType();
            return GetWizardRunFinisher(oktaApplicationType, useExisting);
        }

        /// <summary>
        /// Gets a finisher for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="useExisting">A value indicating whether to use previously retrieved finisher.</param>
        /// <returns>IWizardRunFinisher</returns>
        public virtual IWizardRunFinisher GetWizardRunFinisher(OktaApplicationType oktaApplicationType, bool useExisting = false)
        {
            if (useExisting && this.projectFinalizerInstances.ContainsKey(oktaApplicationType))
            {
                return projectFinalizerInstances[oktaApplicationType];
            }
            Type type = projectFinalizerTypes[oktaApplicationType];
            ConstructorInfo ctor = type.GetConstructor(new Type[] { typeof(IUserManager) });
            IWizardRunFinisher instance = (IWizardRunFinisher)ctor.Invoke(new object[] { UserManager });
            if (this.projectFinalizerInstances.ContainsKey(oktaApplicationType))
            {
                this.projectFinalizerInstances[oktaApplicationType] = instance;
            }
            else
            {
                this.projectFinalizerInstances.Add(oktaApplicationType, instance);
            }

            return instance;
        }
    }
}
