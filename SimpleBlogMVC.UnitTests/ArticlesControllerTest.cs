using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleBlogMVC.UnitTests
{
    [TestClass]
    class ArticlesControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            Assert.AreEqual("ArticlesController","ArticlesController");
        }
    }
}
