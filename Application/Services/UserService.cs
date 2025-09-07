using System;
using Application.Dtos.UserDtos;
using Application.Enums;
using Application.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IApiService _apiService;
    private readonly IUserSessionManager _userSessionManager;
    public UserService(IApiService apiService, IUserSessionManager userSessionManager)
    {
        _apiService = apiService;
        _userSessionManager = userSessionManager;
    }
    public async Task<EnumPublicUserStatus?> Login(string username, string password)
    {
        var result = await _apiService.GetAsync<UserDto>($"PublicUser/LoginPublicUser?username={username}&password={password}");
        if (result == null || result.NationalId <= 0)
        {
            return null;
        }
        if (result.Status == EnumPublicUserStatus.Active)
        {
            _userSessionManager.SetCurrentUser(result);
        }
        return result.Status;
    }
}
