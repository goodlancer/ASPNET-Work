using System.Collections.Generic;

namespace ScolptioCRMWebApi.ApplicationContext
{

    public interface ISecurityContextProvider
    {
        SecurityContext GetSecurityContext(IEnumerable<System.Security.Claims.Claim> claims);
    }

}
