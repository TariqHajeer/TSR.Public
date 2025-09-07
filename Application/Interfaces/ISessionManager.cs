using System;

namespace Application.Interfaces;

public interface ISessionManager
{

    void Set<T>(string key, T value);
    T? Get<T>(string key);
    void Remove(string key);
    void Clear();
}
