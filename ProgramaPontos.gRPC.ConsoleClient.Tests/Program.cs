using System;

namespace ProgramaPontos.gRPC.ConsoleClient.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var ps = new ParticipanteServiceTests();
            ps.CriarParticipante();

            Console.Read();
        }


    }
}
