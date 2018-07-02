namespace Build.Tests.TestSet17
{
    public class SubType
    {
    }

    public class Type
    {
        public Type(SubType subType) => SubType = subType;

        public SubType SubType { get; }
    }
}