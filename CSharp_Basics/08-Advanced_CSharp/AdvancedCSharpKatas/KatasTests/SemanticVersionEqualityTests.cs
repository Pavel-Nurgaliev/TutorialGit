namespace KatasTests
{
    public class SemanticVersionEqualityTests
    {
        [Test]
        public void SemanticVersionEquality_TwoVersions_ReturnsTrueEquality()
        {
            var firstSemanticVersion = new SemanticVersion(1, 2, 3);

            var secondSemanticVersion = new SemanticVersion(1, 2, 3);

            Assert.That(firstSemanticVersion.Equals(secondSemanticVersion), Is.True);
        }
        [Test]
        public void SemanticVersionEquality_TwoVersions_ReturnsFalseEquality()
        {
            var firstSemanticVersion = new SemanticVersion(1, 2, 3);

            var secondSemanticVersion = new SemanticVersion(2, 3, 4);

            Assert.That(firstSemanticVersion.Equals(secondSemanticVersion), Is.False);
        }
        [TestCaseSource(typeof(SemanticVersionData), nameof(SemanticVersionData.SemanticVersionCase))]
        public SemanticVersion[] VersionComparablity_VersionArray_ReturnsSortedVersions(SemanticVersion[] data)
        {
            data.Sort();

            return data;
        }
    }
}
