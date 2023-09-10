using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Database.Models;
using Pustok.Services.Abstracts;

namespace Pustok.Services.Concretes;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PustokDbContext _pustokDbContext;
    private User _currentUser;

    public UserService(IHttpContextAccessor httpContextAccessor, PustokDbContext pustokDbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _pustokDbContext = pustokDbContext;
    }

    public bool IsCurrentUserAuthenticated()
    {
        return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public User CurrentUser
    {
        get {

            if (_currentUser != null)
            {
                return _currentUser;
            }

            if (_httpContextAccessor.HttpContext.User == null)
            {
                throw new Exception("User is not authenticated");
            }

            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim is null)
            {
                throw new Exception("User is not authenticated");
            }

            var userId = Convert.ToInt32(userIdClaim.Value);
            var user = _pustokDbContext.Users.SingleOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new Exception("User not found in system");
            }

            _currentUser = user;

            return _currentUser;
        }
    }

    public string GetCurrentUserFullName()
    {
        return $"{CurrentUser.Name} {CurrentUser.LastName}";
    }
}
