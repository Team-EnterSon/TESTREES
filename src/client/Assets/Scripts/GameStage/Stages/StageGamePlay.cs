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

		// NOTE(sorae): All resources used in this stage must register their own disposers here.
		private CompositeDisposable _disposables = new CompositeDisposable();

		private InGameBoard _inGameBoard { get; set; } = null;

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
			if(NetworkServer.Listen(8965))
				Debug.Log("NetworkServer activated");
			else
				Debug.LogError("Cannot start NetworkServer");

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
			yield return connectToServerRoutine();
		}

		private IEnumerator connectToServerRoutine()
		{
			var client = new NetworkClient();
			Debug.LogFormat("Trying to connect at <color=blue>{0}</color>", StageGamePlay.ServerAddress);
			client.RegisterHandler(MsgType.Connect, (_) => Debug.Log("Connected!"));
			client.Connect(StageGamePlay.ServerAddress, 8965);

			yield break;
		}

		private void setupUI()
		{
			
		}
	}
}
