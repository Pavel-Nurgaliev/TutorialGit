namespace KatasTests
{
    public class PointPositionChangeTests
    {
        [TestCase(1, 1)]
        public void Moved_Twocoordinates_ReturnsNewPoint(int x, int y)
        {
            var point = new Point(x, y);

            Assert.That(point.Moved(2, 3), Is.EqualTo(new Point(3, 4)));
        }
    }
}
