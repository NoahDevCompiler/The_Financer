using FinanceTool.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceTool.Data.Services
{

    public class UserDataService
    {
        private readonly AuthenticationStateProvider _authState;
        private readonly UserManager<User> _userManager;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly SignInManager<User> _signInManager;
        private readonly NavigationManager _navigation;

        public UserDataService(
            AuthenticationStateProvider authState,
            UserManager<User> userManager, 
            IDbContextFactory<ApplicationDbContext> contextFactory, 
            SignInManager<User> signInManager, 
            NavigationManager navigation) 
            {
            _authState = authState;
            _userManager = userManager;
            _signInManager = signInManager;
            _navigation = navigation;
            _contextFactory = contextFactory;
        }

        public async Task<User?> GetCurrentUser() {
            using var context = _contextFactory.CreateDbContext();

            var authState = await _authState.GetAuthenticationStateAsync();
            var userClaims = authState.User;
            if (!userClaims.Identity!.IsAuthenticated) {
                return null;
            }
            var userId = _userManager.GetUserId(userClaims);
            return await context.Users.FindAsync(userId);
        }
        public async Task<string?> GetUserId() {
            var user = await GetCurrentUser();
            return user?.Id;
        }
        public async Task<decimal?> GetBalance() {
            var user = await GetCurrentUser();
            return user?.Balance;
        }
        public async Task SignInUser(User user) {

            await _signInManager.SignInAsync(user, isPersistent: false);
            _navigation.NavigateTo("/", forceLoad: true);
        }
        public async Task LogOutAsync() {

            await _signInManager.SignOutAsync();
            _navigation.NavigateTo("/login", forceLoad: true);
        }
        
    }
}
