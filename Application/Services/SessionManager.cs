using System;
using System.Text.Json;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class SessionManager : ISessionManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public void Set<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        Session.SetString(key, json);
    }

    public T? Get<T>(string key)
    {
        var json = Session.GetString(key);
        if (json == null) return default;
        return JsonSerializer.Deserialize<T>(json);
    }

    public void Remove(string key)
    {
        Session.Remove(key);
    }

    public void Clear()
    {
        Session.Clear();
    }

}
