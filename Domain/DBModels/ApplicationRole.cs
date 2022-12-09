using AspNetCore.Identity.MongoDbCore.Models;

namespace Domains.DBModels
{
    public class ApplicationRole : MongoIdentityRole<string>
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
