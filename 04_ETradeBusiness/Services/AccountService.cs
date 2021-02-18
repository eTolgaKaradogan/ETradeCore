using ETradeBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ETradeDataAccess.EntityFramework.Repositories.Bases;
using ETradeEntities.Entities;

namespace ETradeBusiness.Services.Bases
{
    public class AccountService : IAccountService
    {
        private readonly UserRepositoryBase _userRepository;

        public AccountService(UserRepositoryBase userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(UserModel model, bool saveChanges = true)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.ConfirmPassword))
                {
                    if (model.Password == model.ConfirmPassword)
                    {
                        var user = _userRepository.GetEntityQuery(u => u.UserName.ToUpper() == model.UserName.ToUpper() || u.UserDetail.Email.ToUpper() == model.UserDetail.Email.ToUpper(), "UserDetail").SingleOrDefault();
                        //var email = _userRepository.GetEntity(u =>
                        //    u.UserDetail.Email.ToUpper() == model.UserDetail.Email.ToUpper());
                        if (user == null)
                        {
                            var entity = new User()
                            {
                                Active = true,
                                Guid = Guid.NewGuid().ToString(),
                                Password = model.Password,
                                RoleId = 2, // User rolü, enum ile yapmak daha uygun olur
                                UserName = model.UserName,
                                UserDetail = new UserDetail()
                                {
                                    Adress = model.UserDetail.Adress,
                                    CityId = model.UserDetail.CityId,
                                    CountryId = model.UserDetail.CountryId,
                                    Email = model.UserDetail.Email,
                                    Guid = Guid.NewGuid().ToString()
                                }
                            };
                            _userRepository.AddEntity(entity);
                            if (saveChanges)
                            {
                                SaveChanges();
                            }
                        }
                    }
                }
                
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Delete(int id, bool saveChanges = true)
        {
            try
            {
                _userRepository.DeleteEntity(id);
                if (saveChanges)
                {
                    SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public UserModel GetById(int id)
        {
            try
            {
                return GetQuery().SingleOrDefault(u => u.Id == id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public IQueryable<UserModel> GetQuery(Expression<Func<UserModel, bool>> predicate = null)
        {
            try
            {
                return _userRepository.GetEntityQuery("Role", "UserDetail").Select(user => new UserModel()
                {
                    Active = user.Active,
                    Guid = user.Guid,
                    Id = user.Id,
                    Password = user.Password,
                    RoleId = user.RoleId,
                    UserDetailId = user.UserDetailId,
                    UserName = user.UserName,
                    Role = new RoleModel() // user ile rolemodel ilişkili olduğu için role'ün new RoleModel'e newlenmesi gerekiyor...
                    {
                        Guid = user.Role.Guid,
                        Id = user.Role.Id,
                        RoleName = user.Role.RoleName
                    },
                    UserDetail = new UserDetailModel()
                    {
                        Adress = user.UserDetail.Adress,
                        CityId = user.UserDetail.CityId,
                        CountryId = user.UserDetail.CountryId,
                        Email = user.UserDetail.Email,
                        Id = user.UserDetail.Id,
                        Guid = user.UserDetail.Guid
                    }
                });
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return _userRepository.SaveChanges();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(UserModel model, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
