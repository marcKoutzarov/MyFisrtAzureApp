using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public abstract class ResponseBase<T>
    {
        public bool Success { get; set; } = true;
        public int StatusCode { get; set; } = 200;
        public T Payload { get; set; }
    }
}
