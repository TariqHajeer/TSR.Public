using System;

namespace Application.Interfaces;

public interface IHttpContextService
{
    void SetSessionString(string key, string value);
    string GetSessionString(string key);
    void SetSessionInt(string key, int value);
    int? GetSessionInt(string key);
    void RemoveSessionKey(string key);
    void ClearSession();
    bool SessionKeyExists(string key);
}
