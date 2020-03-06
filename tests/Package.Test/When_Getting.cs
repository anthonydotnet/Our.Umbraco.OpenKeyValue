using System;
using Xunit;
using Moq;
using Our.Umbraco.OpenKeyValue.Core.Repositories;
using Our.Umbraco.OpenKeyValue.Core.Services;
using Our.Umbraco.OpenKeyValue.Core.Models.Pocos;
using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.OpenKeyValue.Core.Builders;

namespace Package.Test
{
	public class When_Getting
	{
		Mock<IOpenKeyValueRepository> _repoMock;
		Mock<IKeyValuePocoBuilder> _builderMock;	
		IOpenKeyValueService _service;
		KeyValue _existingPoco;

		public When_Getting()
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
			var expected = _existingPoco;

			var createdPoco = new KeyValue();
			_repoMock.Setup(x => x.Get(key)).Returns(_existingPoco);
			
			// run
			var result = _service.Get(key);

			// assert
			Assert.Equal(expected.Key, result.Key);
			Assert.Equal(expected.Value, result.Value);
		}


		[Fact]
		public void Item_Exists()
		{
			// setup
			var key = "key";
			var expected = _existingPoco;

			var createdPoco = new KeyValue();
			_repoMock.Setup(x => x.Exists(key)).Returns(true);

			// run
			var result = _service.Exists(key);

			// assert
			Assert.True(result);
			_repoMock.Verify(x => x.Exists(key));
		}


		[Fact]
		public void NonExisting_Item_Is_Handled()
		{
			// setup
			var key = "key";
			var expected = _existingPoco.Value;

			var createdPoco = new KeyValue();
			_repoMock.Setup(x => x.Get(key));

			// run
			var result = _service.Get(key);

			// assert
			Assert.Null(result);
		}

		[Fact]
		public void All_Values_Are_Retrieved()
		{
			// setup
			var values = new List<KeyValue>() { _existingPoco };

			_repoMock.Setup(x => x.GetAll()).Returns(values);

			// run
			var result = _service.GetAll();

			// assert
			Assert.Equal(_existingPoco.Key, result.First().Key);
			Assert.Equal(_existingPoco.Value, result.First().Value);
		}
	}
}
