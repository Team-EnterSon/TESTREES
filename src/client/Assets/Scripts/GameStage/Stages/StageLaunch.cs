using EnterSon;
using EnterSon.Internationalization;
using EnterSon.Stage;
using EnterSon.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTREES.Tables;
using UniRx;
using UnityEngine;

namespace TESTREES.Stages
{
	public class StageLaunch : Stage
	{
		private CompositeDisposable _disposables = new CompositeDisposable();

		public override void EnterStage()
		{
			base.EnterStage();

			var mainRoutine = App.IsBatchMode ? serverRoutine() : clientRoutine();
			StartCoroutine(mainRoutine);
		}

		public override void ExitStage()
		{
			base.ExitStage();

			_disposables?.Dispose();
		}

		private IEnumerator serverRoutine()
		{
			// TODO(sorae): Connect to DB Server, Log Server, some server initializations..
			setNextStage<StageGamePlay>();
			yield break;
		}

		private IEnumerator clientRoutine()
		{
			setupUI();
			yield return patchRoutine();
			yield return afterPatchRoutine();
			setNextStage<StageLogin>();
		}
		
		private IEnumerator patchRoutine()
		{
			// TODO(sorae): impl..
			yield return new WaitForSeconds(2.25f);
		}

		private IEnumerator afterPatchRoutine()
		{
			DataTables.LoadTables();
			I18N.Instance.Initialize(I18N.Language.kEnglish);

			yield return null;
		}

		private void setupUI()
		{
			var launcherBoard = Instantiate(Resources.Load<UIBoard>("UIBoards/LauncherBoard"));
			CanvasController.Attach(launcherBoard);
			_disposables.Add(launcherBoard);


		}
	}
}
