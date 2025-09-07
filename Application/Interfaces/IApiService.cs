using System;
using Application.Dtos.Common;

namespace Application.Interfaces;

public interface IApiService
{
    Task<HttpResult> GetAsync(string url, Dictionary<string, string>? headers = null);
    Task<TResult> GetAsync<TResult>(string url, Dictionary<string, string>? headers = null);
    Task<HttpResult> PostAsync<TRequest>(string url, TRequest data, Dictionary<string, string>? headers = null);
    Task<HttpResult> DeleteAsync(string url, Dictionary<string, string>? headers = null);
    Task<HttpResult> PostFormAsync(string url, Dictionary<string, string> formData, Dictionary<string, string>? headers = null);
    Task<HttpResult> PostMultipartAsync(string url, MultipartFormDataContent formData, Dictionary<string, string>? headers = null);

}
