using CustomAuthorization.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomAuthorization.Infrastructure
{
    public class ChannelRequirement : IAuthorizationRequirement
    {
        public MoviesChannels Channel { get; private set; }

        public ChannelRequirement(MoviesChannels channel)
        {
            Channel = channel;
        }
    }

    public class ChannelAudienceHandler : AuthorizationHandler<ChannelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ChannelRequirement requirement)
        {
            if (requirement.Channel == MoviesChannels.None ||
                context.User.Claims.Any(x => x.Type == Constants.MoviesClaim && x.Value == requirement.Channel.ToString()))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
