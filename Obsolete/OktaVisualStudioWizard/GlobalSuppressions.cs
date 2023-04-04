// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "Allow await in method body", Scope = "member", Target = "~M:Okta.Wizard.OktaVisualStudioWizard.RunStarted(System.Object,System.Collections.Generic.Dictionary{System.String,System.String},Microsoft.VisualStudio.TemplateWizard.WizardRunKind,System.Object[])")]
