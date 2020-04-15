using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dispatch.Models {
    public class Dispatchh {
        [Key]
        public int DispatchhId { get; set; }
        public int IncidentId { get; set; }
        public int UnitId { get; set; }

        //navigational property
        public Incident DispatchedIncident { get; set; }

        //navigational property
        public Unit UnitDispatched { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}