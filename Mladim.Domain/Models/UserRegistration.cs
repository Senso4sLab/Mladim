using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class UserPassword
{
    public string UserId { get; set; } = string.Empty;  
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;

}

public class UrlRegistration
{
    public string Email { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}


public class UserRegistration
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Surname { get; set; } = string.Empty;
    [Required]
    public string Nickname { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    public UserRegistration(string name, string surname, string nickname, string email, string password) =>
        (this.Name, this.Surname, this.Nickname, this.Email, this.Password) = (name, surname, nickname, email, password);


    public static UserRegistration Create(string name, string surname, string nickname, string email, string password) =>
        new UserRegistration(name, surname, nickname, email, password);    

}


public class UserRegistrationConfirmation
{   
    [Required]   
    public string EmailToken { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

}







