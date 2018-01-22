using CustomAuthorization.Infrastructure;
using CustomAuthorization.Infrastructure.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorization.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class ClaimsController : BaseController
    {
        [Authorize(Policy = Constants.ClaimSyfy)]
        public string Syfy() => "You are able to see Syfy (claims)";

        [Authorize(Policy = Constants.ClaimTnt)]
        public string TnT() => "You are able to see Tnt (claims)";

        [Authorize(Policy = Constants.ClaimSyfy)]
        [Authorize(Policy = Constants.ClaimTnt)]
        public string All() => "You are able to see all channels (claims)";
    }
}
