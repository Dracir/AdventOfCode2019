using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityEditor.SceneManagement;

public static class CreateBaseFolderContextMenu
{

	[MenuItem("Assets/GameJam/MakeBaseFolder", false, 999)]
	static void MakeBaseFolder()
	{
		MakeFolder("Prefabs");
		MakeFolder("Scripts",false);
		MakeFolder("Scripts/UI");
		MakeFolder("Scripts/Manager");
		MakeFolder("Scripts/Player");
		MakeFolder("Scripts/Level");
		MakeFolder("Sprites");
		MakeFolder("Sprites/UI");
		MakeFolder("Sprites/Game");
		MakeFolder("Shaders");
		MakeFolder("Sounds",false);
		MakeFolder("Sounds/Music");
		MakeFolder("Sounds/Sfx");
		MakeFolder("Scenes", false);
		MakeFolder("Scenes/Game", false);
		MakeFolder("Scenes/Gyms", false);
		MakeFolder("Materials");
		MakeFolder("Resources");
		MakeFolder("Fonts");
		MakeScene("Scenes/Gyms", "Richard");
		MakeScene("Scenes/Gyms", "Kevin");
		MakeScene("Scenes/Game", "Menu");
		MakeScene("Scenes/Game", "Credit");
		MakeScene("Scenes/Game", "Game");
		RemoveSampleScene();
	}

	private static void RemoveSampleScene()
	{
		File.Delete("Assets/Scenes/SampleScene.unity");
		File.Delete("Assets/Scenes/SampleScene.unity.meta");
	}

	private static void MakeScene(string path, string name)
	{
		var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
		EditorSceneManager.SaveScene(newScene, "Assets/" + path + "/" + name + ".unity");
	}

	static void MakeFolder(string name, bool makeEmptyText = true)
	{
		var pathFolder = Application.dataPath + "/" + name;
		var pathEmptyFile = pathFolder + "/empty.txt";

		if (!Directory.Exists(pathFolder))
		{
			Directory.CreateDirectory(pathFolder);
			if (!File.Exists(pathEmptyFile) && makeEmptyText)
			{
				var fs = File.Create(pathEmptyFile);
				fs.Close();
			}
		}
	}
}
