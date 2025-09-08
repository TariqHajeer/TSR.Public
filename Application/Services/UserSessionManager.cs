using System;
using Application.Dtos.UserDtos;
using Application.Interfaces;

namespace Application.Services;

public class UserSessionManager : IUserSessionManager
{
    private const string CurrentUserKey = "CurrentUser";
    private const string NeedOTp = "NeedOTp";
    private readonly ISessionManager _sessionManager;

    public UserSessionManager(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }
    public void SetCurrentUser(SessionUserDto user)
    {
        _sessionManager.Set(CurrentUserKey, user);
    }

    public SessionUserDto? GetCurrentUser()
    {
        return _sessionManager.Get<SessionUserDto>(CurrentUserKey);
    }

}
