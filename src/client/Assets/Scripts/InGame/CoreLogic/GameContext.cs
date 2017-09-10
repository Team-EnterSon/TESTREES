using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace TESTREES.GamePlay
{
	public class GameContext : MonoBehaviour, IDisposable
	{
		private NetworkManager _networkManager = null;

		public IEnumerator InitializeGame()
		{
			var mapPrefab = Resources.Load<GameObject>("InGame/Prefabs/Map");
			ClientScene.RegisterPrefab(mapPrefab);

			if (NetworkServer.active)
			{
				// NOTE(sorae): spawn map..

				var mapObj = Instantiate(mapPrefab);
				NetworkServer.Spawn(mapObj);
			}

			yield break;
		}

		public void Dispose()
		{
			// TODO(sorae): impl..
		}

		public static class Factory
		{
			public static GameContext Create(NetworkManager networkManager)
			{
				var gameObject = new GameObject("GameContext");
				var instance = gameObject.AddComponent<GameContext>();
				instance._networkManager = networkManager;



				return instance;
			}
		}
	}
}