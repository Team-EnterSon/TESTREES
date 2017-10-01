using System;
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
using EnterSon.UI;
using TESTREES.TBD;

namespace TESTREES.GameStages
{

	public class GameStageGamePlay : GameStage
	{
		private GameContext _gameContext = null;

		private NetworkManager _networkManager = null;

        // NOTE(sorae): All resources used in this stage must register their own disposers here.
        private CompositeDisposable _disposables = new CompositeDisposable();

		private Player _myPlayer { get; set; } = null;

		private User _myUser { get; set; } = null;

		private InGameBoard _inGameBoard { get; set; } = null;

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

            _disposables.Dispose();
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
			yield return gameRoutine();
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

		private IEnumerator gameRoutine()
		{
			Debug.Log("[GameStageGamePlay] Start game routine...");
			yield return phaseRoutine_PawnPlacement();
			// TODO(sorae): Implement effect for batch unit placement completion
			yield return phaseRoutine_GamePlay();
		}

		/// <summary>
		/// Phase for each player to place pawns.
		/// </summary>
		private IEnumerator phaseRoutine_PawnPlacement()
		{
            // NOTE(sorae): Players need to spawn all pawns in their decks.
            while(_myPlayer.Pawns.Count == _myUser.Deck.Count)
			{
				var pickedPosition = GameCoord.Zero;
				var pawnIDToSpawn = default(int);

				yield return _inGameBoard.WaitForPickSpawningPosition(out pickedPosition, out pawnIDToSpawn);

				if (false == _gameContext.SpawnPawn(pawnIDToSpawn, pickedPosition))
				{
					Debug.LogWarningFormat("[GameStageGamePlay] Failed to spwan pawn! typeof : {0}", pawnIDToSpawn);
					continue;
				}

                yield return null;
            }
		}

		/// <summary>
		/// Game play phase
		/// </summary>
		/// <returns></returns>
		private IEnumerator phaseRoutine_GamePlay()
		{
			yield break;
		}


	}
}
