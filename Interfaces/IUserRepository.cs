using HeroDatingApp.DTOs;
using HeroDatingApp.Entities;
using HeroDatingApp.Helpers;

namespace HeroDatingApp.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string userName);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParameters userparams);
        Task<MemberDto> GetMemberAsync(string userName);
        Task<string> GetUserGender(string username);
    }
}