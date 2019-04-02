using Bogus;
using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Reflection;
using Xunit;

namespace ProgramaPontos.Domain.Testes
{
    public class ExtratoTests
    {
        private Faker faker;

        public ExtratoTests()
        {
            faker = new Faker();
        }

        [Fact(DisplayName = "Adicionar Pontos")]
        public void AdicionarPontos()
        {
            //arrange
            var extrato = CriarExtrato();

            //act
            extrato.AdicionarPontos(10);
            extrato.AdicionarPontos(20);

            //assert
            Assert.Equal( 30, extrato.Saldo);

        }

        [Fact(DisplayName = "Remover Pontos")]
        public void RemoverPontos()
        {
            //arrange
            var extrato = CriarExtrato();

            //act
            extrato.AdicionarPontos(10);
            extrato.RemoverPontos(5);


            //assert
            Assert.Equal(5, extrato.Saldo);

        }

        [Fact(DisplayName = "Carregar do Snapshot")]
        public void CarregarSnapshot()
        {

            //arrange...
            var extratoSnapshot = CriarExtrato();
            extratoSnapshot.AdicionarPontos(100);


            //act...
            var snapshot = new Snapshot<Extrato>(extratoSnapshot);
            var loaded = CreateAggregateFromSnapshot(snapshot);

            //assert...
            Assert.True(
                snapshot.Aggregate.Id == loaded.Id &&
                snapshot.Aggregate.ParticipanteId == loaded.ParticipanteId &&
                snapshot.Aggregate.Saldo == loaded.Saldo &&
                snapshot.Aggregate.Movimentacoes == loaded.Movimentacoes
                );
            

        }


        private T CreateAggregateFromSnapshot<T>(ISnapshot<T> snapshot) where T : IAggregateRoot
        {
            return (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(ISnapshot<T>) }, new ParameterModifier[0])
               .Invoke(new object[] { snapshot });
        }


        [Fact(DisplayName = "Quebra")]
        public void Quebra()
        {
            //arrange
            var extrato = CriarExtrato();

            //act
            extrato.AdicionarPontos(10);
            extrato.RemoverPontos(5);
            extrato.EfetuarQuebra(100);

            //assert
            Assert.Equal(100, extrato.Saldo);
        }


      

        private static Extrato CriarExtrato()
        {
            var participanteId = Guid.NewGuid();
            var extratoId = Guid.NewGuid();
            var extrato = new Extrato(extratoId, participanteId);
            return extrato;
        }






    }
}
