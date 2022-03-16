//  Created by Ognjen Todic <ogi@keenresearch.com>
//  Copyright © 2018 Keen Research LLC. All rights reserved.
//
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential


#if UNITY_IPHONE
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Linq;


namespace KeenResearch {
	/// <summary>
	/// The primary manager class for the KeenASR plugin.
	/// </summary>
	public class KeenASRPluginIOS : KeenASRPlugin {
		[DllImport("__Internal")]
		extern static private bool _Init(string bundleName);
		[DllImport("__Internal")]
		extern static private bool _StartListening();
		[DllImport("__Internal")]
		extern static private void _StopListening();
		[DllImport("__Internal")]
		extern static private bool _PrepareForListeningWithCustomDecodingGraph(string dgName); 
		[DllImport("__Internal")]
		extern static private bool _CreateCustomDecodingGraphFromSentences (string dgName, string[] sentences, int numSentences);
		[DllImport("__Internal")]
		extern static private bool _CustomDecodingGraphWithNameExists (string dgName);
		[DllImport("__Internal")]
		extern static private bool _SetVADParameter (int vadParameter, float value);
		[DllImport("__Internal")]
		extern static private void _SetLogLevel (int logLevel);
		[DllImport("__Internal")]
		extern static private float _InputLevel();
		[DllImport("__Internal")]
		extern static private bool _IsListening ();

		[DllImport("__Internal")]
		extern static private void _AdaptToSpeakerWithName(string speakerName);
		[DllImport("__Internal")]
		extern static private void _ResetSpeakerAdaptation();
		[DllImport("__Internal")]
		extern static private void _SaveSpeakerAdaptationProfile();
		[DllImport("__Internal")]
		extern static private bool _RemoveAllSpeakerAdaptationProfiles();
		[DllImport("__Internal")]
		extern static private bool _RemoveSpeakerAdaptationProfiles(string speakerName);

		[DllImport("__Internal")]
		extern static private void _SetCreateAudioRecordings(bool value);
		[DllImport("__Internal")]
		extern static private IntPtr _GetRecordingsDir();
		[DllImport("__Internal")]
		extern static private IntPtr _GetLastRecordingFilename();
		[DllImport("__Internal")]
		extern static private void _SetCreateJSONMetadata (bool value);

		[DllImport("__Internal")]
		extern static private IntPtr _GetLastJSONMetadataFilename();


		[DllImport("__Internal")]
		extern static private bool _IsEchoCancellationAvailable();
		[DllImport("__Internal")]
		extern static private void _PerformEchoCancellation(bool value);
		[DllImport("__Internal")]
		extern static private int _GetRecognizerState();

		[DllImport("__Internal")]
		extern static private bool _CreateDataUploadThread(string appKey);
		[DllImport("__Internal")]
		extern static private void _SetRemoveDataAfterUpload (bool value);
		[DllImport("__Internal")]
		extern static private void _PauseUpload ();
		[DllImport("__Internal")]
		extern static private bool _ResumeUpload ();
		[DllImport("__Internal")]
		extern static private bool _IsUploadPaused ();



		#region Methods

		public KeenASRPluginIOS(string bundleName) {
			// TODO init
			_Init(bundleName);
		}
			
		public bool StartListening() {
			return _StartListening ();
		}

		public void StopListening() {
			_StopListening ();
		}

		public bool PrepareForListeningWithCustomDecodingGraph(string dgName) {
			return _PrepareForListeningWithCustomDecodingGraph(dgName);
		}

		public bool CreateCustomDecodingGraphFromSentences(string dgName, string[] sentences) {
			return _CreateCustomDecodingGraphFromSentences(dgName, sentences, sentences.Length);
		}

		public bool CustomDecodingGraphWithNameExists(string dgName) {
			return _CustomDecodingGraphWithNameExists(dgName);
		}

		public bool SetVADParameter(int parameter, float value) {
			return _SetVADParameter (parameter, value);
		}

		public static void SetLogLevel(int logLevel) {
			_SetLogLevel (logLevel);
		}

		// Adaptation
		public void AdaptToSpeakerWithName(string speakerName) {
			_AdaptToSpeakerWithName (speakerName);
		}

		public void ResetSpeakerAdaptation () {
			_ResetSpeakerAdaptation ();
		}

		public void SaveSpeakerAdaptationProfile() {
			_SaveSpeakerAdaptationProfile ();
		}

		public bool RemoveAllSpeakerAdaptationProfiles () {
			return _RemoveAllSpeakerAdaptationProfiles ();
		}

		public bool RemoveSpeakerAdaptationProfiles(string speakerName) {
			return _RemoveSpeakerAdaptationProfiles (speakerName);
		}

		// Audio Recording Management
		public void SetCreateAudioRecordings(bool value) {
			_SetCreateAudioRecordings (value);
		}
			
		public string GetRecordingsDir() {
			IntPtr ptr = _GetRecordingsDir ();
			return Marshal.PtrToStringAnsi (ptr);		
		}
			
		public string GetLastRecordingFilename() {
			IntPtr ptr = _GetLastRecordingFilename ();
//			if (ptr != null)
				return Marshal.PtrToStringAnsi (ptr);
//			return "";
		}

		public void SetCreateJSONMetadata (bool value) {
			_SetCreateJSONMetadata (value);
		}

		public string GetLastJSONMetadataFilename () {
			IntPtr ptr = _GetLastJSONMetadataFilename ();
			return Marshal.PtrToStringAnsi (ptr);
		}


		public bool IsEchoCancellationAvaialable () {
			return _IsEchoCancellationAvailable();
		}

		public void PerformEchoCancellation (bool value) {
			 _PerformEchoCancellation(value);
		}

		public int GetRecognizerState() {
			return _GetRecognizerState();
		}

		// Misc
		public float InputLevel() {
			return _InputLevel ();
		}

		public bool IsListening() {
			return _IsListening ();
		}

		public static bool CreateDataUploadThread(string appKey) {
			return _CreateDataUploadThread(appKey);
		}

		public static void SetRemoveDataAfterUpload (bool value) {
			_SetRemoveDataAfterUpload (value);
		}

		public static void PauseUpload() {
			_PauseUpload ();
		}

		public static bool ResumeUpload() {
			return _ResumeUpload ();
		}

		public static bool IsUploadPaused () {
			return _IsUploadPaused ();
		}


//		public GameObject Listener {
//			set {
////				UAUnityPlugin_setListener (value.name);
//			}
//		}

		#endregion
	}
}
#endif
