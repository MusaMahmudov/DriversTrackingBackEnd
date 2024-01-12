using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.DriverDTO
{
    public class GetDriverForVehicleTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public string Dimensions { get; set; }
        public string Capacity { get; set; }

        public string CurrentLocation { get; set; }
        public string Notes { get; set; }
    }
}
