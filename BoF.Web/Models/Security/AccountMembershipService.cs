using System;
using System.Collections.Generic;
using System.Web.Security;

namespace BoF.Web.Models.Security
{
    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            this.provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");

            return provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email))
                throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public MembershipUser GetUser(string username)
        {
            return provider.GetUser(username, true);
        }

        public MembershipUserCollection GetAllUsers()
        {
            int totalusers;
            return provider.GetAllUsers(0, 1000, out totalusers);
        }

        public IList<RegisterModel> GetAllUsers(MembershipUserCollection muc)
        {
            var roleService = new RoleService();
            IList<RegisterModel> registerModels = new List<RegisterModel>();
            foreach (var m in muc)
            {
                var roles = roleService.GetUserRole(m.ToString());
                if (roles.Count > 0)
                {
                    foreach (var role in roles)
                    {
                        var registerModel = new RegisterModel
                                                {
                                                    UserName = role.UserName,
                                                    RoleModelName = role.RoleModelName,
                                                    Email = role.Email
                                                };
                        var membershipUser = GetUser(role.UserName);
                        registerModel.Approved = membershipUser.IsApproved;
                        registerModel.LockedOut = membershipUser.IsLockedOut;
                        registerModels.Add(registerModel);
                    }
                }
                else
                {
                    var registerModel = new RegisterModel
                                            {
                                                UserName = m.ToString(),
                                                RoleModelName = ""
                                                
                                            };
                    var membershipUser = GetUser(m.ToString());
                    registerModel.Approved = membershipUser.IsApproved;
                    registerModel.LockedOut = membershipUser.IsLockedOut;
                    registerModels.Add(registerModel);
                }
            }

            return registerModels;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword))
                throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword))
                throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        public bool DeleteUsers(string username)
        {
            return provider.DeleteUser(username, true);
        }
    }
}