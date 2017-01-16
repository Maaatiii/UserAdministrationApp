using System.Globalization;
using System.Windows.Controls;

namespace UserAdministrationApp.Desktop.Users
{
    class RequiredFieldValidationRule : ValidationRule
    {
        public RequiredFieldValidationRule()
        {

        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Length > 0)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Required Field Validation");
            }
        }
    }
}