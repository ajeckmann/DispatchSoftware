using System.ComponentModel.DataAnnotations;
namespace Dispatch.Models
{
    public class LoginUser
    {
        [EmailAddress]
       [Required(ErrorMessage="Gotta have an Email")]
        
        public string LoginEmail{get;set;}

        [Required(ErrorMessage="You must include a PW")]
        [MinLength(4,ErrorMessage="PW must be at least 10 characters")]
        [DataType(DataType.Password)]
        public string LoginPassword{get;set;}  
    }
}