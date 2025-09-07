using System;
using Application.Dtos.UserDtos;
using Application.Interfaces;

namespace Application.Services;

public class UserSessionManager : IUserSessionManager
{
    private const string CurrentUserKey = "CurrentUser";
    private readonly ISessionManager _sessionManager;

    public UserSessionManager(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public void SetCurrentUser(UserDto user)
    {
        _sessionManager.Set(CurrentUserKey, user);
    }

    public UserDto? GetCurrentUser()
    {
        return _sessionManager.Get<UserDto>(CurrentUserKey);
    }

}
