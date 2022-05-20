using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Helpers
{
    public static class AuthHelper
    {
        public static bool IsNotLoggedIn(HttpContext context) //true = not logged in
              // if true redirect                                               
        {
            if (!context.Session.Keys.Any(c => c.Equals("Token")))
            {
                return true;
            }
            return false;
        }
    }
}
