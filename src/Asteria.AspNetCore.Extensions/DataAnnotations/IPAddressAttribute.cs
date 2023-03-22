using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 指示该属性是合法的IP地址
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IPAddressAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        public override bool IsValid(object? value)
        {
            if (value is string and var str)
            {
                return Regex.IsMatch(str, @"((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}");
            }
            return false;
        }

        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string and var str)
            {
                if (Regex.IsMatch(str, @"((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}"))
                {
                    return ValidationResult.Success;
                }
            }

            if (validationContext.MemberName is not null)
                return new ValidationResult(ErrorMessageString, new[] { validationContext.MemberName });
            else
                return new ValidationResult(ErrorMessageString);
        }
    }
}
