namespace TrueSnow.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using NUnit.Framework;

    using TrueSnow.Data.Models;
    using TrueSnow.Services.Data.Contracts;

    [TestFixture]
    public class ArticlesServiceTests
    {
        private Mock<IArticlesService> articlesServiceMock;

        [TestFixtureSetUp]
        public void Init()
        {
            this.articlesServiceMock = new Mock<IArticlesService>();
            var articles = new List<Article>().AsQueryable();
            
            articlesServiceMock
                .Setup(s => s.GetAll())
                .Returns(articles);

            articlesServiceMock
                .Setup(s => s.GetById("5"))
                .Returns(new Article { Id = 5 });
            
            articlesServiceMock
                .Setup(s => s.Add(It.IsAny<Article>()))
                .Verifiable();
        }

        [Test]
        public void GetAllArticlesShouldNotReturnNull()
        {
            var articles = articlesServiceMock.Object.GetAll();
            Assert.AreNotEqual(null, articles);
        }

        [Test]
        public void GetByIdArticlesShouldNotReturnNull()
        {
            Article article = articlesServiceMock.Object.GetById("5");
            Assert.AreEqual(5, article.Id);
        }

        [Test]
        public void AddArticleShouldWorkCorrectly()
        {
            articlesServiceMock.Object.Add(new Article());
            articlesServiceMock.Verify(s => s.Add(It.IsAny<Article>()));
        }
    }
}
