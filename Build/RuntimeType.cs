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
        public RuntimeType(string id) => _id = id;
        public void RegiterType(RuntimeInstance runtime, string id, Type type, Func<object> func)
        {
            if (_init)
                throw new Exception(string.Format("{0} is not registered (more than one constructors available)", Id));
            _runtime = runtime;
            _type = type;
            _id = _type.FullName;
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
