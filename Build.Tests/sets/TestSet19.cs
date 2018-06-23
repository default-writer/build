using System;

namespace Build.Tests.TestSet19
{
    public class Class1
    {
    }

    public class Class2
    {
        readonly Func<Class1> _class1;

        public Class2(Func<Class1> class1) => _class1 = class1;
    }
}