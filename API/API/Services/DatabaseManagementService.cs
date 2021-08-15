using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public static class DatabaseManagamentService
    {
        public static void MigrationInitialization(IApplicationBuilder app)
        {
            using (var servicesScope = app.ApplicationServices.CreateScope())
            {
                servicesScope.ServiceProvider.GetService<Context>().Database.Migrate();
            }
        }
    }
}
