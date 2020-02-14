
using ProgramaPontos.Application.CommandStack.Core;
using System;

namespace ProgramaPontos.Application
{

    public class Resultado<T> : Resultado
    {
        public Resultado(bool sucesso, string mensagem):base(sucesso,mensagem)
        {
            Dados = default;
        }

        public Resultado(T dados)
        {
            Dados = dados;
            Sucesso = true;
        }

        public T Dados { get; }
    }

    public class Resultado
    {

        public Resultado() : this(true, string.Empty) { }
                
        public Resultado(ICommandResponse commandResponse) :
            this(commandResponse.IsValid, commandResponse.Exception?.Message)
        { }

        
        public Resultado(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public bool Sucesso { get; protected set; }
        public string Mensagem { get; protected set; }



    }
}
