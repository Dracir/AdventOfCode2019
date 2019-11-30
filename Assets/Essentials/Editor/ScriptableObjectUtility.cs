using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
	public static T CreateAsset<T>(string name = null, bool focusSelection = true) where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "")
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}
		if (name == null)
			name = typeof(T).ToString();

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/{1}.asset", path, name));

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		if (focusSelection)
		{
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}

		return asset;
	}


	public static T CreateAssetAtPath<T>(string path, string name, bool focusSelection = true) where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/{1}.asset", path, name));

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		if (focusSelection)
		{
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}


		return asset;
	}
}
