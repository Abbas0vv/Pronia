using Pustok.Database.Models;

namespace Pustok.Services.Abstracts;

public interface IUserService
{
    public User CurrentUser { get; }

    string GetCurrentUserFullName();
    bool IsCurrentUserAuthenticated();

}
