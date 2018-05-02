using System;
namespace Kernel
{
    public class NoBindingException : InvalidOperationException
    {
        public NoBindingException()         
        {
        }
        public NoBindingException(string message) :base(message)
        {
            
        }
    }
}
