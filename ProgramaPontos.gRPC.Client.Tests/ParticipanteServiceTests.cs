using Bogus;
using Grpc.Core;
using Programapontos.Grpc;
using System;
using Xunit;


namespace ProgramaPontos.gRPC.Client.Tests
{
    public class ParticipanteServiceTests
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

        [Fact]
        public void CriarParticipante()
        {
            var request = CriarParticipanteRequestFake();

            var reply = client.CriarParticipante(request);
            channel.ShutdownAsync().Wait();

            Assert.True(reply.Sucesso);
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
