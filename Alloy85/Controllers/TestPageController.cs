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

        //public string GlobalPageLevel1 { get; set; }
        //public string GlobalPageLevel2 { get; set; }
        //public string GlobalPageLevel3 { get; set; }
    }

    public class TestPageController : PageControllerBase<TestPage>
    {
        private readonly PageRouteHelper _pageRouteHelper;
        private readonly ContentRouteHelper _contentRouteHelper;
        private readonly UrlResolver _urlResolver;

        public TestPageController(PageRouteHelper pageRouteHelper, ContentRouteHelper contentRouteHelper, UrlResolver urlResolver)
        {
            _pageRouteHelper = pageRouteHelper;
            _contentRouteHelper = contentRouteHelper;
            _urlResolver = urlResolver;
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

            var model = new TestPageViewModel((TestPage) page)
            {
                //GlobalPageLevel1 = urlResolver.GetUrl(new ContentReference(6)),
                //GlobalPageLevel2 = urlResolver.GetUrl(new ContentReference(7)),
                //GlobalPageLevel3 = urlResolver.GetUrl(new ContentReference(8))
            };

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