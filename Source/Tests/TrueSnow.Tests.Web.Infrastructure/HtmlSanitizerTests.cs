namespace TrueSnow.Tests.Web.Infrastructure
{
    using NUnit.Framework;
    using TrueSnow.Web.Infrastructure;

    [TestFixture]
    public class HtmlSanitizerTests
    {
        [Test]
        public void HtmlSanitizerSanitizesCorrectly()
        {
            var sanitizer = new HtmlSanitizerAdapter();
            var html = @"<script>alert('xss')</script><div onload=""alert('xss')"""
                + @"style=""background-color: test"">Test<img src=""test.gif"""
                + @"style=""background-image: url(javascript:alert('xss')); margin: 10px""></div>";

            var sanitized = sanitizer.Sanitize(html);
            Assert.That(sanitized, Is.EqualTo(@"<div style=""background-color: test"">"
                + @"Test<img src=""test.gif"" style=""margin: 10px""></div>"));
        }
    }
}
