using System;
using System.Device.Location;

namespace SampleFunctionProject.Data.Model
{
    internal class Car
    {

        public Guid Id { get; set; }
        public string Model { get; set; } = null!;
        public string Engine { get; set; } = null!;
        public string Infotainment_System { get; set; } = null!;
        public GeoCoordinate Location { get; set; } = null!;

    }
}
