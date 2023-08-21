﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class RegistrationUser
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

    public RegistrationUser(string name, string surname, string nickname, string email, string password) =>
        (this.Name, this.Surname, this.Nickname, this.Email, this.Password) = (name, surname, nickname, email, password);


    public static RegistrationUser Create(string name, string surname, string nickname, string email, string password) =>
        new RegistrationUser(name, surname, nickname, email, password);
    


}






