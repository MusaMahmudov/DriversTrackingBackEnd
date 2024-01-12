using AbelloLLC.Business.DTOs.VehicleTypeDTO;
using AbelloLLC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.DriverDTO
{
    public class GetDriverDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public string Dimensions { get; set; }
        public string CurrentLocation { get; set; }
        public string Notes { get; set; }
        public string ZipCode { get; set; }
        public bool isReserved { get; set; }
        public DateTime? ReservedAt { get; set; }
        public string? ReservedBy { get; set; }
        public bool isActive { get; set; }
        public string Capacity { get; set; }
        public TimeSpan reservedAgo { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public GetVehicleTypeForDriverDTO VehicleType { get; set; }
    }
}
