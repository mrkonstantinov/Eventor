namespace Eventor.Services.Identity.API.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string City { get; set; }
        public string Region { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        // 0 - Unknown
        // 1 - Male
        // 2 - Female
        [Required]
        public int Gender { get; set; }
    }
}
