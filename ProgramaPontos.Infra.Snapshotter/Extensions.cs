using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramaPontos.Domain.Events;

namespace ProgramaPontos.Infra.Snapshotter
{
    public static class Extensions
    {
        public static IServiceCollection AddSnapshotSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection.AddSingleton((context) =>
            {
                return configuration.GetSection(nameof(SnapshotSettings)).Get<SnapshotSettings>();
            });
        }

        public static IServiceCollection AddDomainEventHandlers(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddScoped<IDomainEventHandler<ExtratoPontosAdicionadosDomainEvent>, ExtratoDomainEventHandler>()
                .AddScoped<IDomainEventHandler<ExtratoPontosRemovidosDomainEvent>, ExtratoDomainEventHandler>()
                .AddScoped<IDomainEventHandler<ExtratoQuebraAdicionadaDomainEvent>, ExtratoDomainEventHandler>();

        }
    }
}
