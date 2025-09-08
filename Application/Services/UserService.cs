using Application.Dtos.Common;
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
            var sessionUser = SessionUserDto.FromUser(result);
            sessionUser.NeedToVerifyOtp = true;
            _userSessionManager.SetCurrentUser(sessionUser);
            await _apiService.PostAsync("Otp/addOtp", new RequestOtp()
            {
                Mobile = result.MobileNo,
                UserId = sessionUser.NationalId
            });
        }
        return result.Status;
    }
    public async Task<bool> ValidateOtp(string otp)
    {
        if (string.IsNullOrEmpty(otp))
        {
            throw new ArgumentNullException(nameof(otp));
        }
        var user = _userSessionManager.GetCurrentUser();
        var request = new VerifyOTPDto
        {
            Code = otp,
            UserId = user.NationalId
        };
        var result = await _apiService.PostAsync<VerifyOTPDto, ResultMessageDto>("Otp/VerifyOtp", request);
        var success = result?.IsSuccess ?? false;
        if (success)
        {
            user.NeedToVerifyOtp = false;
            _userSessionManager.SetCurrentUser(user);
        }
        return success;
    }
}
