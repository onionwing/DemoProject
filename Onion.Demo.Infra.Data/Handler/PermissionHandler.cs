using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Onion.Demo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Data.Handler
{
    public class PermissionRequirement : IAuthorizationRequirement { }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IPermissionService _permissionService;

        public PermissionHandler(IHttpContextAccessor accessor, IPermissionService permissionService)
        {
            _accessor = accessor;
            _permissionService = permissionService;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var http = _accessor.HttpContext!;
            var path = http.Request.Path.Value!;
            var method = http.Request.Method;

            var roles = context.User?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? Array.Empty<string>();
            if (await _permissionService.UserHasAccessAsync(path, method, roles))
                context.Succeed(requirement);
            else
                context.Fail();
        }
    }
}
