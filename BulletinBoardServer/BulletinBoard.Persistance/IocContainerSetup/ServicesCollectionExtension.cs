using JobPosting.Persistance.Dbcontext;
using JobPosting.Persistance.IocContainerSetup.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPosting.Persistance.IocContainerSetup
{
    public static class ServicesCollectionExtension
    {
        public static void AddPersistanceServicesCollection(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("JobPostingContext");
            services.AddDbContext<JobPostingDbContext>(options => options.
            UseSqlServer(connectionString));

            services.AddJobsServicesCollection();
        }
    }
}
