using NUnit.Framework;
using SwinAdventure;

namespace UnitTest
{
    [TestFixture()]
    public class IdentifiableObjectTest
    {
        // test - search
        [Test()]
        public void TestAreYou()
        {
            IdentifiableObject id = new IdentifiableObject(new string[] { "Bob", "george" });
            Assert.AreEqual(id.AreYou("bob"), true);
        }

        // test - search not found
        [Test()]
        public void TestNotAreYou()
        {
            IdentifiableObject id = new IdentifiableObject(new string[] { "bob", "george" });
            Assert.AreEqual(id.AreYou("paul"), false);
        }

        // test - cases should not matter
        [Test()]
        public void TestCaseSensitive()
        {
            IdentifiableObject id = new IdentifiableObject(new string[] { "bob", "gEorge" });
            Assert.AreEqual(id.AreYou("George"), true);
        }

        // test - receive the first id in the list
        [Test()]
        public void TestFirstID()
        {
            IdentifiableObject id = new IdentifiableObject(new string[] { "bob", "george" });
            Assert.AreEqual(id.FirstID, "bob");
        }

        // test - add id to the object
        [Test()]
        public void TestAddID()
        {
            IdentifiableObject id = new IdentifiableObject(new string[] { "bob", "george" });
            id.AddIdentifier("Karen");
            Assert.AreEqual(id.AreYou("karen"), true);
        }
    }
}