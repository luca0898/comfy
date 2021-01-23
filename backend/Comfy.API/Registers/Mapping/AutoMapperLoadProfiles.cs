using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comfy.Registers.Mapping
{
    public class AutoMapperLoadProfiles
    {
        public static void Load(IServiceCollection services)
        {
            var x =  new MapperConfiguration(config =>
            {
                config.AllowNullDestinationValues = true;
                config.AllowNullCollections = true;

                // Adding each profile
                config.AddProfile<ScheduleProfile>();
                // ...
            }).CreateMapper();

            services.AddSingleton(register => x);
        }
    }
}
