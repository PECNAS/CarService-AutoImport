﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class EditUserModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
