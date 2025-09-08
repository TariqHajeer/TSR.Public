using System;
using Application.Dtos.UserDtos;

namespace Application.Interfaces;

public interface IUserSessionManager
{

    void SetCurrentUser(SessionUserDto user);
    SessionUserDto? GetCurrentUser();

}
