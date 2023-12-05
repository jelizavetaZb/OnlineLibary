using LightInject;
using OnlineLibary.IoC;
[assembly: CompositionRootType(typeof(DefaultWebCompositionRoot))]

namespace OnlineLibary.IoC
{
    public class DefaultWebCompositionRoot : ICompositionRoot
    {
        private readonly List<string> _assembliesToScan = new() { "OnlineLibary.*" };
        public void Compose(IServiceRegistry serviceRegistry)
        {
            foreach (var item in _assembliesToScan)
                serviceRegistry.RegisterAssembly($"{item}.dll");

            serviceRegistry.Register(c => AutoMapperConfiguration.GetConfiguration().CreateMapper());
        }
    }
}
