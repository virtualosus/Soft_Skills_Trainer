//  Created by Ognjen Todic <ogi@keenresearch.com>
//  Copyright © 2018 Keen Research LLC. All rights reserved.
//
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential

#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeenResearch {
	public class KeenASRPluginAndroid : KeenASRPlugin {
        private static AndroidJavaClass plugin;

		private static void InitBindingObject() {
			plugin = new AndroidJavaClass("com.keenresearch.keenasr.unity.KeenASRUnityBinding");
		}

        #region Methods
        public KeenASRPluginAndroid(string bundleName) {
			if (plugin == null)
				InitBindingObject ();

			var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
            Debug.Log("KeenASRAndroid: Initializing KeenASR with bundle name: " + bundleName);
            plugin.CallStatic("init", bundleName, jo);
        }
			
		public bool StartListening() {
//			Debug.Log("KeenASRAndroid: StartListening()");
			// check if plugin is non-null
			return plugin.CallStatic<bool>("startListening");
        }

        
		public void StopListening() {
//			Debug.Log("KeenASRAndroid: StopListening()");
            plugin.CallStatic("stopListening");
        }


        public bool PrepareForListeningWithCustomDecodingGraph(string dgName) {
            if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call PrepareForListeningWithCustomDecodingGraph()");
                return false;
            }
//			Debug.Log("KeenASRAndroid: PrepareForListeningWithCustomDecodingGraph() called with dgName: " + dgName);
            return plugin.CallStatic<bool>("prepareForListeningWithCustomDecodingGraph", dgName);
        }


        public bool CreateCustomDecodingGraphFromSentences(string dgName, string[] sentences) {
            if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call CreateCustomDecodingGraphFromSentences()");
                return false;
            }
//			Debug.Log("KeenASRAndroid: CreateCustomDecodingGraphFromSentences() called with dgName: " + dgName);
            return plugin.CallStatic<bool>("createCustomDecodingGraphFromSentences", dgName, sentences);
        }

        
		public bool CustomDecodingGraphWithNameExists(string dgName) {
            if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call CustomDecodingGraphWithNameExists()");
                return false;
            }
//			Debug.Log("KeenASRAndroid: CustomDecodingGraphWithNameExists() called with dgName: " + dgName);
            return plugin.CallStatic<bool>("customDecodingGraphWithNameExists", dgName);
        }


        public bool SetVADParameter(int parameter, float value) {
            if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call SetVADParameter()");
                return false;
            }
//			Debug.Log("KeenASRAndroid: SetVADParameter() called");
            return plugin.CallStatic<bool>("setVADParameter", parameter, value);
        }


        public static void SetLogLevel(int logLevel) {
//			Debug.Log("KeenASRAndroid: SetLogLevel() called");
			if (plugin == null)
				InitBindingObject ();
            plugin.CallStatic("setLogLevel", logLevel);
        }


		public void AdaptToSpeakerWithName(string speakerName) {
			//Debug.Log("KeenASRAndroid: AdaptToSpeakerWithName() called with speakerName " + speakerName);
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call AdaptToSpeakerWithName()");
				return;
			}
            plugin.CallStatic("adaptToSpeakerWithName", speakerName);
        }


		public void ResetSpeakerAdaptation() {
			//Debug.Log("KeenASRAndroid: ResetSpeakerAdaptation() called");
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call ResetSpeakerAdaptation()");
				return;
			}
            plugin.CallStatic("resetSpeakerAdaptation");
        }


        public void SaveSpeakerAdaptationProfile() {
        	if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call SaveSpeakerAdaptationProfile()");
                return;
            }
//			Debug.Log("KeenASRAndroid: SaveSpeakerAdaptationProfile() called");
            plugin.CallStatic("saveSpeakerAdaptationProfile");
        }

        public bool RemoveAllSpeakerAdaptationProfiles() {
            if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call RemoveAllSpeakerAdaptationProfiles()");
                return false;
            }
//			Debug.Log("KeenASRAndroid: RemoveAllSpeakerAdaptationProfiles() called");
            return plugin.CallStatic<bool>("removeAllSpeakerAdaptationProfiles");
        }

        public bool RemoveSpeakerAdaptationProfiles(string speakerName) {
//			Debug.Log("KeenASRAndroid: RemoveSpeakerAdaptationProfiles() called with speakerName " + speakerName);
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call RemoveSpeakerAdaptationProfiles()");
				return false;
			}
			return plugin.CallStatic<bool>("removeSpeakerAdaptationProfiles", speakerName);
        }

        // Audio Recording Management
        public void SetCreateAudioRecordings(bool value) {   
//			Debug.Log("KeenASRAndroid: SetCreateAudioRecordings() setting to " + value);
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call SetCreateAudioRecordings()");
				return;
			}
            plugin.CallStatic("setCreateAudioRecordings", value);
        }


        public string GetRecordingsDir() {   
//			Debug.Log("KeenASRAndroid: GetRecordingsDir()");
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call GetRecordingsDir()");
				return "";
			}
            return plugin.CallStatic<string>("getRecordingsDir");
        }

        public string GetLastRecordingFilename() {   
//			Debug.Log("KeenASRAndroid: GetLastRecordingFilename()");
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call GetLastRecordingFilename()");
				return "";
			}
            return plugin.CallStatic<string>("getLastRecordingFilename");
        }

		public void SetCreateJSONMetadata (bool value) {
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call SetCreateJSONMetadata()");
				return;
			}
			Debug.LogWarning ("SetCreateJSONMetadata is currently not supported in Android version of KeenASR SDK");
		}


		public string GetLastJSONMetadataFilename () {
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call GetLastJSONMetadataFilename()");
				return "";
			}
			Debug.LogWarning ("GetLastJSONMetadataFilename is currently not supported in Android version of KeenASR SDK");
			return "";
		}

        public bool IsEchoCancellationAvaialable() {
//			Debug.Log("KeenASRAndroid: IsEchoCancellationAvaialable()");
            return plugin.CallStatic<bool>("isEchoCancellationAvaialable");
        }

        
		public void PerformEchoCancellation(bool value) {   
//			Debug.Log("KeenASRAndroid: setEchoCancellation()");
            plugin.CallStatic("setEchoCancellation", value);
        }


        public int GetRecognizerState() {
			//			Debug.Log("KeenASRAndroid: GetRecognizerState()");
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call GetRecognizerState()");
				return -1;
			}
            return plugin.CallStatic<int>("getRecognizerState");
        }

        // Misc
        public float InputLevel() {   
//			Debug.Log("KeenASRAndroid: InputLevel()");
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call InputLevel()");
				return -200.0f;
			}
            return plugin.CallStatic<float>("getInputLevel");
        }

        public bool IsListening() {   
//			Debug.Log("KeenASRAndroid: IsListening()");
			if (!KeenASR.isInitialized) {
				Debug.Log("KeenASRAndroid: KeenASR is not initialized. Can't call IsListening()");
				return false;
			}
            return plugin.CallStatic<bool>("isListening");
        }

        #endregion
    }
}
#endif