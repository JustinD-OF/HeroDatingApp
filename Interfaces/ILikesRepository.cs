using HeroDatingApp.DTOs;
using HeroDatingApp.Entities;
using HeroDatingApp.Helpers;

namespace HeroDatingApp.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId); // returns the like between these users
        Task<AppUser> GetUserWithLikes(int userId);  // returns the user and within the object every like
        Task<PagedList<LikeDto>> GetUserLikes(LikesParameters likesParams);
    }
}