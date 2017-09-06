﻿using EnterSon.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageContainer : MonoBehaviour
{
	[SerializeField]
	private GameStage _firstStage = null;

	private bool _isInitialized = false;

	private Dictionary<Type, GameStage> _stages = new Dictionary<Type, GameStage>();
	public IEnumerable<GameStage> Stages { get { return _stages.Values; } }

	private GameStage _currentStage = null;

	public void OnEnable()
	{
		if(false == _isInitialized)
		{
			initializeAllStages();
			_isInitialized = true;
		}
	}
	
	private void initializeAllStages()
	{
		var allTypesOfStage = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
							   from assemblyType in domainAssembly.GetTypes()
							   where typeof(GameStage).IsAssignableFrom(assemblyType)
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
		//switchStage(typeof())
	}

	public void Update()
	{
		_currentStage.UpdateStage(Time.deltaTime);
		if (_currentStage.NextStageType != null)
			switchStage(_currentStage.NextStageType);
	}

	private void switchStage(Type nextStageType)
	{
		Debug.Assert(_stages.ContainsKey(nextStageType));

		// NOTE(sorae): current stage can be null when launch
		if(_currentStage != null)
			_currentStage.ExitStage();
		_currentStage = _stages[nextStageType];
		_currentStage.EnterStage();
	}
}