using Umbraco.Core.Composing;
using Our.Umbraco.OpenKeyValue.Core.Services;
using Our.Umbraco.OpenKeyValue.Core.Repositories;

namespace Our.Umbraco.OpenKeyValue.Core.StartUp
{
	public class OpenKeyValueComposer : IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register(typeof(IOpenKeyValueRepository), typeof(OpenKeyValueRepository));
            composition.Register(typeof(IKeyValuePocoBuilder), typeof(KeyValuePocoBuilder));
            composition.Register(typeof(IOpenKeyValueService), typeof(OpenKeyValueService));
        }
    }
}
