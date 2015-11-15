using MapThisTweet.DataProviders;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class LifecycleController : ApiController
    {
        [HttpGet, Route("api/operate/{start}")]
        public string Operate(bool start)
        {
            if (start)
            {
                TweetsRepository.Pause();
            }
            else
            {
                TweetsRepository.Resume();
            }

            return start ? "RESUMED" : "PAUSED";
        }

        [HttpGet, Route("api/demo/{fun}")]
        public string SetFun(bool fun)
        {
            TweetsController.IsFun = fun;
            return fun ? "FUN" : "GENERAL";
        }
    }
}
