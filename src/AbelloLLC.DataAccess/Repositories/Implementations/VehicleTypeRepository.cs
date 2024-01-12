using AbelloLLC.Core.Entities;
using AbelloLLC.DataAccess.Contexts;
using AbelloLLC.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.DataAccess.Repositories.Implementations
{
    public class VehicleTypeRepository : Repository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
