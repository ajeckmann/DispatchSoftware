using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dispatch.Models {
    public class Rescuer {
        [Key]
        public int RescuerId { get; set; }

        [Required (ErrorMessage = "Must include First Name")]
        [MinLength (3, ErrorMessage = "Name must be at least 3 characters")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Must include Last Name")]
        [MinLength (3, ErrorMessage = "Name must be at least 3 characters")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Must include Age")]
        [Range (18, 75, ErrorMessage = "Age must be between 18 and 75")]

        public int Age { get; set; }

        public string Rank { get; set; } //firefighter, paramedic, EMT-b, Captain, Lieutenant

        public int UserId { get; set; }
        public User creator { get; set; }

        //navigation property to refer to association between rescuer and unit

        public List<Assignment> AssignedUnit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatdAt { get; set; } = DateTime.Now;

        //a rescuer is always "available" when entered into the system (IsAvailable=true by default)
        public Boolean IsAssigned { get; set; } = false;
    }
}