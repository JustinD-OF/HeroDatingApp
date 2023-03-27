using AutoMapper;
using HeroDatingApp.DTOs;
using HeroDatingApp.Entities;
using HeroDatingApp.Extensions;

namespace HeroDatingApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(destination => destination.PhotoUrl, 
                // ForMember tells automapper to which property of MemberDto we want to map
                    options => options.MapFrom(source => source.Photos.FirstOrDefault(x => x.IsMain).Url))
                    // Then in options we specify we want to map there from the user's main photo
                .ForMember(destination => destination.Age, 
                    options => options.MapFrom(source => source.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto,AppUser>();
            CreateMap<Message, MessageDto>()
            // Map all the main photos of senders
                .ForMember(destination => destination.SenderPhotoUrl,
                    options => options.MapFrom(source => source.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
            // Map the main photos of recipients
                .ForMember(destination => destination.RecipientPhotoUrl,
                    options => options.MapFrom(source => source.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
            CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null );

        }
        
    }
}