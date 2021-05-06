using System;

namespace LameScooter
{
    public class NotFoundException : Exception
    {
        public override string Message { get; }
        
        public NotFoundException()
        {

        }

        public NotFoundException(string message)
        {
            Message = message;
        }
    }
}