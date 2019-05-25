using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenerationC
{
    public class Login
    {
        [MaxLength(15, ErrorMessage = "Your username is too big, try fewer characters(15)!")]
        [Required(ErrorMessage = "Username can not be empty, please insert a username")]
        public string Username { get; set; }


        [MaxLength(150, ErrorMessage = "Your password is too big, try fewer characters(150)!"),
            MinLength(8, ErrorMessage = "Your password is too short, try fewer characters(8)!")]
        [Required(ErrorMessage = "Password can not be empty, please insert a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(150, ErrorMessage = "Your password is too big, try fewer characters(150)!"),
            MinLength(8, ErrorMessage = "Your password is too short, try fewer characters(8)!")]
        [Required(ErrorMessage = "Password can not be empty, please insert a password")]
        [DataType(DataType.Password)]
        public string NPassword { get; set; }
    }
}