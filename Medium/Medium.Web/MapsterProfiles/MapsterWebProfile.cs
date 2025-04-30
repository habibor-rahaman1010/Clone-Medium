using Mapster;
using Medium.Application.DTO;
using Medium.Domain.Entities;
using Medium.Web.Areas.Admin.Models.Category;

namespace Medium.Web.MapsterProfiles
{
    public class MapsterWebProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryDto>();
            config.NewConfig<CategoryUpdateModel, Category>();
        }
    }
}
