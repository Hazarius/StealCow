using System.Collections.Generic;
using Model;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameMainController))]
public class GameMainControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Add Level"))
		{
			var gameController = (GameMainController) target;
			var newContext = new GameContext();
			newContext.corralData = new List<CorralInitialData>();
			newContext.corralData.Add(new CorralInitialData() {position = new Vector2(5,5),size = new Vector2(2,2)});
			newContext.dogs = new List<ObjectInitialData>();
			newContext.dogs.Add(new ObjectInitialData() {initialPosition = new Vector2(1,1),speed = 3});
			newContext.cows = new List<ObjectInitialData>();
			for (var i = 0; i < 100; i++)
			{
				newContext.cows.Add(new ObjectInitialData() {initialPosition = new Vector2(Random.Range(1,17),Random.Range(0,9)),speed = 3});
			}
			newContext.conditionData = new GameConditionData() {maxTime = -1,requiredCow = 100};
			gameController.levels.Add(newContext);						
		}
	}
}