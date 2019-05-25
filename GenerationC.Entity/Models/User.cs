using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GenerationC.Database.models;

namespace GenerationC.Database.models
{
    public class User
    {

        [Required(ErrorMessage = "Id is not optional, please insert one Id")]
        public int Id { get; set; }


        [MaxLength(60, ErrorMessage = "The name is too big, try fewer characters(60)!")]
        [Required(ErrorMessage = "Name can not be empty, please insert a name")]
        public string Name { get; set; }


        [MaxLength(60, ErrorMessage = "The email is too big, try fewer characters(60)!")]
        [Required(ErrorMessage = "Email can not be empty, please insert an email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


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
        

        private DateTime _setDate = DateTime.Now;

        [Display(Name = "Created at")]
        [DataType(DataType.DateTime)]
        public DateTime Created_at
        {
            get { return _setDate; }
            set { _setDate = value; }
        }
        public  virtual ICollection<Device> Devices { get; set; }
    }
}
