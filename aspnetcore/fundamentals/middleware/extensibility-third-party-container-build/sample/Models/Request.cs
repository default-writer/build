using System;

namespace MiddlewareExtensibilitySample.Models
{
    public class Request
    {
        public DateTime DT { get; set; }
        public int Id { get; set; }
        public string MiddlewareActivation { get; set; }
        public string Value { get; set; }
    }
}