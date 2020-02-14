
using ProgramaPontos.Application.CommandStack.Core;
using System;

namespace ProgramaPontos.Application
{

    public class Resultado<T> : Resultado
    {
        public Resultado(bool sucesso, string mensagem) : base(sucesso, new[] { mensagem })
        {

        }
        public Resultado(bool sucesso, string[] mensagens):base(sucesso,mensagens)
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
            this(commandResponse.IsValid, commandResponse.Reasons)
        { 
        
        }

        public Resultado(bool sucesso, string[] mensagens)
        {
            this.Sucesso = sucesso;
            this.Mensagens = mensagens;
        }



        public Resultado(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagens =new string[] { mensagem };
        }

        public bool Sucesso { get; protected set; }
        public string[] Mensagens { get; protected set; }



    }
}
