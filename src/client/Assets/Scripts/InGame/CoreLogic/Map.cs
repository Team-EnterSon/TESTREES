using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TESTREES.GamePlay
{
	public class Map : NetworkBehaviour
	{
		[SerializeField]
		private RectTransform[] _deployZones = null;

		public Rect[] DeployZones { get { return _deployZones.Select(eachZone => eachZone.rect).ToArray(); } }


	}
}