using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyBlog.CustomValidators;

public class EmailByRegexAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return false;

        string? email = value.ToString();
        Regex regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.[a-zA-Z]{2,6}$");

        return email != null && regex.IsMatch(email);
    }
}