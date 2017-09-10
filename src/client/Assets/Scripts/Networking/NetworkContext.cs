using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TESTREES.Networking
{
	public class NetworkContext : NetworkManager
	{
		private static NetworkContext _cachedInstance = null;
		public static NetworkContext Instance
		{
			get
			{
				if (_cachedInstance == null)
				{
					var newGameObject = new GameObject("NetworkContext");
					_cachedInstance = newGameObject.AddComponent<NetworkContext>();
					newGameObject.AddComponent<NetworkManagerHUD>();
				}
				return _cachedInstance;
			}
		}
	}
}