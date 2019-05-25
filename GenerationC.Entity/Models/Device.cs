using System;
using GenerationC.Database.models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerationC.Database.models
{
    public class Device
    {
        [Required(ErrorMessage="Id is not optional, please insert one Id")]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage="The name is too big, try fewer characters(20)!")]
        [Required(ErrorMessage = "Name can not be empty, please insert a name")]
        public string Name  { get; set; }

        [MaxLength(15, ErrorMessage= "The type is too big, try fewer characters(15)!")]
        [Required(ErrorMessage = "Type can not be empty, please insert a type")]
        public string Type { get; set; }

        [MaxLength(20, ErrorMessage = "The Ip address is too big, try fewer characters(20)!")]
        [Required(ErrorMessage = "Ip address is not optional, please insert one")]
        [Display(Name ="Ip address")]
        public string Gateway { get; set; }

        private DateTime _setDate = DateTime.Now;

        [Display(Name = "Created at")]
        [DataType(DataType.DateTime)]
        public DateTime Created_at {
            get { return _setDate; }
            set { _setDate = value; }
        }

        [Display(Name = "User's Id")]
        public int User_Id { get; set; }
        public virtual User User { get; set; }

    }
}
