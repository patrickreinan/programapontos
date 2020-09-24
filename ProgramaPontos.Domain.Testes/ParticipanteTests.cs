using Bogus;
using ProgramaPontos.Domain.Aggregates.ParticipanteAggregate;
using ProgramaPontos.Domain.Core.Exceptions;
using System;
using Xunit;

namespace ProgramaPontos.Domain.Testes
{
    public class ParticipanteTests
    {
        private readonly Faker faker;

        public ParticipanteTests()
        {
            faker = new Faker();
        }

        [Fact(DisplayName = "Alterar Nome do Participante")]
        public void AlterarNomeParticipante()
        {
            //arrange
            var nome = faker.Person.FullName;
            var nomeAlterado = faker.Person.FullName;
            var email = faker.Internet.Email();
            var participante = new Participante(Guid.NewGuid(), nome, email);

            //act
            participante.AlterarNome(nomeAlterado);

            //assert
            Assert.Equal(participante.Nome, nomeAlterado);

        }

        [Fact(DisplayName = "Alterar Nome do Participante para vazio")]
        public void AlterarNomeParticipanteParaVazio()
        {
            //arrange
            var nome = faker.Person.FullName;
            var nomeAlterado = string.Empty;
            var email = faker.Internet.Email();
            var participante = new Participante(Guid.NewGuid(), nome, email);


            var resultado = Assert.Throws<DomainException>(() => participante.AlterarNome(nomeAlterado));


            //assert
            Assert.NotNull(resultado);

        }


        [Fact(DisplayName = "Alterar Email do Participante")]
        public void AlterarEmailParticipante()
        {
            //arrange
            var nome = faker.Person.FullName;
            var emailAlterado = faker.Internet.Email();
            var email = faker.Internet.Email();
            var participante = new Participante(Guid.NewGuid(), nome, email);


            participante.AlterarEmail(emailAlterado);


            //assert
            Assert.Equal(emailAlterado, participante.Email);

        }

        [Fact(DisplayName = "Alterar Email do Participante para vazio")]
        public void AlterarEmailParticipanteParaVazio()
        {
            //arrange
            var nome = faker.Person.FullName;
            var email = faker.Internet.Email();
            var emailAlterado = string.Empty;
            var participante = new Participante(Guid.NewGuid(), nome, email);


            var resultado = Assert.Throws<DomainException>(() =>
            {

                //act
                participante.AlterarEmail(emailAlterado);

            });

            //assert
            Assert.NotNull(resultado);

        }

        [Fact(DisplayName = "Criar Participante")]
        public void CriarParticipante()
        {
            //arrange
            var nome = faker.Person.FullName;
            var email = faker.Internet.Email();

            //act
            var participante = new Participante(Guid.NewGuid(), nome, email);

            //assert
            Assert.Equal(participante.Nome, nome);
        }
    }
}
