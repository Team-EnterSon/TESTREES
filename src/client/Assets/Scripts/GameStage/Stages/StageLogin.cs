using EnterSon.Stage;
using EnterSon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTREES.UI;
using UniRx;
using UnityEngine;

namespace TESTREES.Stages
{
	public class StageLogin : Stage
	{
		private CompositeDisposable _disposables = new CompositeDisposable();

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
			var loginBoard = Instantiate(Resources.Load<LoginBoard>("UIBoards/LoginBoard"));
			CanvasController.Attach(loginBoard);
			_disposables.Add(loginBoard);

			loginBoard.OnLoginButtonClicked += () => OnLoginButtonClicked(loginBoard.Nickname);
		}

		private void OnLoginButtonClicked(string givenNickname)
		{
			if(string.IsNullOrEmpty(givenNickname))
			{
				Debug.Log("[StageLogin] Given nickname is null or empty ~_~");
				// TODO(sorae): Show popup message - "Invalid nickname!"
				return;
			}

			StageLobby.Nickname = givenNickname;
			setNextStage<StageLobby>();
		}
	}
}
