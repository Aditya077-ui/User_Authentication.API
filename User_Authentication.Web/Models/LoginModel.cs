﻿using System.ComponentModel.DataAnnotations;

namespace User_Authentication.Web.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
