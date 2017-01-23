using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEditor.XCodeEditor;

public static class InMobiPostProcessBuild {
	[PostProcessBuild]
	public static void onPostProcessBuild(BuildTarget target,string targetPath){
		string xcodeProjPath = Application.dataPath;
		Debug.Log ("[Inmobi] xcode project path is: " + xcodeProjPath);
		Debug.Log ("[Inmobi] targetPath parameter: " + targetPath);

		XCProject project = new XCProject (targetPath);
		var files = System.IO.Directory.GetFiles( xcodeProjPath, "*.projmods", System.IO.SearchOption.AllDirectories );

		foreach( var file in files ) {
			project.ApplyMod( file );
		}
		
		// Finally save the xcode project
		project.Save();

	}
}
