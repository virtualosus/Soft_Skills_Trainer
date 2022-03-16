#if UNITY_IPHONE
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;


public class PostBuildProcesses : MonoBehaviour {

	const string iOSMicrophoneUsageDesc = "Microphone access is required for speech recognition";

	[PostProcessBuild]
	public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject) {
		Debug.Log ("Running PostBuild process");	

		if (buildTarget == BuildTarget.iOS) {
			Debug.Log ("Modifying XCode settings");	


			//ADD MICROPHONE LINE TO PLIST FILE
			// this can now also be done directly in Unity via Player Settings for iOS
			string plistPath = Path.Combine( pathToBuiltProject, "Info.plist");
			PlistDocument plist = new PlistDocument();
			plist.ReadFromString(File.ReadAllText(plistPath));

			Debug.Log ("Adding microphone use description to Info.plist (this can also be done via Player settings)");
			PlistElementDict rootDict = plist.root;
			rootDict.SetString("NSMicrophoneUsageDescription", iOSMicrophoneUsageDesc);
			File.WriteAllText(plistPath, plist.WriteToString());

			// As of Unity 5.4.3 project signing in XCode can be specified directly in Unity
			// under File > Build Settings, choose iOS Player, click on Player Settings, under
			// Other Settings you can specify the Automatic Signing Team ID. (Note that this is *not* 
			// your Apple ID. The Team ID can be obtained at http://developer.apple.com, under Membership

			            //EmbedFramework
            string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
            string targetGuid = proj.GetUnityMainTargetGuid();
            const string defaultLocationInProj = "Plugins/KeenASR/iOS";
            const string coreFrameworkName = "KeenASR.framework";
            string framework = Path.Combine(defaultLocationInProj, coreFrameworkName);
            string fileGuid = proj.AddFile(framework, "Frameworks/" + framework, PBXSourceTree.Sdk);
//            PBXProjectExtensions.AddFileToEmbedFrameworks(proj, targetGuid, fileGuid);
//            proj.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
            proj.WriteToFile (projPath);
            //EmbedFrameworks end


		}
	}
}
#endif

