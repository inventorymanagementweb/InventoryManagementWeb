using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace InventoryManagement.Identity.Model
{
    public class UserReturn
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}
