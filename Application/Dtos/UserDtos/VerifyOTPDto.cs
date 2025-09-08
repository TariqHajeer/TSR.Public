using System;

namespace Application.Dtos.UserDtos;

public class VerifyOTPDto : RequestOtp
{
    public string Code { get; set; }
}
