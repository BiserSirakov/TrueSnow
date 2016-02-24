namespace TrueSnow.Services.Web.Tests
{
    using Contracts;

    using NUnit.Framework;

    [TestFixture]
    public class IdentifierProviderTests
    {
        private IIdentifierProvider provider;

        [TestFixtureSetUp]
        public void Init()
        {
            this.provider = new IdentifierProvider();
        }

        [Test]
        public void EncodingAndDecodingDoesntChangeTheId()
        {
            const int Id = 1337;
            var encoded = provider.EncodeId(Id);
            var actual = provider.DecodeId(encoded);
            Assert.AreEqual(Id, actual);
        }
    }
}
