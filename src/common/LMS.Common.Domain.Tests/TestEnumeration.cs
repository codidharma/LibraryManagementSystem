namespace LMS.Common.Domain.Tests;

public class TestEnumeration : Enumeration
{
    public static TestEnumeration Test1 = new(1, "Test1");
    public static TestEnumeration Test2 = new(1, "T2st2");
    public TestEnumeration(int id, string name) : base(id, name)
    {
    }
}
