using System;

namespace Build.Generics
{
    public interface IRuntimeAttributeProvider<out T, in V> where T : Attribute, IRuntimeAttribute
    {
        T GetAttribute(V prop, Type type, bool inherit = false);
    }
}