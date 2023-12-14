using AutoMapper;

namespace OnlineLibary.Managers.Managers
{
    public class BaseManager
    {
        public BaseManager(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper;
        protected IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;
    }
}
