using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EnterSon.I18N
{
	public class I18N
	{
		// The sequence of this enum must match with the sequence of the I18N.csv file.
		public enum Language
		{
			kKorean = 0,
			kEnglish = 1,
		}

		private static I18N _instance = default(I18N);
		private Dictionary<string, string> _loadedData { get; set; } = new Dictionary<string, string>();
		private const string I18N_PATH = @"Tables/I18N";

		public static I18N Instance
		{
			get
			{
				if (_instance == null)
					_instance = new I18N();
				return _instance;
			}
		}

		public void Initialize(Language lang)
		{
			loadData(lang);
		}

		private void loadData(Language lang)
		{
			var sourceText = Resources.Load<TextAsset>(I18N_PATH);
			if(sourceText == null)
			{
				Debug.LogWarningFormat("[I18N] Initialize Fail! Cannot find I18N.csv");
			}
			StringReader reader = new StringReader(sourceText.text);
			while(true)
			{
				string line = reader.ReadLine();
				if (line == null)
					break;

				// If the text does not exist or not translated, show the raw key.
				var each = line.Split(',');
				if(each.Length < (int)lang + 2)
				{
					Debug.LogWarningFormat("[I18N] Initialize Fail! Your Language does not exist is I18N.csv file.");
					_loadedData.Add(each[0], each[0]);
				}
				else
				{
					if(each[(int)lang + 1].Length == 0)
					{
						_loadedData.Add(each[0], each[0]);
					}
					else
					{
						_loadedData.Add(each[0], each[(int)lang + 1]);
					}
				}
			}
		}

		public string Get(string key)
		{
			if(!_loadedData.ContainsKey(key))
			{
				return key;
			}
			return _loadedData[key];
		}

		public string this[string key]
		{
			get
			{
				return Get(key);
			}
		}
	}
}
