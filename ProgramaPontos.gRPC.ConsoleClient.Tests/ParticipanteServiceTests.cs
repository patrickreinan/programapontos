using Bogus;
using Grpc.Core;
using Programapontos.Grpc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.gRPC.ConsoleClient.Tests
{
    class ParticipanteServiceTests
    {
        private Channel channel;
        private ParticipanteService.ParticipanteServiceClient client;
        private Faker faker;

        public ParticipanteServiceTests()
        {
            faker = new Faker();
            channel = new Channel("localhost:50001", ChannelCredentials.Insecure);
            client = new ParticipanteService.ParticipanteServiceClient(channel);
        }


        public void CriarParticipante()
        {
            var request = CriarParticipanteRequestFake();

            var reply = client.CriarParticipante(request);
            if (!reply.Sucesso) throw new InvalidOperationException();

            System.Threading.Thread.Sleep(10000);

            var requestedFromDatabase = client.RetornarParticipantePorEmail(new RetornarParticipantePorEmailRequest() { Email = request.Email });
            if (requestedFromDatabase.Dados.Id != request.Id) throw new InvalidOperationException();

            channel.ShutdownAsync().Wait();

            Console.WriteLine($"OK -> {requestedFromDatabase.Dados.Nome} - {requestedFromDatabase.Dados.Email} - {requestedFromDatabase.Dados.Id} ");
        }

        private CriarParticipanteRequest CriarParticipanteRequestFake()
        {
            return new CriarParticipanteRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Nome = faker.Name.FullName(),
                Email = faker.Internet.Email()

            };
        }
    }
}
