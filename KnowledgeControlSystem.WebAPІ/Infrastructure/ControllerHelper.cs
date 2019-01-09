using System;
using System.Security.Claims;
using System.Security.Principal;

namespace KnowledgeControlSystem.WebAPІ.Infrastructure
{
    public static class ControllerHelper
    {
        public static int GetCurrentUserId(IPrincipal user)
        {
            return Convert.ToInt32(((ClaimsIdentity) user.Identity).FindFirst("Id").Value);
        }
    }
}