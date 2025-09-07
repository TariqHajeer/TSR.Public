using System;
using Application.Dtos.Common;

namespace Application.Interfaces;

public interface IBrandService
{
    Task<List<BrandDto>> GetAllBrands();
}

