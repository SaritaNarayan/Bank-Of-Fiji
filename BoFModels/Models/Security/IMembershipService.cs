using System.Collections.Generic;
using System.Web.Security;

namespace BoF.Web.Models.Security
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        MembershipUserCollection GetAllUsers();
        IList<RegisterModel> GetAllUsers(MembershipUserCollection muc);
        bool DeleteUsers(string username);
        MembershipUser GetUser(string username);
    }
}