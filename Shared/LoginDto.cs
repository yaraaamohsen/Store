﻿using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
