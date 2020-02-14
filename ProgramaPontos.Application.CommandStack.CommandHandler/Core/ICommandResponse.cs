using System;

namespace ProgramaPontos.Application.CommandStack.Core
{
    public interface ICommandResponse
    {
        bool IsValid { get; }
        string[] Reasons { get; }
    }
}