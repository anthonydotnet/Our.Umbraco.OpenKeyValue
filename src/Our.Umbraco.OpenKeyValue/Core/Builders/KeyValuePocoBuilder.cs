using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using System;

namespace Our.Umbraco.OpenKeyValue.Core.Builders
{
	public interface IKeyValuePocoBuilder
	{
		KeyValue Build(string key, string value);
	}

	public class KeyValuePocoBuilder: IKeyValuePocoBuilder
	{ 
		public KeyValue Build(string key, string value)
		{
			var poco = new KeyValue
			{
				Key = key,
				Value = value,
				Updated = DateTime.UtcNow
			};
			return poco;
		}
	}
}

