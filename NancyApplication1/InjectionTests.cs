using System.Threading.Tasks;

using Nancy.Testing;

using NSubstitute;

using NUnit.Framework;

namespace NancyApplication1
{
    [TestFixture]
    public class InjectionTests
    {

        [Test]
        public void Nsub_Async_GetCalled()
        {

            this.task.GetValueAsync().Returns(c => Task.Run(() => "i did not fail"));

            var browser = new Browser(with => with.Dependency(this.task).Module<IndexModule>());
            var actual = browser.Get("/async");
            Assert.That(actual.Body.AsString(), Is.EqualTo("i did not fail"));
        }


        [Test]
        public void Nsub_Async_Object_GetCalled()
        {
            this.task.GetObjectAsync().Returns(
                c => Task.Run(() => new ValueObject { Text = "i did not fail", Other = new ValueObject { Text = "failz" } }));

            var browser = new Browser(with => with.Dependency(this.task).Module<IndexModule>());
            var actual = browser.Get("/asyncobj");
            Assert.That(actual.Body.AsString(), Is.StringContaining("i did not fail"));
        }


        [Test]
        public void Nsub_WithBootstrapper_Async_GetCalled()
        {
            this.task.GetValueAsync().Returns(c => Task.Run(() => "i did not fail"));
            var bs = new ConfigurableBootstrapper(
                with => { with.Dependency(this.task).Module<IndexModule>(); });
            var browser = new Browser(bs);
            var actual = browser.Get("/async");
            Assert.That(actual.Body.AsString(), Is.EqualTo("i did not fail"));
        }


        [Test]
        public void Nsub_WithBootstrapper_GetCalled()
        {
            this.task.GetValue().Returns("i did not fail");
            var bs = new ConfigurableBootstrapper(
                with => { with.Dependency(this.task).Module<IndexModule>(); });
            var browser = new Browser(bs);
            var actual = browser.Get("/");
            Assert.That(actual.Body.AsString(), Is.EqualTo("i did not fail"));
        }


        [Test]
        public void NsubWithOnlyBrowserGetCalled()
        {
            this.task.GetValue().Returns("i did not fail");
            var browser = new Browser(cfg => cfg.Dependency(this.task).Module<IndexModule>());
            var actual = browser.Get("/");
            Assert.That(actual.Body.AsString(), Is.EqualTo("i did not fail"));
        }


        public InjectionTests()
        {

            this.task = Substitute.For<ITask>();

        }


        private ITask task;
    }
}