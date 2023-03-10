using HeroDatingApp.Extensions;

namespace HeroDatingApp.Entities
{
    public class AppUser
    {
        public int Id {get; set;}
        public string UserName {get; set;}
        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }
        public string Gender {get; set;}
        public string Power {get;set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
        public DateOnly DateOfBirth {get; set;}
        public string KnownAs {get; set;}
        public DateTime Created {get; set;} = DateTime.UtcNow;
        public DateTime LastActive {get; set;} = DateTime.UtcNow;
        public string AboutMe {get; set;}
        public string LookingFor {get; set;}
        public string Interests {get; set;}
        public string Country {get; set;}
        public string City {get; set;}
        public string Address {get; set;}
        public List<Photo> Photos {get; set;} = new List<Photo>();

    }
}