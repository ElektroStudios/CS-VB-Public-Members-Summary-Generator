
// Test public class with an inner interface.
// The generator should include the public members of the inner interface in the summary.

#pragma warning disable // Disable all warnings in this file to test the generator.

public class TestClassPublic
{
    public interface TestInterfacePublic
    {
        void TestMethodPublicNoModifier();

        new void TestMethodShadows();

        new void TestMethodOverloads();
    }
}

#pragma warning restore  // Re-enable all warnings.