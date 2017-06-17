using System;

namespace Lab13
{
    public class KeyException: ArgumentException
    {
        public KeyException(): base("This key already exists")
        {
        }

        public KeyException(string message): base(message)
        {
        }
    }
}