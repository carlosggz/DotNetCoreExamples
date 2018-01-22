using CustomAuthorization.Infrastructure.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorization.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class RolesController : BaseController
    {
        [Authorize(Roles = "Syfy")]
        public string Syfy() => "You are able to see Syfy (roles)";

        [Authorize(Roles = "Tnt")]
        public string TnT() => "You are able to see Tnt (roles)";

        [Authorize(Roles = "Syfy")]
        [Authorize(Roles = "Tnt")]
        public string All() => "You are able to see all channels (roles)";
    }
}
