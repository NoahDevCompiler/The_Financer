using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using FinanceTool.Models;

namespace FinanceTool.Data.Services
{
    public class UserDataService
    {
        private readonly AuthenticationStateProvider _authState;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public UserDataService(AuthenticationStateProvider authState,UserManager<User> userManager, ApplicationDbContext dbContext) {
            _authState = authState;
            _userManager = userManager;
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

    }
}
