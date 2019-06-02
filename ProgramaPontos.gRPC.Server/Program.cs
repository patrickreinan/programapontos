using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Programapontos.Grpc;
using ProgramaPontos.Infra.Ioc.AspNetCore;
using System;

namespace ProgramaPontos.gRPC.Server
{
    class Program
    {
        const int Port = 50001;


        private static IServiceProvider services;
        private static IConfiguration configuration;

        private static void SetupDependencyInjection()
        {
            services = new ServiceCollection()
                .AddProgramaPontosServices(configuration)
                .AddSingleton<Services.ParticipanteService>()
            .BuildServiceProvider();
        }

        public static void Main(string[] args)
        {
            LoadConfiguration();
            SetupDependencyInjection();
            StartServer();
        }

        private static void LoadConfiguration()
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();
            
        }

        private static void StartServer()
        {
            var server = new Grpc.Core.Server
            {
                Services = { ParticipanteService.BindService(services.GetService<Services.ParticipanteService>()) },
                Ports = { new Grpc.Core.ServerPort("localhost", Port, Grpc.Core.ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine($"Server listening on port {Port}");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
