using System;

namespace Application.Dtos.UserDtos;

public class SessionUserDto : UserDto
{
    public bool NeedToVerifyOtp { get; set; }
    public static SessionUserDto FromUser(UserDto user)
    {
        if (user == null) return null;

        return new SessionUserDto
        {
            NationalId = user.NationalId,
            UId = user.UId,
            IdentityKind = user.IdentityKind,
            FullNameAr = user.FullNameAr,
            FullNameEn = user.FullNameEn,
            MobileNo = user.MobileNo,
            Email = user.Email,
            Status = user.Status,
            UserDesc = user.UserDesc,
            EntityNumber = user.EntityNumber,
            BirthDate = user.BirthDate,
            Gender = user.Gender,
            NationalityID = user.NationalityID,
            LastLogin = user.LastLogin,
        };
    }
}

