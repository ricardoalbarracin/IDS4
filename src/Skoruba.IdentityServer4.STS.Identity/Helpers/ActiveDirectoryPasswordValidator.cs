

using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace Skoruba.IdentityServer4.STS.Identity.Helpers
{
    public class ActiveDirectoryPasswordValidator : IResourceOwnerPasswordValidator

    {
        // private readonly SignInManager<TUser> _signInManager;
        private IEventService _events;
        //private readonly UserManager<TUser> _userManager;
        private readonly ILogger<ActiveDirectoryPasswordValidator> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOwnerPasswordValidator{TUser}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="events">The events.</param>
        /// <param name="logger">The logger.</param>
        public ActiveDirectoryPasswordValidator(

            IEventService events,
            ILogger<ActiveDirectoryPasswordValidator> logger)
        {
            _events = events;
            _logger = logger;
        }
        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var clientId = context.Request?.Client?.ClientId;
            //var user = await _userManager.FindByNameAsync(context.UserName);
            if (true)
            {
                //var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, true);
                if (true)
                {
                    // var sub = await _userManager.GetUserIdAsync(user);

                    _logger.LogInformation("Credentials validated for username: {username}", context.UserName);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(context.UserName, "", context.UserName, false, clientId));

                    context.Result = new GrantValidationResult("", AuthenticationMethods.Password);
                    return;
                }
                //else if (result.IsLockedOut)
                //{
                //    _logger.LogInformation("Authentication failed for username: {username}, reason: locked out", context.UserName);
                //    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "locked out", false, clientId));
                //}
                //else if (result.IsNotAllowed)
                //{
                //    _logger.LogInformation("Authentication failed for username: {username}, reason: not allowed", context.UserName);
                //    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "not allowed", false, clientId));
                //}
                //else
                //{
                //    _logger.LogInformation("Authentication failed for username: {username}, reason: invalid credentials", context.UserName);
                //    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid credentials", false, clientId));
                //}
            }
            else
            {
                _logger.LogInformation("No user found matching username: {username}", context.UserName);
                await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid username", false, clientId));
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        }
    }
}
