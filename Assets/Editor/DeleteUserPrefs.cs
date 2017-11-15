// Clear all the editor prefs keys.
//
// Warning: this will also remove editor preferences as the opened projects, etc.

using UnityEngine;
using UnityEditor;

public class DeletePrferences : ScriptableObject
{
	[MenuItem("Prefs/Clear all Editor Preferences")]
	static void deleteAllEditorPrefs()
	{
		if (EditorUtility.DisplayDialog("Delete all editor preferences.",
			"Are you sure you want to delete all the editor preferences? " +
			"This action cannot be undone.", "Yes", "No"))
		{
			Debug.Log("deleting editor preferences");
			EditorPrefs.DeleteAll();
		}
	}
	
	[MenuItem("Prefs/Clear all Player Preferences")]
	static void deleteAlluserPrefs()
	{
		if (EditorUtility.DisplayDialog("Delete all player preferences.",
			"Are you sure you want to delete all the player preferences? " +
			"This action cannot be undone.", "Yes", "No"))
		{
			Debug.Log("deleting player preferences");
			PlayerPrefs.DeleteAll();
		}
	}
}