using System;

namespace MMO_RPG
{
    public class AlreadyExistException : Exception
    {
        public override string Message { get; }

        public AlreadyExistException(string message)
        {
            Message = message;
        }
    }
}