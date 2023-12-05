using AutoMapper;

namespace OnlineLibary.IoC
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration GetConfiguration()
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.StartsWith("OnlineLibary."))
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p));
            return new MapperConfiguration(cfg =>
            {
                foreach (var type in types)
                    cfg.AddProfile(type);
            });
        }
    }
}
