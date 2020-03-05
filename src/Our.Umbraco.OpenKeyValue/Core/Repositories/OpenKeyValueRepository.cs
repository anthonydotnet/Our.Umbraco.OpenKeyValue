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
		KeyValue Create(KeyValue poco);
		IEnumerable<KeyValue> GetAll();
		KeyValue Update(KeyValue poco);
		void Delete(string key);
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


		public void Save(KeyValue entity)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				scope.Database.Insert(entity);
				scope.Complete();
			}
		}

		public void Delete(string key)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				var poco = Get(key);

				scope.Database.Delete(poco);
				scope.Complete();
			}
		}

		public KeyValue Get(string key)
		{
			using (var scope = _scopeProvider.CreateScope())
			{
				var db = scope.Database;
				//var poco = db.Query<KeyValue>("SELECT * FROM umbracoKeyValue WHERE key = '@0'", key).Single();
				var poco = db.Query<KeyValue>("SELECT * FROM umbracoKeyValue WHERE [key] = @key", new { key }).FirstOrDefault();

				return poco;
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
	}
}
