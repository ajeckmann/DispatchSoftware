using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dispatch.Models {
    public class Unit {
        [Key]
        public int UnitId { get; set; }

        [Required (ErrorMessage = "Must include Unit Designation")]
        public string NumberType { get; set; }

        [Required (ErrorMessage = "Must include Unit Number")]
        [Range (1, 500)]
        public int Number { get; set; }

        [Required (ErrorMessage = "Must include Unit Capacity")]
        public string UnitType { get; set; }

        //unit is always active by default after being created(IsActive==true)
        public Boolean IsAvailable { get; set; } = true;

        public string ResponseStatus { get; set; }

        //navigation property to refer to association between rescuer and unit
        public List<Assignment> personnel { get; set; }

        //navigation property to refer to association between incident and unit (a dispatch)
        public List<Dispatchh> calls { get; set; }

        public int UserId { get; set; }

        public User creator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}