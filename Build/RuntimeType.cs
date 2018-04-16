using System;

namespace Build
{
    class RuntimeType
    {
        bool _init;
        bool _guard;
        object _instance;
        Func<object> _func;
        RuntimeInstance _runtime;
        Type _type;
        string _id;
        public string Id { get { return _id; } }
        public bool IsRegistered { get { return _init; } }
        //public RuntimeType(string id) => _id = id;
        public Type Type => _type;
        public RuntimeType(Type type) { _id = type.FullName; _type = type; }
        public void RegisterType(RuntimeInstance runtime, string id, Type type, Func<object> func)
        {
            if (_init)
                throw new Exception(string.Format("{0} is not registered (more than one constructors available)", Id));
            _runtime = runtime;
            _id = id;
            _type = type;
            _func = func;
            _init = true;
        }
        public object CreateInstance()
        {
            object Evaluate()
            {
                if (_guard)
                    throw new Exception(string.Format("{0} is not evaluated (circular references found)", Id));
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
                        throw new Exception(string.Format("{0} is not evaluated (no instance)", Id));
                    return _instance;
            }
        }
    }
}
