using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class UserWithRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAdress { get; set; }
        public string Role { get; set; }
    }
}