using Microsoft.AspNetCore.Identity;


namespace ASPNetCoreIdentityCustomFields.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
