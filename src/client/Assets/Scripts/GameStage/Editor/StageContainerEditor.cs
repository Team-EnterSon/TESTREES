using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EnterSon.GameStage
{
	[CustomEditor(typeof(StageContainer))]
	public class StageContainerEditor : Editor
	{
		private int _startStageIndex = 0;

		public override void OnInspectorGUI()
		{ 
			base.OnInspectorGUI();

			drawFirstStageToRunField();

		}

		private void drawFirstStageToRunField()
		{
			var fieldName = "StartStage";

			var stageNames = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
								   from assemblyType in domainAssembly.GetTypes()
								   where assemblyType.IsSubclassOf(typeof(GameStage))
								   select assemblyType.ToString());

			var beutifiedStageNames = stageNames.Select(eachName => eachName.Split('.').Last());

			if(stageNames.Count() == 0)
			{
				EditorGUILayout.LabelField(fieldName, "There is no class inherits from GameStage.");
				return;
			}

			// there are least 1 type of GameStage
			_startStageIndex = EditorGUILayout.Popup(fieldName, _startStageIndex, beutifiedStageNames.ToArray());

			var selectedStageName = stageNames.ElementAt(_startStageIndex);
			var startStageField = typeof(StageContainer).GetField("_startStageName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			startStageField.SetValue(target, selectedStageName);

			serializedObject.ApplyModifiedProperties();
		}
	}
}