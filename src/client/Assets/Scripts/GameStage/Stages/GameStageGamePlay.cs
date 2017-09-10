﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnterSon.GameStage;
using TESTREES.GamePlay;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TESTREES.Networking;
using EnterSon.Utilities;
using UniRx;

namespace TESTREES.GameStages
{

	public class GameStageGamePlay : GameStage
	{
		private GameContext _gameContext = null;

		private NetworkManager _networkManager = null;

		private HashSet<IDisposable> _disposables = new HashSet<IDisposable>();

		public override void InitializeStage()
		{
			base.InitializeStage();
		}

		public override void EnterStage()
		{
			base.EnterStage();

			StartCoroutine(MainRoutine());
		}

		public override void ExitStage()
		{
			base.ExitStage();

			_disposables.ForEach(eachDisposable => eachDisposable.Dispose());
			_disposables.Clear();

		}

		public override void UpdateStage(float dt)
		{
			base.UpdateStage(dt);

		}

		public IEnumerator MainRoutine()
		{
			yield return initializeNetwork();
			yield return initializeGame();
		}

		private IEnumerator initializeGame()
		{
			Debug.Log("[GameStageGamePlay] Start to initialize Game...");

			var gameCam = GameCamera.Factory.Create();
			_disposables.Add(gameCam);

			_gameContext = GameContext.Factory.Create(_networkManager);
			_disposables.Add(_gameContext);
			yield return _gameContext.InitializeGame();
			yield break;
		}

		private IEnumerator initializeNetwork()
		{
			// TODO(sorae): network connection

			_networkManager = new GameObject("NetworkManager").AddComponent<NetworkManager>();
			_networkManager.gameObject.AddComponent<NetworkManagerHUD>();

			_networkManager.autoCreatePlayer = false;

			Debug.Log("[GameStageGamePlay] Waiting for network connection..");
			while (false == _networkManager.isNetworkActive)
				yield return null;
			Debug.Log("[GameStageGamePlay] Connected!");

			yield break;
		}
	}
}
