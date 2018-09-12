using AutoMapper;

namespace JjOnlineStore.Common.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}