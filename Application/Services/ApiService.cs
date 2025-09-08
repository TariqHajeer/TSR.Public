using System;
using System.Text;
using System.Text.Json;
using Application.Dtos.Common;
using Application.Interfaces;

namespace Application.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true, // maps nameAr -> NameAr
        PropertyNamingPolicy = null          // keep PascalCase
    };

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResult> GetAsync(string url, Dictionary<string, string>? headers = null)
    {
        AddHeaders(headers);
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return new HttpResult
        {
            StatusCode = (int)response.StatusCode,
            Content = content,
            IsSuccessStatusCode = response.IsSuccessStatusCode
        };
    }
    public async Task<TResult> GetAsync<TResult>(string url, Dictionary<string, string>? headers = null)
    {
        AddHeaders(headers);
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await Deserialize<TResult>(response);
    }

    public async Task<HttpResult> PostAsync<TRequest>(string url, TRequest data, Dictionary<string, string>? headers = null)
    {
        AddHeaders(headers);
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        var resultContent = await response.Content.ReadAsStringAsync();

        return new HttpResult
        {
            StatusCode = (int)response.StatusCode,
            Content = resultContent,
            IsSuccessStatusCode = response.IsSuccessStatusCode
        };
    }
    public async Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest data, Dictionary<string, string>? headers = null)
    {
        AddHeaders(headers);
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }
        return await Deserialize<TResult>(response);

    }

    public async Task<HttpResult> DeleteAsync(string url, Dictionary<string, string>? headers = null)
    {
        AddHeaders(headers);
        var response = await _httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return new HttpResult
        {
            StatusCode = (int)response.StatusCode,
            Content = content,
            IsSuccessStatusCode = response.IsSuccessStatusCode
        };
    }
    public async Task<HttpResult> PostFormAsync(string url, Dictionary<string, string> formData, Dictionary<string, string>? headers = null)
    {
        AddHeaders(headers);

        var content = new FormUrlEncodedContent(formData);

        var response = await _httpClient.PostAsync(url, content);
        var resultContent = await response.Content.ReadAsStringAsync();

        return new HttpResult
        {
            StatusCode = (int)response.StatusCode,
            Content = resultContent,
            IsSuccessStatusCode = response.IsSuccessStatusCode
        };
    }

    public async Task<HttpResult> PostMultipartAsync(string url, MultipartFormDataContent formData, Dictionary<string, string>? headers = null)
    {
        AddHeaders(headers);

        var response = await _httpClient.PostAsync(url, formData);
        var resultContent = await response.Content.ReadAsStringAsync();

        return new HttpResult
        {
            StatusCode = (int)response.StatusCode,
            Content = resultContent,
            IsSuccessStatusCode = response.IsSuccessStatusCode
        };
    }


    private void AddHeaders(Dictionary<string, string>? headers)
    {
        if (headers == null) return;

        foreach (var header in headers)
        {
            if (_httpClient.DefaultRequestHeaders.Contains(header.Key))
                _httpClient.DefaultRequestHeaders.Remove(header.Key);

            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }
    private static async Task<TResult> Deserialize<TResult>(HttpResponseMessage httpResponse)
    {
        var responseString = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResult>(responseString, Options)!; // add ! if sure not null
    }

}
