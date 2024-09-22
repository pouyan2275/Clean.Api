using Application.Bases.Interfaces.IServices;
using Application.Dtos.Categories;
using Domain.Entities;

namespace Application.IServices
{
    public interface ICategoryService : ICrudService<CategoryDto,CategoryDtoSelect,Category>
    {
    }
}
