
using ProgramaPontos.Application.CommandStack.Core;
using System;

namespace ProgramaPontos.Application
{

    public class Resultado<T> : Resultado
    {
        public Resultado(T dados)
        {
            Dados = dados;
        }

        public Resultado(Exception ex) : base(ex)        {        }

        public T Dados { get; }
    }

    public class Resultado
    {
        public Resultado(Exception ex) : this(false, ex.Message, ex) { }

        public Resultado() : this(true, string.Empty, null) { }

        public Resultado(bool sucesso, string mensagem) : this(sucesso, mensagem, null) { }

        public Resultado(ICommandResponse commandResponse) :
            this(commandResponse.IsValid, commandResponse.Exception?.Message, commandResponse.Exception)
        { }



        public Resultado(bool sucesso, string mensagem, Exception exception)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Exception = exception;
        }

        public bool Sucesso { get; protected set; }
        public string Mensagem { get; protected set; }
        public Exception Exception { get; protected set; }



    }
}
