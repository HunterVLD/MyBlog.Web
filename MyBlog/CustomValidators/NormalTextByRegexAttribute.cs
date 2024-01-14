using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyBlog.CustomValidators;

public class NormalTextByRegexAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return false;

        string? normalText = value.ToString();
        Regex regex = new Regex(@"^[\p{L}\p{N}\p{P}\p{Zs}]+$");

        return normalText != null && regex.IsMatch(normalText);
    }
}