using System;
using Application.Enums;

namespace Application.Dtos.UserDtos;

public class UserDto
{
    public long NationalId { get; set; }
    public Guid UId { get; set; }
    public EnumIDKind IdentityKind { get; set; }
    public string FullNameAr { get; set; }
    public string FullNameEn { get; set; }
    public string MobileNo { get; set; }
    public string Email { get; set; }
    public EnumPublicUserStatus Status { get; set; }
    public string UserDesc { get; set; }
    public string EntityNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public EnumGender? Gender { get; set; }
    public int? NationalityID { get; set; }
    public DateTime? LastLogin { get; set; }
}
