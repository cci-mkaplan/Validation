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
using ValidationV4.Attributes;

namespace ValidationV4
{
    public class UserInfoViewModel : ObservableValidator
    {
        private string _name = string.Empty;
        private string _email = string.Empty;
        public UserInfoViewModel()
        {
        }

        //[Required(ErrorMessage = "Name is Required")]
        //[MinLength(4, ErrorMessage = "Name should be longer than one character")]
        [NameValidation(5)]
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value, true);
            }
        }

        //[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value, true);
            }
        }
    }
}
