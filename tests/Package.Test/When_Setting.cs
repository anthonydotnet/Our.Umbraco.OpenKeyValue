using System;
using Xunit;
using Moq;
using Our.Umbraco.OpenKeyValue.Core.Repositories;
using Our.Umbraco.OpenKeyValue.Core.Services;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using Our.Umbraco.OpenKeyValue.Core.Builders;

namespace Package.Test
{
	public class When_Setting
	{
		Mock<IOpenKeyValueRepository> _repoMock;
		Mock<IKeyValuePocoBuilder> _builderMock;	
		IOpenKeyValueService _service;
		KeyValue _existingPoco;

		public When_Setting()
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
		public void New_Value_Is_Saved()
		{
			// setup
			var key = "key";
			var value = "value";

			var createdPoco = new KeyValue();
			_repoMock.Setup(x => x.Get(key));
			_repoMock.Setup(x => x.Create(It.IsAny<KeyValue>())).Returns(createdPoco);
			

			// run
			var result = _service.Set(key, value);

			// assert
			_builderMock.Verify(x => x.Build(key, value));
			_repoMock.Verify(x => x.Create(It.IsAny<KeyValue>()));
			_repoMock.Verify(x => x.Update(It.IsAny<KeyValue>()), Times.Never);
		}

		[Fact]
		public void New_Value_Causes_Update()
		{
			// setup
			var key = "key";
			var value = "value";

			_repoMock.Setup(x => x.Get(key)).Returns(_existingPoco);
			_repoMock.Setup(x => x.Update(It.IsAny<KeyValue>())).Returns(_existingPoco);

			// run
			var result = _service.Set(key, value);

			// assert
			Assert.Equal(value, _existingPoco.Value);
			_repoMock.Verify(x => x.Update(It.IsAny<KeyValue>()));
			_repoMock.Verify(x => x.Create(It.IsAny<KeyValue>()), Times.Never);
		}


		[Fact]
		public void Update_Succeeds()
		{
			// setup
			var key = "key";
			var value = "value";

			_repoMock.Setup(x => x.Get(key)).Returns(_existingPoco);
			_repoMock.Setup(x => x.Update(It.IsAny<KeyValue>())).Returns(_existingPoco);

			// run
			var result = _service.Set(key, value);

			// assert
			Assert.Equal(value, _existingPoco.Value);
			_repoMock.Verify(x => x.Update(It.IsAny<KeyValue>()));
		}
	}
}
