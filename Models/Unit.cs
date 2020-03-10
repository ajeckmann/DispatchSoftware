using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dispatch.Models
{
    public class Unit
    {
        [Key]
        public int UnitId{get;set;}

        
        [Required(ErrorMessage="Must include Unit Designation")]
        public string NumberType{get;set;}
        [Required(ErrorMessage="Must include Unit Number")]
        [Range(1,500)]
        public int Number{get;set;}

        [Required(ErrorMessage="Must include Unit Capacity")]
        public string UnitType{get;set;}

        public List<Assignment> personnel{get;set;}
        public List<Dispatchh> calls{get;set;}

        public int UserId{get;set;}

        public User creator{get;set;}

        public DateTime CreatedAt {get;set;}=DateTime.Now;
        public DateTime UpdatedAt{get;set;}=DateTime.Now;

        

        public Boolean IsAvailable{get;set;}=true;
        
        

    }
}