namespace KatasTests
{
    public class SemanticVersionData
    {
        public static IEnumerable<TestCaseData> SemanticVersionCase
        {
            get
            {
                yield return new TestCaseData(
                                 (object)new SemanticVersion[] {
                                                         new(2,0,0)
                                                       , new(1,5,0)
                                                       , new(1,5,3)
                                                       , new(1,0,0)
                                                       }
                                 ).Returns(new SemanticVersion[] {
                                                                   new(1, 0, 0)
                                                                 , new(1, 5, 0)
                                                                 , new(1, 5, 3)
                                                                 , new(2, 0, 0)
                                                                 });
            }
        }
    }
}
