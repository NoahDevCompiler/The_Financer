using FinanceTool.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinanceTool.Data.Services
{
   
    public class UserDataService
    {
        private readonly AuthenticationStateProvider _authState;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;
        private readonly NavigationManager _navigation;

        public UserDataService(AuthenticationStateProvider authState,UserManager<User> userManager, ApplicationDbContext dbContext, SignInManager<User> signInManager, NavigationManager navigation) {
            _authState = authState;
            _userManager = userManager;
            _signInManager = signInManager;
            _navigation = navigation;
            _dbContext = dbContext;
        }

        public async Task<User?> GetCurrentUser() {
            var authState = await _authState.GetAuthenticationStateAsync();
            var userClaims = authState.User;
            if (!userClaims.Identity!.IsAuthenticated) {
                return null;
            }
            var userId = _userManager.GetUserId(userClaims);
            return await _dbContext.Users.FindAsync(userId);

        }
        public async Task<string?> GetUserId() {
            var user = await GetCurrentUser();
            return user?.Id;
        }
        public async Task LogOutAsync() {

            await _signInManager.SignOutAsync();
            _navigation.NavigateTo("/login", forceLoad: true);
        }
        
    }
}
