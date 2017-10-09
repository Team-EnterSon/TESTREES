using EnterSon.Stage;
using EnterSon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TESTREES.UI;
using UniRx;
using UnityEngine;

namespace TESTREES.Stages
{
	public class StageLobby : Stage
	{
		private CompositeDisposable _disposables = new CompositeDisposable();

		private LobbyBoard _lobbyBoard = default(LobbyBoard);

		// TODO(sorae): Must to be replaced by DataContainer
		public static string Nickname = default(string);

		public override void InitializeStage()
		{
			base.InitializeStage();

		}

		public override void EnterStage()
		{
			base.EnterStage();

			setupUI();
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

		private void setupUI()
		{
			_lobbyBoard = Instantiate(Resources.Load<LobbyBoard>("UIBoards/LobbyBoard"));
			CanvasController.Attach(_lobbyBoard);
			_disposables.Add(_lobbyBoard);

			_lobbyBoard.OnGameStartButtonClicked += OnGameStartButtonClicked;
			_lobbyBoard.NicknameBanner = StageLobby.Nickname;
		}

		private void OnGameStartButtonClicked()
		{
			// check given server address is valid
			var givenAddress = _lobbyBoard.ServerAddress;
			IPAddress _;
			if (false == IPAddress.TryParse(givenAddress, out _))
			{
				// TODO(sorae): Show popup message - "Invalid server address!!"
				return;
			}

			StageGamePlay.ServerAddress = givenAddress;
			setNextStage<StageGamePlay>();
		}
	}
}
