using System;

namespace MiddlewareExtensibilitySample.Middleware
{
    public class Lazy<T>
    {
        public Lazy(Func<T> func) => Func = func;

        Func<T> Func { get; }

        public T GetInstance() => Func();
    }
}