using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ValidationV4
{
    public class UserInfoViewModel : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _email = string.Empty;
        public UserInfoViewModel()
        {
            //ValidateName(_name);
            //ValidateMail(_email);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new(propertyName));

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    ValidateName(_name);
                    OnPropertyChanged();
                    OnErrorsChanged(nameof(Name));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    ValidateMail(_email);
                    OnPropertyChanged();
                    OnErrorsChanged(nameof(Email));
                }
            }
        }

        private void ValidateName(string name)
        {
            var errors = new List<string>(3);
            if (name.Length == 0)
            {
                ClearErrors();
                return;
            }

            if (name.Length < 5)
                errors.Add("Name is forbidden for unknown reasons.");
            //if (name.Contains("1", StringComparison.InvariantCulture))
            //    errors.Add("Using Jungkook's favorite number is not allowed.");

            //if (name.Length == 0)
            //    errors.Add("You'll need a name. I will not accept this.");
            //else if (name.Length > 4)
            //    errors.Add("Your name is too long. Make it shorter.");

            //if (name.Contains("LPK", StringComparison.InvariantCultureIgnoreCase))
            //    errors.Add("Name is forbidden for unknown reasons.");
            //errors.Add("Name is forbidden for unknown reasons.");
            SetErrors("Name", errors);
        }

        private void ValidateMail(string mail)
        {
            var errors = new List<string>(2);
            if (mail.Length == 0)
            {
                ClearErrors();
                return;
            }


            //if (mail.Contains("1", StringComparison.InvariantCulture))
            //    errors.Add("Using Jungkook's favorite number is not allowed.");

            if (!mail.Contains("@", StringComparison.InvariantCultureIgnoreCase))
                errors.Add("Invalid mail.");
            //errors.Add("Invalid mail.");
            SetErrors("Email", errors);
        }


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }

        public bool HasErrors => _validationErrors.Count > 0;
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new();
        public Dictionary<string, ICollection<string>> ValidationErrors
        {
            get => _validationErrors;
        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) ||
                !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }

        private void SetErrors(string key, ICollection<string> errors)
        {
            if (errors.Any())
                _validationErrors[key] = errors;
            else
                _ = _validationErrors.Remove(key);

            OnErrorsChanged(key);
        }

        private void ClearErrors()
        {
             _validationErrors.Clear();
        }
    }
}
