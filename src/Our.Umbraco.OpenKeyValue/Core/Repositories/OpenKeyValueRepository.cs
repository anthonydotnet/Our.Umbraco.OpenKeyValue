using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Scoping;

namespace Our.Umbraco.OpenKeyValue.Core.Repositories
{
	public interface IOpenKeyValueRepository
	{
		KeyValue Get(string key);
		IEnumerable<KeyValue> GetAll();
		KeyValue Create(KeyValue poco);
		KeyValue Update(KeyValue poco);
		int Delete(string key);
		bool Exists(string key);
	}


	public class OpenKeyValueRepository : IOpenKeyValueRepository
	{
        private static IScopeProvider _scopeProvider;

        public OpenKeyValueRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

		public IEnumerable<KeyValue> GetAll()
        {
			using (var scope = _scopeProvider.CreateScope())
			{
				var db = scope.Database;
				var ips = db.Query<KeyValue>("SELECT * FROM umbracoKeyValue");

				return ips;
			}
        }

		public KeyValue Get(string key)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				var db = scope.Database;
				var poco = db.Query<KeyValue>("SELECT * FROM umbracoKeyValue WHERE [key] = @key", new { key }).FirstOrDefault();

				return poco;
			}
		}

		public bool Exists(string key)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				var db = scope.Database;
				var value = db.ExecuteScalar<int>("SELECT COUNT(*) FROM umbracoKeyValue WHERE [key] = @key", new { key });

				return value > 0;
			}
		}


		public KeyValue Create(KeyValue poco)
        {
            var idObj = new object();
            using (var scope = _scopeProvider.CreateScope())
            {
                idObj = scope.Database.Insert(poco);
                scope.Complete();                
            }

            var item = Get(Convert.ToString(idObj));

            return item;
        }


		public void Insert(KeyValue entity)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				scope.Database.Insert(entity);
				scope.Complete();
			}
		}

		public KeyValue Update(KeyValue poco)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				scope.Database.Update(poco);
				scope.Complete();
			}

			var item = Get(poco.Key);

			return item;
		}

		public int Delete(string key)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				var result = scope.Database.Delete<KeyValue>("WHERE [key] = @key", new { key });
				scope.Complete();

				return result;
			}
		}
	}
}
