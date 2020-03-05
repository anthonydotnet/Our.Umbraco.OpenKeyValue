using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;

namespace Our.Umbraco.OpenKeyValue.Core.Extensions
{
    public static class EntityExtensions
	{
		public static KeyValueDto ToDto(this KeyValue poco)
		{
			var dto = new KeyValueDto()
			{
				Key = poco.Key,
				Value = poco.Value,
				Updated = poco.Updated,
			};

			return dto;
		}
	}
}
