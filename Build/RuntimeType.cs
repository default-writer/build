using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    public interface IRuntimeType
    {
        string Id { get; }
        IRuntimeType[] Parameters { get; }
        object CreateInstance(object[] args);
    }
    class RuntimeType : IRuntimeType
    {
        readonly string _id;
        readonly Type _type;
        bool _init;
        bool _guard;
        object _instance;
        Func<object> _func;
        RuntimeInstance _runtime;
        RuntimeType[] _args;
        public string Id => _id;
        public Type Type => _type;
        public bool IsRegistered => _init;
        public IRuntimeType[] Parameters => _args;
        public RuntimeType(Type type) { _id = type.FullName; _type = type; }
        public void RegisterType(RuntimeInstance runtime, RuntimeType[] args)
        {
            if (_init)
                throw new TypeInjectionException(string.Format("{0} is not registered (more than one constructor available)", _id));
            _runtime = runtime;
            _args = args;
            _func = Evaluate;
            _init = true;
        }
        object Evaluate() => Activator.CreateInstance(_type, _args.Select(p => p.CreateInstance()).ToArray());
        object Call() => Activator.CreateInstance(_type, _args.Select(p => p._instance).ToArray());
        public object CreateInstance(params object[] args)
        {
            if (args.Length != _args.Length)
                throw new TypeInitializationException(string.Format("{0} is not instantiated (parameters count mismatch)", _id));
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] != null && args[i].GetType() != _args[i].Type)
                    throw new TypeInitializationException(string.Format("{0} is not instantiated (parameter type mismatch)", _id));
                _args[i]._instance = args[i];
            }
            return Call();
        }
        public object CreateInstance()
        {
            object Evaluate()
            {
                if (_guard)
                    throw new TypeInitializationException(string.Format("{0} is not instantiated (circular references found)", _id));
                _guard = true;
                object result = _func();
                _guard = false;
                return result;
            }
            switch (_runtime)
            {
                case RuntimeInstance.Singleton:
                    if (!_guard)
                    {
                        _instance = Evaluate();
                        _guard = true;
                    }
                    return _instance;
                case RuntimeInstance.CreateInstance:
                    return Evaluate();
                case RuntimeInstance.None:
                default:
                    if (_func != null)
                        throw new TypeInitializationException(string.Format("{0} is not instantiated (constructor not allowed)", _id));
                    return _instance;
            }
        }
    }
}
