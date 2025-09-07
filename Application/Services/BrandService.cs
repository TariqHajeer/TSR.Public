using System;
using System.Text.Json;
using Application.Dtos.Common;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class BrandService : IBrandService
{
    private readonly IApiService _apiService;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;

    private const string AllBrands = "BrandService.GetAllBrands";

    public BrandService(IApiService apiService, IMemoryCache cache, IConfiguration configuration)
    {
        _apiService = apiService;
        _cache = cache;
        _configuration = configuration;
    }
    public async Task<List<BrandDto>> GetAllBrands()
    {
        if (!_cache.TryGetValue(AllBrands, out List<BrandDto> brands))
        {
            brands = await _apiService.GetAsync<List<BrandDto>>("Brand/GetAllBrand");
            _cache.Set(AllBrands, brands, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(_configuration.GetValue<int>("CacheExpiry"))
            });
        }

        return brands;
    }

}
