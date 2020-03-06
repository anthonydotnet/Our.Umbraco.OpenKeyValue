using System;
using Xunit;
using Moq;
using Our.Umbraco.OpenKeyValue.Core.Repositories;
using Our.Umbraco.OpenKeyValue.Core.Services;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using Our.Umbraco.OpenKeyValue.Core.Builders;

namespace Package.Test
{
	public class When_Deleting
	{
		Mock<IOpenKeyValueRepository> _repoMock;
		Mock<IKeyValuePocoBuilder> _builderMock;	
		IOpenKeyValueService _service;
		KeyValue _existingPoco;

		public When_Deleting()
		{
			_repoMock = new Mock<IOpenKeyValueRepository>();
			_builderMock = new Mock<IKeyValuePocoBuilder>();
			_service = new OpenKeyValueService(_repoMock.Object, _builderMock.Object);

			_existingPoco = new KeyValue()
			{
				Key = "key",
				Value = "Existing Value",
				Updated = new DateTime(2020, 1, 1)
			};
		}

		[Fact]
		public void Value_Is_Retrieved()
		{
			// setup
			var key = "key";
			var expected = _existingPoco.Value;

			_repoMock.Setup(x => x.Get(key)).Returns(_existingPoco);
			
			// run
			_service.Delete(key);

			// assert
			_repoMock.Verify(x => x.Delete(key));
		}


	}
}
