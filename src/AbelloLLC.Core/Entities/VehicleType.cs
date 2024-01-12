using AbelloLLC.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Core.Entities
{
    public class VehicleType : BaseSectionEntity
    {
        public string Name { get; set; }
        public List<Driver>? Drivers { get; set; }
    }
}
