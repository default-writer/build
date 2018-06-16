namespace Build.Interfaces.Tests
{
    interface IMyFunRuleSet
    {
        Type1 Rule(Arg1 arg1, Arg2 arg2);
    }

    class Arg1
    {
    }

    class Arg2
    {
    }

    class Type1
    {
        public Type1(Arg1 arg1, Arg2 arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }

        public Arg1 Arg1 { get; }

        public Arg2 Arg2 { get; }
    }
}