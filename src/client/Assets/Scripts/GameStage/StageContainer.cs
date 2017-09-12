using EnterSon.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnterSon.GameStage
{
	public class StageContainer : MonoBehaviour
	{
		[SerializeField, HideInInspector]
		private string _startStageName = null;

		private bool _isInitialized = false;

		private Dictionary<Type, GameStage> _stages = new Dictionary<Type, GameStage>();
		public IEnumerable<GameStage> Stages { get { return _stages.Values; } }

		private GameStage _currentStage = null;

		public void OnEnable()
		{
			if (false == _isInitialized)
			{
				initializeAllStages();
				_isInitialized = true;
			}
		}

		private void initializeAllStages()
		{
			var allTypesOfStage = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
								   from assemblyType in domainAssembly.GetTypes()
								   where assemblyType.IsSubclassOf(typeof(GameStage))
								   select assemblyType).ToList();

			Action<Type> addStage = (stageType) =>
			{
			// TODO(sorae): Filter out types that are not stages.
			var newStage = this.gameObject.AddComponent(stageType) as GameStage;
				if (newStage != null)
					this._stages.Add(stageType, newStage);
			};

			allTypesOfStage.ForEach(stageType => addStage(stageType));
			this.Stages.ForEach(eachStage => eachStage.InitializeStage());

			// TODO(sorae): start with launcher stage
			switchStage(Type.GetType(_startStageName));
		}

		public void Update()
		{
			_currentStage.UpdateStage(Time.deltaTime);
			if (_currentStage.NextStageType != null)
				switchStage(_currentStage.NextStageType);
		}

		private void switchStage(Type nextStageType)
		{
			Debug.Assert(nextStageType != null);
			Debug.Assert(_stages.ContainsKey(nextStageType));

			// NOTE(sorae): current stage can be null when launch
			_currentStage?.ExitStage();
			_currentStage = _stages[nextStageType];
			_currentStage.EnterStage();
		}

		private IEnumerator clearMemoryRoutine()
		{
			// NOTE(sorae): Delay 1 frame to be assured that references has been cleaned up
			yield return null;
			Resources.UnloadUnusedAssets();
		}
	}
}