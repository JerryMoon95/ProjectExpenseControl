using System;
using ProjectExpenseControl.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProjectExpenseControl.CustomAuthentication
{
    public class CustomMembershipUser: MembershipUser
    {
        #region User Properties

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdArea { get; set; }
        public ICollection<Role> Roles { get; set; }

        #endregion

        public CustomMembershipUser(User user) 
            : base("CustomMembership", user.USR_DES_NAME, user.USR_IDE_USER, user.USR_DES_EMAIL, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.USR_IDE_USER;
            FirstName = user.USR_DES_FIRST_NAME;
            LastName = user.USR_DES_LAST_NAME;
            IdArea = user.USR_IDE_AREA;
            Roles = user.Roles;
        }
    }
}