using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Alloy85.Models.Pages;
using Alloy85.Models.ViewModels;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace Alloy85.Controllers
{
    public class TestPageViewModel : PageViewModel<TestPage>
    {
        public TestPageViewModel(TestPage currentPage)
            : base(currentPage)
        { }
    }

    public class TestPageController : PageControllerBase<TestPage>
    {
        private readonly PageRouteHelper _pageRouteHelper;
        private readonly ContentRouteHelper _contentRouteHelper;

        public TestPageController(PageRouteHelper pageRouteHelper, ContentRouteHelper contentRouteHelper)
        {
            _pageRouteHelper = pageRouteHelper;
            _contentRouteHelper = contentRouteHelper;
        }

        public async Task<ActionResult> Index(TestPage currentPage)
        {
            var temp = await TestAsyncMethod();
            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            var contentRouteHelper = ServiceLocator.Current.GetInstance<ContentRouteHelper>();
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();

            var page = pageRouteHelper.Page; // throws NRE
            // var page = _pageRouteHelper.Page; // works fine
            // var page = contentRouteHelper.Content; // throws NRE
            // var page = _contentRouteHelper.Content; // works fine
            // var page = currentPage; // works fine

            var model = new TestPageViewModel((TestPage) page);

            return View(model);
        }

        public async Task<string> TestAsyncMethod()
        {
            //return Task.FromResult("Hello"); // works fine
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync("http://google.com");
            }
        }
    }
}