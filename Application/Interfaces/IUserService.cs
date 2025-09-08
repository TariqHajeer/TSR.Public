using System;
using Application.Enums;

namespace Application.Interfaces;

public interface IUserService
{
    Task<EnumPublicUserStatus?> Login(string username, string password);
    Task<bool> ValidateOtp(string otp);
}
