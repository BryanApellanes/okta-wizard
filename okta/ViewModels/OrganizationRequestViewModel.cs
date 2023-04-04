using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Okta.Wizard.Wpf.ViewModels
{
    public class OrganizationRequestViewModel
    {
        public OrganizationRequestViewModel(Action buttonClickAction, Func<bool>? canExecute = null)
        {
            Country = "United States";
            ButtonText = "Continue";
            ButtonClickCommand = new RelayCommand(buttonClickAction, canExecute ?? (() => true));
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }

        public string ButtonText { get; set; }
        public ICommand ButtonClickCommand { get; set; }


        public bool GetIsValid(out string[] missingProperties)
        {
            List<string> invalids = new List<string>();
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                isValid = false;
                invalids.Add(nameof(FirstName));
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                isValid = false;
                invalids.Add(nameof(LastName));
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                isValid = false;
                invalids.Add(nameof(Email));
            }
            else if (!IsValidEmail(Email))
            {
                isValid = false;
                invalids.Add(nameof(Email));
            }
            
            if (string.IsNullOrWhiteSpace(Country))
            {
                isValid = false;
                invalids.Add(nameof(Country));
            }

            missingProperties = invalids.ToArray();
            return isValid;
        }

        public OrganizationRequest GetOrganizationRequest()
        {
            return new OrganizationRequest
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Country = Country
            };
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
