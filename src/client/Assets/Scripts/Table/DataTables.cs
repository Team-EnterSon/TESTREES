using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TESTREES.Tables
{
	public class DataTables
	{
		private static Dictionary<Type, object> loadedTables { get; set; } = new Dictionary<Type, object>();


		public static void LoadTables()
		{
			var allTableDataTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
								   from assemblyType in domainAssembly.GetTypes()
								   where Attribute.IsDefined(assemblyType, typeof(HasTable))
								   select assemblyType).ToList();
			allTableDataTypes.ForEach(eachType => loadTable(eachType));
		}

		public static object Get<T>() where T : struct
		{
			if(false == loadedTables.ContainsKey(typeof(T)))
			{
				Debug.LogErrorFormat("[DataTables] There is no loaded table for <color=orange>{0}</color>", typeof(T).Name);
			}
			return loadedTables[typeof(T)];
		}

		private static void loadTable(Type tableType)
		{
			if (false == Attribute.IsDefined(tableType, typeof(HasTable)))
			{
				Debug.LogWarningFormat(
					"[DataTables] Given type <color=orange>{0}</color> has not attribute <color=blue>{1}</color>!! Skip to parse..",
					tableType.Name, typeof(HasTable).ToString().Split('.').Last());
				return;
			}
			else if (loadedTables.ContainsKey(tableType))
			{
				Debug.LogWarningFormat("[DataTables] Table of given type <color=orange>{0}</color> is already loaded!! Skip to parse..", tableType.Name);
				return;
			}

			var tableAttribute = tableType.GetCustomAttributes(true).
				Where(eachAttribute => eachAttribute is HasTable).Cast<HasTable>()
				.FirstOrDefault();
			var tableName =  "Tables/" + tableAttribute.TableName;

			var sourceText = Resources.Load<TextAsset>("TableName");
			if(sourceText == null)
			{
				Debug.LogWarningFormat("[DataTables] Cannot load table <color=orange>{0}</color>! Skip to parse..",	tableName);
				return;
			}

			var parsedTable = JsonConvert.DeserializeObject(sourceText.text, tableAttribute.TableType);

			if (parsedTable == null)
			{
				Debug.LogWarningFormat("[DataTables] Cannot parse table <color=orange>{0}</color> as type of <color=orange>{1}</color>", tableName, tableAttribute.TableType.Name);
				return;
			}

			loadedTables.Add(tableType, parsedTable);
		}
	}
}
