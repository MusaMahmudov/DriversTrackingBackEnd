using AbelloLLC.Business.DTOs.DriverDTO;
using AbelloLLC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.VehicleTypeDTO
{
    public class GetVehicleTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<GetDriverForVehicleTypeDTO>? Drivers { get; set; }
    }
}
