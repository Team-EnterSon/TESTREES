using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using EnterSon.Internationalization;

namespace EnterSon.UI
{
	[RequireComponent(typeof(Text))]
	public class TextTranslator : MonoBehaviour
	{
		public void OnEnable()
		{
			var targetText = GetComponent<Text>();
			targetText.text = I18N.Instance.Get(targetText.text);
		}
	}
}
