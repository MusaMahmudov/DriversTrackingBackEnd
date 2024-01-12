using AbelloLLC.DataAccess.Contexts;
using AbelloLLC.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.DataAccess.Background
{
    public class DriverReserv : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public DriverReserv(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var drivers =  dbContext.Drivers.ToList();

                    var cutoffTime = DateTime.UtcNow.AddMinutes(-19);

                    foreach (var driver in drivers)
                    {
                        if  ( driver.isReserved && driver.ReservedAt <= cutoffTime)
                        {
                            driver.ReservedBy = null;
                            driver.ReservedAt = null;
                            driver.isReserved = false;
                            dbContext.Drivers.Update(driver);
                        }
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
