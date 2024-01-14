using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyBlog.CustomValidators;

public class PasswordByRegexAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return false;

        string? password = value.ToString();
        Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).{8,}$");

        return password != null && regex.IsMatch(password);
    }
}