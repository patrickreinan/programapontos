using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.ReadModel.ElasticSearch.Extensions
{
    public static class ElasticSearchConnectionExtensions
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(ElasticSearchSettings)).Get<ElasticSearchSettings>();

            services.AddSingleton<ElasticSearchContext> ((context) =>
            {
                return new ElasticSearchContext(settings);
            });

            return services;
        }

        public static void MappingFor<T>(this ConnectionSettingsBase<ConnectionSettings> connectionSettings) where T : class
        {
            var name = typeof(T).Name.ToLower();

            connectionSettings.DefaultMappingFor<T>(m => m.IndexName(name).IndexName(name));
        }
        
    }
}
