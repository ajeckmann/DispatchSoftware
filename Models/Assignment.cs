using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatch.Models {
    public class Assignment {
        [Key]
        public int AssignmentId { get; set; }
        public int RescuerId { get; set; }
        public int UnitId { get; set; }

        //navigational property
        public Rescuer RiderAssigned { get; set; }

        //navigational property
        public Unit UnitAssigned { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatdAt { get; set; } = DateTime.Now;
    }
}