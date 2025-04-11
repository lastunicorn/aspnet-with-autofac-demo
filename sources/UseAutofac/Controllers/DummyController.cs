using System.Web.Http;
using UseAutofac.Services;

namespace UseAutofac.Controllers
{
    public class DummyController : ApiController
    {
        private readonly IMyService myService;

        public DummyController(IMyService myService)
        {
            this.myService = myService ?? throw new System.ArgumentNullException(nameof(myService));
        }

        public string Get()
        {
            return myService.GetMessage();
        }
    }
}
