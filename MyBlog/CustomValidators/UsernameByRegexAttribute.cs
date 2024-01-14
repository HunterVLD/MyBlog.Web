using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyBlog.CustomValidators;

public class UsernameByRegexAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return false;

        string? username = value.ToString();
        Regex regex = new Regex(@"^[A-Za-z][A-Za-z0-9_]{4,19}$");

        return username != null && regex.IsMatch(username);
    }
}