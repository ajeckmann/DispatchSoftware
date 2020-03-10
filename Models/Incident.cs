using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dispatch.Models
{
    public class Incident
    {
        [Key]
        public int IncidentId{get;set;}

        [Required(ErrorMessage="Must Include Location")]
        public string Location{get;set;}

        [Required(ErrorMessage="Must Include Type")]
        public string Type{get;set;}
        public int UserId{get;set;}
        public List<Dispatchh> dispatchedUnits{get;set;}
        public User Dispatcher{get;set;}

        [Required(ErrorMessage="Must Include Description")]
        public String Description{get;set;}

        public DateTime CreatedAt {get;set;}=DateTime.Now;
        public DateTime UpdatdAt{get;set;}=DateTime.Now;

        

        public Boolean IsActive{get;set;}=true;



        
    }
}
        



        

        


        
 