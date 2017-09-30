namespace TESTREES.Tables
{
	[System.AttributeUsage(System.AttributeTargets.Struct)]
	internal class HasTable : System.Attribute
	{
		public string TableName { get; set; }
		public System.Type TableType { get; set; }
		public HasTable(string tableName, System.Type tableType)
		{
			this.TableName = tableName;
			this.TableType = tableType;
		}
	}
}