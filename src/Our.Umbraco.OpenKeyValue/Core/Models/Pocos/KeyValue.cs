using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using umbraCore = Umbraco.Core;

namespace Our.Umbraco.OpenKeyValue.Core.Models.Pocos
{
	[TableName(umbraCore.Constants.DatabaseSchema.Tables.KeyValue)]
	[PrimaryKey("key", AutoIncrement = false)]
	[ExplicitColumns]
	public class KeyValue
	{
		[Column("key")]
		[Length(256)]
		[PrimaryKeyColumn(AutoIncrement = false, Clustered = true)]
		public string Key { get; set; }

		[Column("value")]
		[NullSetting(NullSetting = NullSettings.Null)]
		public string Value { get; set; }

		[Column("updated")]
		[Constraint(Default = SystemMethods.CurrentDateTime)]
		public DateTime Updated { get; set; }
	}
}

