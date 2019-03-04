using System;
using System.Reflection;

namespace Build
{
    public interface IRuntimeAttributeProvider<out T> where T : IRuntimeAttribute
    {
        T GetAttribute(ICustomAttributeProvider p, Type type, bool inherit = false);
    }
}