using CustomAuthorization.Core;
using CustomAuthorization.Infrastructure;
using CustomAuthorization.Infrastructure.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorization.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class PolicyController : BaseController
    {
        [Authorize(Policy = Constants.PolicySyfy)]
        public string Syfy() => "You are able to see Syfy (policy)";

        [Authorize(Policy = Constants.PolicyTnt)]
        public string TnT() => "You are able to see Tnt (policy)";

        [Authorize(Policy = Constants.PolicySyfy)]
        [Authorize(Policy = Constants.PolicyTnt)]
        public string All() => "You are able to see all channels (policy)";
    }

}
