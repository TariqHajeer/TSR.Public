using Application.Enums;

namespace Application.Dtos.UserDtos;

public class RequestOtp
{
    public bool IsMobile { get; set; } = true;
    public EnumOtpStatus OtpStatus { get; set; } = EnumOtpStatus.NewOtp;
    public EnumOtpService OtpServiceId { get; set; } = EnumOtpService.Login;
    public string Mobile { get; set; }
    public long? UserId { get; set; }
}
