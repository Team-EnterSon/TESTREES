using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterSon
{
	public static class App
	{
		public static bool IsBatchMode
		{
			get
			{
#if UNITY_STANDALONE
				return Environment.CommandLine.Contains("-batchmode");
#else
				return false;
#endif
			}
		}
	}
}
