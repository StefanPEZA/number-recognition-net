﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Repository;
using Services.DatasetService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests
{
    internal static class Helper
    {
        public static IServiceProvider Provider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(item => item.UseSqlite("Data Source=num_recog.db"));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDatasetService, DatasetService>();

            return services.BuildServiceProvider();
        }

        public static T GetRequiredService<T>()
        {
            var provider = Provider();

            return provider.GetRequiredService<T>();
        }
    }
}
