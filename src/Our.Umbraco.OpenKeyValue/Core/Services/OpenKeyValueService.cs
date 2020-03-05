using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using System;
using System.Collections.Generic;
using Our.Umbraco.OpenKeyValue.Core.Repositories;
using Our.Umbraco.OpenKeyValue.Core.Extensions;
using System.Linq;

namespace Our.Umbraco.OpenKeyValue.Core.Services
{
	public interface IOpenKeyValueService
	{
		IEnumerable<KeyValueDto> GetAll();
		string GetValue(string key);
		KeyValueDto SetValue(string key, string value);
		KeyValueDto UpdateValue(string key, string value);
		void Delete(string key);
	}

	public class OpenKeyValueService : IOpenKeyValueService
	{
		private readonly IOpenKeyValueRepository _repository;
		private readonly IKeyValuePocoBuilder _builder;

		public OpenKeyValueService(IOpenKeyValueRepository repository, IKeyValuePocoBuilder builder)
		{
			_repository = repository;
			_builder = builder;
		}


		public IEnumerable<KeyValueDto> GetAll()
		{
			var pocos = _repository.GetAll();

			return pocos.Select(x => x.ToDto());
		}

		public string GetValue(string key)
		{
			var poco = _repository.Get(key);

			return poco.Value;
		}

		public KeyValueDto SetValue(string key, string value)
		{
			var poco = _repository.Get(key);

			if (poco == null)
			{
				poco = _builder.Build(key, value);
				
				poco = _repository.Create(poco);
			}
			else
			{
				poco.Value = value;
				poco.Updated = DateTime.Now;
				poco = _repository.Update(poco);
			}

			return poco.ToDto();
		}

		public KeyValueDto UpdateValue(string key, string value)
		{
			var poco = _repository.Get(key);

			poco.Value = value;
			poco.Updated = DateTime.Now;
			poco = _repository.Update(poco);

			return poco.ToDto();
		}

		public void Delete(string key)
		{
			_repository.Delete(key);
		}
	}


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
				Updated = DateTime.Now
			};
			return poco;
		}
	}
}

