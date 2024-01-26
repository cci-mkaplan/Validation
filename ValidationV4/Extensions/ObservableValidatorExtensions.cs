using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationV4.Extensions
{
    public static class ObservableValidatorExtensions
    {
        public static IEnumerable<string> GetFormattedErrors(this ObservableValidator validator, string propertyName)
        {
            var errors = validator.GetErrors(propertyName);
            if (errors == null)
            {
                return Enumerable.Empty<string>().ToList<string>();
            }

            return errors.Select(s => s.ErrorMessage).ToList();
        }
    }
}
