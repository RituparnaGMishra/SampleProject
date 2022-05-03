using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleFunctionProject.Data.Model
{
    internal class Demand
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public GeoCoordinate PickupLocation { get; set; } = null!;

#nullable enable
        public GeoCoordinate? DropOffLocation { get; set; }
        public string? DesiredModel { get; set; } 
        public string? DesiredEngine { get; set; } 
        public string? DesiredInfotainment_System { get; set; } 
#nullable disable
      
    }
}
