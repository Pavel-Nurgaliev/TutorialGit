using TraficLightStateMachine;

namespace KatasTests
{
    public class TraficLightStateMachineTests
    {
        [TestCase(Light.Red, Light.Green)]
        [TestCase(Light.Green, Light.Yellow)]
        [TestCase(Light.Yellow, Light.Red)]
        public void Next_LightEnumValue_ReturnsNextLightEnumValue(Light current, Light expected)
        {
            var nextLight = LightHelper.Next(current);

            Assert.That(nextLight, Is.EqualTo(expected));
        }

        [Test]
        public void CanWrite_PermissionsWithWrite_ReturnsTrue()
        {
            var p = Permissions.Read | Permissions.Write;

            Assert.That(PermissionsHelper.CanWrite(p), Is.True);
        }

        [Test]
        public void HasFlag_PermissionsWithoutExecute_ReturnsFalse()
        {
            var p = Permissions.Read | Permissions.Write;

            Assert.That(p.HasFlag(Permissions.Execute), Is.False);
        }
    }
}
