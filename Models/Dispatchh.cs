using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dispatch.Models
{
    public class Dispatchh
    {
        [Key]
        public int DispatchhId{get;set;}
        public int IncidentId{get;set;}
        public int UnitId{get;set;}

        public Incident DispatchedIncident{get;set;}
        

        public Unit UnitDispatched{get;set;}

        public DateTime CreatedAt {get;set;}=DateTime.Now;
        public DateTime UpdatdAt{get;set;}=DateTime.Now;

    }
}