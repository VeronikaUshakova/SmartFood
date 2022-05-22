using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
