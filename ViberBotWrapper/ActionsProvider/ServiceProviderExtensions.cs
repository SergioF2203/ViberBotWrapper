﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ViberBotWebApp.ActionsProvider
{
    public static class ServiceProviderExtensions
    {
        public static void AddMessageService(this IServiceCollection services)
        {
            services.AddTransient<MessageActionProvider>();
        }

        public static void AddStateManagerService(this IServiceCollection services)
        {
            services.AddSingleton<StateManagerService>();
        }
    }
}
