using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EnterSon.UI
{
	public static class CanvasController
	{
		private static Canvas _cachedCanvas = null;
		public static Canvas UICanvas
		{
			get
			{
				if(_cachedCanvas == null)
				{
					// TODO(sorae): Handle exception
					_cachedCanvas = GameObject.FindObjectOfType<Canvas>();
				}

				return _cachedCanvas;
			}
		}

		public static void Attach(UIBoard board)
		{
			board.transform.SetParent(UICanvas.transform);
			board.transform.localScale = Vector3.one;
		}
	}
}
