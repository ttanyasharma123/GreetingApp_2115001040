using System;

namespace Middleware_Layer.GlobalExceptionHandler
{
    
    [Serializable]
    public class CustomSerializableException : Exception
    {
        public CustomSerializableException(string message) : base(message) { }
    }

    
    public class CustomNonSerializableException : Exception
    {
        public CustomNonSerializableException(string message) : base(message) { }
    }
}
