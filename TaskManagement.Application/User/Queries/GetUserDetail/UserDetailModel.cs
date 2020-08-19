using System;
using System.Linq.Expressions;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.User.Queries.GetUserDetail
{
    public class UserDetailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public static Expression<Func<AppUser, UserDetailModel>> Projection
        {
            get
            {
                return user => new UserDetailModel
                {
                   FirstName = user.FirstName,
                   LastName = user.LastName
                };
            }
        }

        public static UserDetailModel Create(AppUser user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}