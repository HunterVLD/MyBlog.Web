﻿namespace MyBlog.Models.ViewModels;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }

    public string? returnUrl { get; set; }
}