using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnterSon.Stage;
using TESTREES.GamePlay;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using EnterSon.Utilities;
using UniRx;
using EnterSon.UI;
using EnterSon;

namespace TESTREES.Stages
{

	public class StageGamePlay : Stage
	{
		public static string ServerAddress { get; set; } = "127.0.0.1";
		private const int PORT_NUMBER = 8962;

		// NOTE(sorae): All resources used in this stage must register their own disposers here.
		private CompositeDisposable _disposables = new CompositeDisposable();

		private InGameBoard _inGameBoard { get; set; } = null;

		private GameContext _gameContext { get; set; } = null;

		public override void InitializeStage()
		{
			base.InitializeStage();

		}

		public override void EnterStage()
		{
			base.EnterStage();

			var mainRoutine = App.IsBatchMode ? serverRoutine() : clientRoutine();
			StartCoroutine(mainRoutine);
		}

		private IEnumerator serverRoutine()
		{
			if (NetworkServer.Listen(StageGamePlay.PORT_NUMBER))
				Debug.Log("NetworkServer activated");
			else
			{
				Debug.LogError("Cannot start NetworkServer");
				yield break;
			}

// 			_gameContext = new GameObject("GameContext").AddComponent<GameContext>();
// 			NetworkServer.Spawn(_gameContext);

			yield break;
		}

		public override void ExitStage()
		{
			base.ExitStage();

			_disposables?.Dispose();

		}

		public override void UpdateStage(float dt)
		{
			base.UpdateStage(dt);

		}

		private IEnumerator clientRoutine()
		{
			setupUI();
			yield return connectToServer();

			yield return placePawns();
			yield return playGame();

		}

		private IEnumerator connectToServer()
		{
			var client = new NetworkClient();
			Debug.LogFormat("Trying to connect at <color=blue>{0}</color>", StageGamePlay.ServerAddress);
			client.RegisterHandler(MsgType.Connect, (_) => registerAllSpawnablePrefabs());
			client.Connect(StageGamePlay.ServerAddress, StageGamePlay.PORT_NUMBER);

			while (false == client.isConnected)
				yield return null;
			
			Debug.LogFormat("[{0}] Connected to server!", nameof(StageGamePlay));
		}

		private IEnumerator placePawns()
		{
			

			yield break;
		}

		private IEnumerator playGame()
		{

			yield break;
		}

		private void setupUI()
		{
			
		}

		private void registerAllSpawnablePrefabs()
		{
			// FIXME(sorae): remove path literals 
			var prefabPaths = new string[]
			{
					"InGame/Prefabs/Map",
					"InGame/Prefabs/Pawn",

			};

			foreach(var eachPath in prefabPaths)
			{
				var prefab = Resources.Load<GameObject>(eachPath);
				if (prefab != null)
					ClientScene.RegisterPrefab(prefab);
				else
					Debug.LogWarningFormat("[{0}] Cannot load prefab - <color=red>{1}</color>", nameof(StageGamePlay), eachPath);
			}
		}
	}
}
