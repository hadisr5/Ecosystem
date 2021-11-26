using AutoMapper;

namespace Seventy.ViewModels.CustomMapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
