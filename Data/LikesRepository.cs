using HeroDatingApp.DTOs;
using HeroDatingApp.Entities;
using HeroDatingApp.Extensions;
using HeroDatingApp.Helpers;
using HeroDatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroDatingApp.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParameters likesParams) // userId can be the source user or the target user, depending on the predicate
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (likesParams.Predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                users = likes.Select(like => like.TargetUser);
            }
            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.TargetUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }

            var likedUsers = users.Select(user => new LikeDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Age = user.DateOfBirth.CalculateAge(),
                KnownAs = user.KnownAs,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City
                
            });

            return await PagedList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(user => user.LikedUsers)
                .FirstOrDefaultAsync(user => user.Id == userId);
        }
    }
}