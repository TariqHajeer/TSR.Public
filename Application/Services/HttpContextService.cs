using System;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Session methods
    public void SetSessionString(string key, string value)
    {
        _httpContextAccessor.HttpContext?.Session.SetString(key, value);
    }

    public string? GetSessionString(string key)
    {
        return _httpContextAccessor.HttpContext?.Session.GetString(key);
    }

    public void SetSessionInt(string key, int value)
    {
        _httpContextAccessor.HttpContext?.Session.SetInt32(key, value);
    }

    public int? GetSessionInt(string key)
    {
        return _httpContextAccessor.HttpContext?.Session.GetInt32(key);
    }

    public void RemoveSessionKey(string key)
    {
        _httpContextAccessor.HttpContext?.Session.Remove(key);
    }

    public void ClearSession()
    {
        _httpContextAccessor.HttpContext?.Session.Clear();
    }

    public bool SessionKeyExists(string key)
    {
        return _httpContextAccessor.HttpContext?.Session.Keys.Contains(key) ?? false;
    }
}


