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
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext context) : base(context)
        {
        }
    }
}
