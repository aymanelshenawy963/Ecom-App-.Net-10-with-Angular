using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;

namespace Ecom.Api.Mapping;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<CategoryDTO, Category>().ReverseMap();
        CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
    }
}
