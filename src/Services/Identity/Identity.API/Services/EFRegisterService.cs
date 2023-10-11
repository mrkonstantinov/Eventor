namespace Identity.API.Services
{
    public class EFRegisterService : IRegisterService<ApplicationUser>
    {
        private UserManager<ApplicationUser> _userManager;

        public EFRegisterService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
        }

        public Task CreateAsync(ApplicationUser user)
        {
            return _userManager.CreateAsync(user);
        }
    }
}
