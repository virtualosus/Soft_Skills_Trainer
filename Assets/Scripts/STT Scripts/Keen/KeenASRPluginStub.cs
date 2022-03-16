//  Created by Ognjen Todic <ogi@keenresearch.com>
//  Copyright © 2018 Keen Research LLC. All rights reserved.
//
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Linq;


namespace KeenResearch {
	/// <summary>
	/// KeenASR plugin stub class (used for non-supported platforms).
	/// </summary>
	public class KeenASRPluginStub : KeenASRPlugin {
		
		#region Methods

		public KeenASRPluginStub(string bundleName) {
			Debug.LogWarning ("Stub KeenASRPluginStub called with bundle name: " + bundleName);
		}

		public bool StartListening() {
			Debug.LogWarning("Stub KeenASRPluginStub.StartListening() called");
			return true;
		}

		public void StopListening() {
			Debug.LogWarning("Stub KeenASRPluginStub.StopListening() called");
		}

		public bool PrepareForListeningWithCustomDecodingGraph(string dgName) {
			Debug.LogWarning("Stub KeenASRPluginStub.PrepareForListening() called with dgName: " + dgName);
			return true;
		}

		public bool CreateCustomDecodingGraphFromSentences(string dgName, string[] sentences) {
			Debug.LogWarning("Stub KeenASRPluginStub.CreateCustomDecodingGraphFromSentences() called with dgName: " + dgName);
			return true;
		}

		public bool CustomDecodingGraphWithNameExists(string dgName) {
			Debug.LogWarning("Stub KeenASRPluginStub.CustomDecodingGraphWithNameExists() called with dgName: " + dgName);
			return true;
		}

		public bool SetVADParameter(int parameter, float value) {
			Debug.LogWarning("Stub KeenASRPluginStub.SetVADParameter() called");
			return true;
		}

		public static void SetLogLevel(int logLevel) {
			Debug.LogWarning("Stub KeenASRPluginStub.SetLogLevel() called");
		}

		// Adaptation
		public void AdaptToSpeakerWithName(string speakerName) {
			Debug.LogWarning("Stub KeenASRPluginStub.AdaptToSpeakerWithName() called with speakerName " + speakerName);
		}

		public void ResetSpeakerAdaptation () {
			Debug.LogWarning("Stub KeenASRPluginStub.ResetSpeakerAdaptation() called");
		}

		public void SaveSpeakerAdaptationProfile() {
			Debug.LogWarning("Stub KeenASRPluginStub.SaveSpeakerAdaptationProfile() called");
		}

		public bool RemoveAllSpeakerAdaptationProfiles () {
			Debug.LogWarning("Stub KeenASRPluginStub.RemoveAllSpeakerAdaptationProfiles() called");
			return true;
		}

		public bool RemoveSpeakerAdaptationProfiles(string speakerName) {
			Debug.LogWarning("Stub KeenASRPluginStub.RemoveSpeakerAdaptationProfiles() called with speakerName " + speakerName);
			return true;
		}

		// Audio Recording Management
		public void SetCreateAudioRecordings(bool value) {
			Debug.LogWarning("Stub KeenASRPluginStub.SetCreateAudioRecordings() setting to " + value);
		}
			
		public string GetRecordingsDir() {
			Debug.LogWarning("Stub KeenASRPluginStub.GetRecordingsDir()");
			return "";
		}

		public string GetLastRecordingFilename() {
			Debug.LogWarning("Stub KeenASRPluginStub.GetLastRecordingFilename()");
			return "";
		}

		public void SetCreateJSONMetadata (bool value) {
			Debug.LogWarning("Stub KeenASRPluginStub.SetCreateJSONMetadata()");
		}

		public string GetLastJSONMetadataFilename () {
			Debug.LogWarning("Stub KeenASRPluginStub.GetLastJSONMetadataFilename()");
			return "X";
		}

		public bool IsEchoCancellationAvaialable () {
			Debug.LogWarning("Stub KeenASRPluginStub.IsEchoCancellationAvaialable()");
			return false;
		}

		public void PerformEchoCancellation (bool value) {
			Debug.LogWarning("Stub KeenASRPluginStub.setEchoCancellation()");
		}

		public int GetRecognizerState() {
			Debug.LogWarning("Stub KeenASRPluginStub.GetRecognizerState()");
			return 0;
		}

		// Misc
		public float InputLevel() {
			Debug.LogWarning("Stub KeenASRPluginStub.InputLevel()");
			return 0.0f;
		}

		public bool IsListening() {
			Debug.LogWarning("Stub KeenASRPluginStub.IsListening()");
			return true;
		}
			
		public static bool CreateDataUploadThread(string appKey) {
			Debug.LogWarning("Stub KeenASRPluginStub.CreateDataUploadThread()");
			return true;
		}

		public static void SetRemoveDataAfterUpload (bool value) {
			Debug.LogWarning("Stub KeenASRPluginStub.SetRemoveDataAfterUpload()");
		}

		public static void PauseUpload() {
			Debug.LogWarning("Stub KeenASRPluginStub.PauseUpload()");
		}

		public static bool ResumeUpload() {
			Debug.LogWarning("Stub KeenASRPluginStub.ResumeUpload()");
			return true;
		}

		public static bool IsUploadPaused () {
			Debug.LogWarning("Stub KeenASRPluginStub.IsUploadPaused()");
			return true;
		}

		#endregion
	}
}