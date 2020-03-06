using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using System;
using System.Collections.Generic;
using Our.Umbraco.OpenKeyValue.Core.Repositories;
using Our.Umbraco.OpenKeyValue.Core.Extensions;
using System.Linq;
using Our.Umbraco.OpenKeyValue.Core.Builders;

namespace Our.Umbraco.OpenKeyValue.Core.Services
{
	public interface IOpenKeyValueService
	{
		IEnumerable<KeyValueDto> GetAll();
		KeyValueDto Get(string key);
		KeyValueDto Set(string key, string value);
		bool Exists(string key);
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

		public KeyValueDto Get(string key)
		{
			var poco = _repository.Get(key);

			return poco?.ToDto();
		}


		public bool Exists(string key)
		{
			var value = _repository.Exists(key);

			return value;
		}

		public KeyValueDto Set(string key, string value)
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
				poco.Updated = DateTime.UtcNow;
				poco = _repository.Update(poco);
			}

			return poco.ToDto();
		}

		public void Delete(string key)
		{
			_repository.Delete(key);
		}
	}
}

