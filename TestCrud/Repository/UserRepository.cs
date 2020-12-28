using TestCrud.Repository;
using TestCrud.Models.Response;
using TestCrud.Models;
using TestCrud.Models.DBModels;
using System.Collections.Generic;
using TestCrud.Repository.DatabaseContext;
using TestCrud.Shared.Constants;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestCrud.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Fields

        private readonly TestCrudDbContext testCrudDbContext;

        #endregion

        #region Constructor
        public UserRepository(TestCrudDbContext testCrudDbContext)
        {
            this.testCrudDbContext = testCrudDbContext;
        }
        #endregion

        #region Public Methods
        public GenericResponse SaveUser(UserProfile userProfile)
        {
            GenericResponse response = new GenericResponse();
            var dbProfile = testCrudDbContext.TbUsers.FirstOrDefault(t => t.Id == userProfile.Id);
            var skillTypeId = GetSkillTypeId(userProfile.SkillType);

            if(dbProfile == null)
            {
                dbProfile = new TbUser()
                {
                    Id = userProfile.Id
                };
                testCrudDbContext.TbUsers.Add(dbProfile);
            }
            dbProfile.Name = userProfile.Name;
            dbProfile.Designation = userProfile.Designation;
            dbProfile.Dob = userProfile.DOB;
            dbProfile.SkillId = skillTypeId;
            dbProfile.IsArchive = false;

            var savedSuccessfully = testCrudDbContext.SaveChanges() > 0;
            response.Error = !savedSuccessfully;
            response.Message = savedSuccessfully ? Constants.ProfileSavedSuccess : Constants.ErrorMessage;
            return response;
        }

        public UserResponse GetUser(string userId)
        {
            var user = (from userProfile in testCrudDbContext.TbUsers
                        where userProfile.Id == userId && !userProfile.IsArchive
                        select new UserResponse
                        {
                            Id = userProfile.Id,
                            Name = userProfile.Name,
                            Designation = userProfile.Designation,
                            SkillType = userProfile.Skill.Code
                        }).FirstOrDefault();
            return user;
        }

        public List<UserResponse> GetUsers()
        {
            var users = (from userProfile in testCrudDbContext.TbUsers
                         where !userProfile.IsArchive
                         select new UserResponse
                         {
                            Id = userProfile.Id,
                            Name = userProfile.Name,
                            Designation = userProfile.Designation,
                            DOB = userProfile.Dob,
                            SkillType = userProfile.Skill.Code
                         }).ToList();
            return users;
        }

        public GenericResponse DeleteUser(string userId)
        {
            var response = new GenericResponse();
            var user = testCrudDbContext.TbUsers.FirstOrDefault(t => t.Id == userId);
            user.IsArchive = true;
            var savedSuccessfully = testCrudDbContext.SaveChanges() > 0;
            response.Error = !savedSuccessfully;
            response.Message = savedSuccessfully ? Constants.ProfileDeletedSuccess : Constants.ErrorMessage;
            return response;
        }

        public List<SelectItem> GetSkillType()
        {
            return (from skillType in testCrudDbContext.TbSkillTypes
                where !skillType.IsArchive
                select new SelectItem
                {
                    Id = skillType.Id,
                    Code = skillType.Code,
                    DisplayName = skillType.Display
                }).ToList();
        }
        #endregion

        #region Private Methods
        private int GetSkillTypeId(string skillType)
        {
            return testCrudDbContext.TbSkillTypes.FirstOrDefault(st => st.Code.ToLower() == skillType.ToLower()).Id;
        }
        #endregion
    }
}
