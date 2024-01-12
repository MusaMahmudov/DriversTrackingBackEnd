using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.DriverDTO
{
    public class PutDriverDTO
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public string Dimensions { get; set; }
        public string? CurrentLocation { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Notes { get; set; }
        public string Capacity { get; set; }
        public bool isReserved { get; set; }
        public string? ZipCode { get; set; }
        public bool isActive { get; set; }
        public Guid VehicleTypeId { get; set; }
    }
}
