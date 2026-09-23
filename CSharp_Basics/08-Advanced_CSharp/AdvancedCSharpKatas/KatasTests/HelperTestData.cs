namespace KatasTests
{
    public class HelperTestData()
    {
        public static IEnumerable<TestCaseData> IntArrayForMaxTesting
        {
            get
            {
                yield return new TestCaseData((object)new[] { 3, 7, 2 }).Returns(7);
            }
        }
        public static IEnumerable<TestCaseData> StringArrayForMaxTesting
        {
            get
            {
                yield return new TestCaseData((object)new[] { "apple", "pear", "fig" }).Returns("pear");
            }
        }
        public static IEnumerable<TestCaseData> SVArrayForMaxTesting
        {
            get
            {
                yield return new TestCaseData((object)new[] { new SemanticVersion(1,0,0)
                                                    , new SemanticVersion(2,1,0)
                                                    }).Returns(2);
            }
        }
    }
}
