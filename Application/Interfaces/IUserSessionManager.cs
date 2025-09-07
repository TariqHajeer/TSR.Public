using System;
using Application.Dtos.UserDtos;

namespace Application.Interfaces;

public interface IUserSessionManager
{

    void SetCurrentUser(UserDto user);
    UserDto? GetCurrentUser();

}
