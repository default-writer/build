using System;

namespace Build
{
    public interface ITypeFilter
    {
        bool CanCreate(Type type);

        bool CanRegister(Type type);
    }
}