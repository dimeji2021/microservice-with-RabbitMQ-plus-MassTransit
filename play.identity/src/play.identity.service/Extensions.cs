using play.identity.service.Dtos;
using play.identity.service.Entities;

namespace play.identity.service
{
    public static class Extensions
    {
        public static UserDto AsDto(this ApplicationUser user)
        {
            return new UserDto(
                user.Id,
                user.UserName,
                user.Email,
                user.Gil,
                user.CreatedOn);
        }
    }
}