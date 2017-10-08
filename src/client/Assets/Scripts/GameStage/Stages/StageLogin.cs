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

			initializeUI();
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

		private void initializeUI()
		{
			var loginBoard = Instantiate(Resources.Load<LoginBoard>("UIBoards/LoginBoard"));
			CanvasController.Attach(loginBoard);
			_disposables.Add(loginBoard);

			loginBoard.OnLoginButtonClicked += () => Debug.Log("Clicked!");
		}
	}
}
