﻿using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Web.Models.Security.Auth;
public class LoginUser
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }
}
