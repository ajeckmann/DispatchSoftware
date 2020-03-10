using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dispatch.Models
{
    public class User
    {
        [Key]
        public int UserId{get;set;}

        [Required(ErrorMessage="Must add your First Name")]
        [MinLength(2, ErrorMessage="Your First Name must contain at least 2 characters")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="Must add your Last Name")]
        [MinLength(2, ErrorMessage="Your Last Name must contain at least 2 characters")]
        public string LastName {get;set;}

        [Required]
        [EmailAddress]
        public string Email {get;set;}

        [Required(ErrorMessage="You must include a passsword")]
        [MinLength(8, ErrorMessage="Password must contain at least 8 characters long, sunny")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [Required(ErrorMessage="You must confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Your password ain't the same, sunny")]
        [NotMapped]
        public string ConfirmPassword{get;set;}
        public DateTime CreatedAt{get;set;}=DateTime.Now;
        public DateTime UpdatedAt{get;set;}

        public List<Incident> IncidentsDispatched{get;set;}
        
       

    }
}