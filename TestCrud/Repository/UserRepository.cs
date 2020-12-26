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
        private readonly TestCrudDbContext testCrudDbContext;

        public UserRepository(TestCrudDbContext testCrudDbContext)
        {
            this.testCrudDbContext = testCrudDbContext;
        }

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

        private int GetSkillTypeId(string skillType)
        {
            return testCrudDbContext.TbSkillTypes.FirstOrDefault(st => st.Code.ToLower() == skillType.ToLower()).Id;
        }
    }
}
