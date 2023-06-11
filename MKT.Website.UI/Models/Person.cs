﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MKT.Website.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        //[DisplayName("الاسم*")]
        [Required(ErrorMessage = "Please Enter your Name")]
        [MinLength(3, ErrorMessage = "Please Enter your FullName correctly")]
        [MaxLength(50, ErrorMessage = "Please Check your Name")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Please Enter your Phone")]
        [MinLength(6, ErrorMessage = "Please Enter your Phone correctly")]
        [MaxLength(12, ErrorMessage = "Please check your Phone")]
        [Phone(ErrorMessage = "Please Enter your Phone correctly")]
        public string? PersonPhone { get; set; }

        [Required(ErrorMessage = "Please Enter your E-mail")]
        [MinLength(5, ErrorMessage = "Please Enter your E-mail correctly")]
        [MaxLength(50, ErrorMessage = "Please check your E-mail")]
        [EmailAddress(ErrorMessage = "Please Enter your E-mail correctly")]
        public string? PersonEmail { get; set; }
        public string? PersonSubscription { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


    }
}

