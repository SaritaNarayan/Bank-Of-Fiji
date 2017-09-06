using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.Web.Models.Mappers
{
    public class UserProfileMapper
    {
        private readonly ISession _session;

        public UserProfileMapper(ISession session)
        {
            this._session = session;
        }
        public UserProfileModel UserProfileToUserProfileModel(UserProfile UserProfile)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var UserProfileModel = new UserProfileModel();

            UserProfileModel.ID = UserProfile.Id;
            UserProfileModel.Username = UserProfile.Username;
            UserProfileModel.Role = UserProfile.Role;
            UserProfileModel.Status = UserProfile.Status;

            return UserProfileModel;
        }

        public UserProfile UserProfileModelToUserProfile(UserProfileModel UserProfileModel)
        {
            var UserProfile = new UserProfile();

            UserProfile.Id = UserProfileModel.ID;
            UserProfile.Username = UserProfileModel.Username;
            UserProfile.Role = UserProfileModel.Role;
            UserProfile.Status = UserProfileModel.Status;

            return UserProfile;
        }

    }
}