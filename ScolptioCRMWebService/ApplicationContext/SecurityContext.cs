using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ScolptioCRMWebApi.ApplicationContext
{
    public class SecurityContext
    {
        public string UserName { get; }
        public string UserId { get; }
        public string Email { get; }
        public string OrgId { get; }
        public List<string> Roles { get; }
        public List<string> Permission { get; }
        public string OrgName { get; }
        public string DisplayName { get; }


        public ClaimsIdentity claims { get; set; }
        public SecurityContext(IEnumerable<Claim> claims) : this(new ClaimsIdentity(from claimKeyValuePair in claims
                                                                                    select new Claim(claimKeyValuePair.Type, claimKeyValuePair.Value)))
        {

        }

        public SecurityContext(ClaimsIdentity claimsIdentity)
        {
            UserName = (claimsIdentity.HasClaim((Claim c) => c.Type == "UserName") ? claimsIdentity.Claims.First((Claim c) => c.Type == "UserName").Value.ToLower() : string.Empty);
            Email = (claimsIdentity.HasClaim((Claim c) => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress") ? claimsIdentity.Claims.First((Claim c) => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToLower() : string.Empty);
            Roles = JsonConvert.DeserializeObject<List<string>>(claimsIdentity.Claims.First((Claim c) => c.Type == "Roles").Value);
            Permission = JsonConvert.DeserializeObject<List<string>>(claimsIdentity.Claims.First((Claim c) => c.Type == "Permissions").Value);
            UserId = claimsIdentity.Claims.First((Claim c) => c.Type == "UserId").Value;
            OrgName = claimsIdentity.Claims.First((Claim c) => c.Type == "OrgName").Value;
            OrgId = claimsIdentity.Claims.First((Claim c) => c.Type == "OrgId").Value;
            DisplayName = claimsIdentity.Claims.First((Claim c) => c.Type == "DisplayName").Value;
            claims = claimsIdentity;
        }

    }
}
