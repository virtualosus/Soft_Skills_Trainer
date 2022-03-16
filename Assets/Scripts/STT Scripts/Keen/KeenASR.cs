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


namespace KeenResearch 
{
	[System.Serializable]
	public class ASRWord {
		public string text;
		public double confidence;
		public double startTime;
		public double duration;
		public bool isTag;
	}

	[System.Serializable]
	public class ASRResult {
		public string text;
		public string cleanText;
		public double confidence;
		public ASRWord[] words;

		public static ASRResult CreateFromJSON(string jsonString) {
			return JsonUtility.FromJson<ASRResult>(jsonString);
		}
	}

	/// <summary>
	/// The primary manager class for the KeenASR plugin.
	/// </summary>
	public class KeenASR {
		public KeenASR() {
			Debug.Log("Initializing KeenASR object");
		}
        public static bool isInitialized = false;

        /// <summary>
        /// The vad parameter timeout for no speech. Use this parameter to set the number of 
        /// seconds after which the SDK automatically stops listening if there was no speech 
        /// at all in the signal
        /// </summary>
        public const int VadParamTimeoutForNoSpeech=0;

		/// <summary>
		/// The vad parameter timeout for end silence for a good match. Use this parameter to set
		/// the number of seconds after which the SDK automatically stops listening after it has 
		/// detected some speech (and it has high confidence in the result).
		/// If this value is too short, users may be cut-off (when engine stops listening) as soon
		/// as they make a short pause. If it is too long, the system will appear as non-responsive 
		/// since it waits for a long silence before it ends listening. Furthermore, users may start 
		/// speaking again, if they find the system unresponsive. Good values are typically between 
		/// 0.6sec and 1.2sec (former when single-word commands are targetted; latter for phrases and 
		/// longer responses. Note that you may set VAD values while the recognizer is listening, e.g.
		/// from PartialASRResultHandler.
		/// </summary>
		public const int VadParamTimeoutEndSilenceForGoodMatch=1;

		/// <summary>
		/// The vad parameter timeout for end silence for any kind of match. Similar to
		/// VadParamTimeoutEndSilenceForGoodMatch, but you may want to set this to slightly longer
		/// value so that low-confidence results have a bit input audio before timing out.
		/// 
		/// </summary>
		public const int VadParamTimeoutEndSilenceForAnyMatch=2;

		/// <summary>
		/// The vad parameter timeout for maximum duration. Use this parameter to set the number of 
		/// seconds after which the SDK automatically stops listening (regardless of the presence
		/// of speech). This paremeter controls the maximum duration for a single listening session
		/// </summary>
		public const int VadParamTimeoutMaxDuration=3;

		/** Log debug messages and higher */
		public const int LogLevelDebug = 0;
		/** Log info messages and higher */
		public const int LogLevelInfo = 1;
		/** Log only warnings or errors (default level)*/
		public const int LogLevelWarn = 2;

		/// <summary>
		///  Recognizer has been initialized but it needs decoding graph before it can start listening.
		/// </summary>
		public const int RecognizerStateNeedsDecodingGraph = 0;

		/// <summary>
		/// Recognizer is ready to start listening.
		/// </summary>
		public const int RecognizerStateReadyToListen = 1;

		/// <summary>
		///  Recognizer is actively listening.
		/// </summary>
		public const int RecognizerStateListening = 2;

		/// <summary>
		/// Recognizer is not processing incoming audio any more and is computing the final hypothesis.
		/// This state should not last more than 200-300ms. Longer times in this state indicate that SDK
		/// may be running on devices that are not capabable to perform processing in real time or misconfiguration
		/// of the SDK.
		/// </summary>
		public const int RecognizerStateFinalProcessing = 3;

		public delegate void InitializedHandler(bool status);
		public delegate void PartialASRResultHandler(string text); // TODO - ASRResult object instead of text
		public delegate void FinalASRResultHandler(ASRResult result);
		public delegate void UnwindAppAudioBeforeAudioInterruptHandler();
		public delegate void RecognizerReadyToListenAfterInterruptHandler();

		/// <summary>
		/// Occurs when the underlying ASR engine is fully initialized.
		/// </summary>
		public static event InitializedHandler onInitializedReceived;

		/// <summary>
		/// Occurs when partial result is received for a given instance of the recognizer.
		/// </summary>
		public event PartialASRResultHandler onPartialASRResultReceived;

		/// <summary>
		/// Occurs when final result is received for a given instance of the recognizer.
		/// </summary>
		public event FinalASRResultHandler onFinalASRResultReceived;

		/// <summary>
		/// This method is called when audio interrupt occurs. It will be called in synchronous
		/// manner immediately before KeenASR starts unwinding its audio stack. You
		/// would use this method to stop playing any audio that is controlled directly by
		/// your app. Your app should not modify underlying audio session state nor interact with the
		/// recognizer at this point.
		/// 
		/// @warning NOTE: this method will be called during app wind-down when audio
		/// interrupt occurs or the app goes to the background. It is crucial that this method
		/// performs quickly, otherwise KIOSRecognizer may not have sufficient time to properly
		/// unwind its audio stack before the app goes to background.
		/// </summary>
		public event UnwindAppAudioBeforeAudioInterruptHandler onUnwindAppAudioBeforeAudioInterruptReceived;

		/// <summary>
		/// Occurs when on recognizer ready to listen after the app comes to foreground. You would use this
		/// method to setup UI.
		/// </summary>
		public event RecognizerReadyToListenAfterInterruptHandler onRecognizerReadyToListenAfterInterruptReceived;


		/// <summary>
		/// Gets the shared KeenASR instance.
		/// </summary>
		/// <value>The shared KeenASR instance.</value>
		public static KeenASR Instance {
			get {
//				if (KeenASR.sharedInstance == null) {
//					;
//					Debug.LogWarning ("Exiting because KeenASR instance is null " + KeenASR.sharedInstance);
//					Application.Quit ();
//				}
				return KeenASR.sharedInstance;
			}
		}
		/// <summary>
		/// Initialize Keen ASR SDK with the ASR Bundle stored in the bundleName. After the SDK is initialized,
		/// InitializedHandler will be called.
		/// 
		/// On Android platform this call with be run asynchroniously whereas on iOS it will run synchroniosly.
		/// On all platforms InitializedHandler will be called which you can use to continue with the setup of
		/// decoding graphs and other resources, assuming SDK was succesfully initialized.
		/// </summary>
		/// <param name="bundleName">name of the ASR bundle.</param>
		/// <returns><c>true</c>, if initializaitin was succesful, <c>false</c> otherwise.</returns>
		public static bool Initialize(string bundleName) {
			if (KeenASR.sharedInstance!=null && KeenASR.sharedInstance.plugin != null) {
				Debug.LogWarning("ASR engine has already been initialized. Ignoring init with bundle " + bundleName + "'");
				return true;
			}
			//sharedInstance.plugin = KeenASRPlugin;
			KeenASR.sharedInstance = new KeenASR();
			Debug.LogWarning ("Created new sharedInstance " + KeenASR.sharedInstance);
			bool retCode = KeenASR.sharedInstance.Init(bundleName);
			Debug.LogWarning ("Initialized with the bundle " + bundleName);

			return retCode;
		}
			
		/// <summary>
		/// Creates custom decoding graph from the list of sentences/phrases.
		/// </summary>
		/// <returns><c>true</c>, if custom decoding graph was created, <c>false</c> otherwise.</returns>
		/// <param name="dgName">Name of the custom decoding graph (you can use this name later on to reference the decoding graph).</param>
		/// <param name="sentences">An array of strings, each one defining the phrase that's likely to be said by the user.</param>
		public bool CreateCustomDecodingGraphFromSentences(string dgName, string[] sentences) {
			if (this.plugin == null) {
				Debug.LogWarning ("No ASR engine instance. Did you call Initialize?");
				return false;
			}
			return plugin.CreateCustomDecodingGraphFromSentences (dgName, sentences);
		}


		/// <summary>
		/// Prepares for listening with custom decoding graph, which has been previously
		/// created with CreateCustomDecodingGraphFromSentences method.
		/// </summary>
		/// <returns><c>true</c>, if succesful, <c>false</c> otherwise.</returns>
		/// <param name="dgName">Dg name.</param>
		public bool PrepareForListeningWithCustomDecodingGraph(string dgName) {
			if (this.plugin == null) {
				Debug.LogWarning ("No ASR engine instance. Did you call Initialize?");
				return false;
			}
			return plugin.PrepareForListeningWithCustomDecodingGraph (dgName);
		}

		/// <summary>
		/// Checks if the custom decoding graph with the given name exists.
		/// </summary>
		/// <returns><c>true</c>, if decoding graph exists, <c>false</c> otherwise.</returns>
		/// <param name="dgName">Name of the custom decoding graph created 
		/// via CreateCustomDecodingGraphFromSentences .</param>
		public bool CustomDecodingGraphWithNameExists(string dgName) {
			return plugin.CustomDecodingGraphWithNameExists (dgName);
		}


		/// <summary>
		/// Starts listening.
		/// </summary>
		/// <returns><c>true</c>, if listening was started succesfully, <c>false</c> otherwise.</returns>
		public bool StartListening() {
			if (this.plugin==null) {
				Debug.LogWarning ("No ASR engine instance. Did you call Initialize?");
				return false;
			}
			return plugin.StartListening ();
		}

		/// <summary>
		/// Stops listening.
		/// </summary>
		public void StopListening() {
			if (this.plugin==null) {
				Debug.LogWarning ("No ASR engine instance. Did you call Initialize?");
				return;
			}
			plugin.StopListening ();
		}
			

		/// <summary>
		/// Sets one of the Voice Activity Detection parameters which are used to automatically
		/// stop listening.
		/// </summary>
		public bool SetVADParameter(int vadParameter, float value) {
			if (this.plugin == null) {
				Debug.LogWarning ("No ASR engine instance. Did you call Initialize?");
				return false;
			}
			return plugin.SetVADParameter (vadParameter, value);
		}

		/// <summary>
		/// Set framework logging level.
		/// </summary>
		/// <param name="logLevel">Log level for logging. One of LogLevelDebug, LogLevelInfo, 
		/// LogLevelWarn. Default is LogLevelWarn</param>
		public static void SetLogLevel(int logLevel) {
			if (Application.isEditor) {
				Debug.Log ("Calling SetLogLevel stub method");
			} else {
#if UNITY_IPHONE
				KeenASRPluginIOS.SetLogLevel (logLevel);
#elif UNITY_ANDROID
				KeenASRPluginAndroid.SetLogLevel (logLevel);
#endif
			// add other platforms as they become available
			}

		}

		/// <summary>
		/// Starts adaptation for the user with the specified (pseudo) name. If previous profile
		/// exists for this user, it will be used. If you know with high confidence that specific user
		/// will be using the system, you can use this method; if the system can be used in shared environment,
		/// it is better not to use the adaptation.
		/// </summary>
		/// <param name="speakerName">(pseudo) name of the user.</param>
		public void AdaptToSpeakerWithName (string speakerName) {
			plugin.AdaptToSpeakerWithName (speakerName);
		}

		/// <summary>
		/// Resets the speaker adaptation.
		/// </summary>
		public void ResetSpeakerAdaptation () {
			plugin.ResetSpeakerAdaptation ();
		}

		/// <summary>
		/// Saves the speaker adaptation profile in the system, so it can be used on subsequent calls
		/// if AdaptToSpeakerWithName was called.
		/// </summary>
		public void SaveSpeakerAdaptationProfile () {
			plugin.SaveSpeakerAdaptationProfile ();
		}

		/// <summary>
		/// Removes all speaker adaptation profiles from the device.
		/// </summary>
		/// <returns><c>true</c>, if all speaker adaptation profiles were removed, <c>false</c> otherwise.</returns>
		public bool RemoveAllSpeakerAdaptationProfiles () {
			return plugin.RemoveAllSpeakerAdaptationProfiles ();
		}

		/// <summary>
		/// Removes the speaker adaptation profiles for the specified user
		/// </summary>
		/// <returns><c>true</c>, if speaker adaptation profiles was removed, <c>false</c> otherwise.</returns>
		/// <param name="speakerName">Speaker name.</param>
		public bool RemoveSpeakerAdaptationProfiles (string speakerName) {
			return plugin.RemoveSpeakerAdaptationProfiles (speakerName);
		}

		// Audio Recording Management
		/// <summary>
		/// Enable storing of audio files on the device. If set to true, all audio captured between
		/// startListening and stopListening will be saved on the device. You can use GetLastRecordingFilename
		/// method to obtain the full path to the last audio recording. 
		/// </summary>
		/// <param name="value">If set to <c>true</c> audio recordings will be saved on the device.</param>
		public void SetCreateAudioRecordings(bool value) {
			plugin.SetCreateAudioRecordings (value);
		}
			

		/// <summary>
		///  Directory in which audio recordings will be saved.
		/// </summary>
		/// <returns>The recordings dir.</returns>
		public string GetRecordingsDir () {
			return plugin.GetRecordingsDir ();
		}

		/// <summary>
		/// Gets the full path to the last audio recording
		/// </summary>
		/// <returns>The last recording filename.</returns>
		public string GetLastRecordingFilename () {
			return plugin.GetLastRecordingFilename ();
		}

		/// <summary>
		/// Sets the create JSON metadata flag.
		/// </summary>
		/// <param name="value">If set to <c>true</c> value SDK will create JSON 
		/// file with speech recognition metadata in the filesystem.</param> 
		public void SetCreateJSONMetadata(bool value) {
			plugin.SetCreateJSONMetadata (value);
		}

		/// <summary>
		/// Gets the last JSON metadata filename.
		/// </summary>
		/// <returns>The last JSON metadata filename.</returns>
		public string GetLastJSONMetadataFilename() {
			return plugin.GetLastJSONMetadataFilename ();
		}
		/// <summary>
		/// Determines whether device supports echo cancellation
		/// </summary>
		/// <returns><c>true</c> if this device supports echo cancellation; otherwise, <c>false</c>.</returns>
		public bool IsEchoCancellationAvaialable () {
			return plugin.IsEchoCancellationAvaialable();
		}

		/// <summary>
		/// Performs the echo cancellation.
		/// </summary>
		/// <param name="value">If set to <c>true</c> value audio being played via the app (on the device speaker)
		/// will be removed from the audio captured via the microphone.</param>
		public void PerformEchoCancellation (bool value) {
			plugin.PerformEchoCancellation(value);
		}

		public int GetRecognizerState() {
			return plugin.GetRecognizerState();
		}

		// Misc
		/// <summary>
		/// Returns the level (in dB) of the input audio signal captured via the mic. You can use 
		/// poll this method periodically and use the return value to drive the UI element in order
		/// to provide user with the visual indication that the system is hearing them.
		/// </summary>
		/// <returns>The level on the signal from the microphone in decibels.</returns>
		public float InputLevel() {
			return plugin.InputLevel ();
		}


		public static bool CreateDataUploadThread(string appKey) {
			#if UNITY_IPHONE
			return KeenASRPluginIOS.CreateDataUploadThread (appKey);
			#else
			Debug.LogWarning("CreateDateUploadThread is currently available only on iOS platform");
			return false;
			#endif
		}

		public static void SetRemoveDataAfterUpload(bool value) {
			#if UNITY_IPHONE
			KeenASRPluginIOS.SetRemoveDataAfterUpload(value);
			#else
			Debug.LogWarning("SetRemoveDataAfterUpload is currently available only on iOS platform");
			#endif
		}


		public static bool ResumeUpload() {
			#if UNITY_IPHONE
			return KeenASRPluginIOS.ResumeUpload();
			#else
			Debug.LogWarning("ResumeUpload is currently available only on iOS platform");
			return false;
			#endif
		}

		public static void PauseUpload() {
			#if UNITY_IPHONE
			KeenASRPluginIOS.PauseUpload();
			#else
			Debug.LogWarning("PauseUpload is currently available only on iOS platform");
			#endif
		}

		public static bool IsUploadPaused() {
			#if UNITY_IPHONE
			return KeenASRPluginIOS.IsUploadPaused();
			#else
			Debug.LogWarning("IsUploadPaused is currently available only on iOS platform");
			return false;
			#endif
		}


		/// <summary>
		/// Determines whether the SDK is listening for the incoming audio.
		/// </summary>
		/// <returns><c>true</c> if this instance is listening; otherwise, <c>false</c>.</returns>
		[Obsolete("This method is deprecated. Use GetRecognizerState() instead")]
		public bool IsListening () {
			return plugin.IsListening ();
		}





		// Private methods & members
		private static KeenASR sharedInstance = null;
//		private KeenASRListener listener;
		private KeenASRPlugin plugin = null;

		private bool Init(string bundleName) {
			if (Application.isEditor) {
				this.plugin = new KeenASRPluginStub(bundleName);
			} else {
				#if UNITY_ANDROID
				this.plugin = new KeenASRPluginAndroid (bundleName);
				#elif UNITY_IOS
				this.plugin = new KeenASRPluginIOS(bundleName);
				#else
				this.plugin = new KeenASRPluginStub(bundleName);
				#endif
			}

			if (plugin == null) {
				Debug.LogWarning ("Unable to create KeenASRPlugin");
				return false;
			}
			GameObject gameObject = new GameObject("[KeenASRListener]");
			gameObject.AddComponent<KeenASRListener>();
			MonoBehaviour.DontDestroyOnLoad(gameObject);
			return true;
		}


		private class KeenASRListener : MonoBehaviour {

			private void Initialized(string status) {
				if ("1".Equals (status))
					isInitialized = true;

				InitializedHandler handler = KeenASR.onInitializedReceived;
				if (handler == null) {
					if (! isInitialized) {
						Debug.LogWarning ("KeenASR was not properly initialized and onInitialized event is NOT handled");
					}
					return;
				}
				handler (isInitialized);
			}
			
			private void PartialASRResult(string result) {
				PartialASRResultHandler handler = KeenASR.Instance.onPartialASRResultReceived;

				if (handler == null) {
					Debug.LogWarning("Unable to obtain handler for the partial result callback");
					return;
				}
				handler (result);
			}

			private void FinalASRResult(string jsonResult) {
				FinalASRResultHandler handler = KeenASR.Instance.onFinalASRResultReceived;

				if (handler == null) {
					Debug.Log ("NUnable to obrain handler for the the final result callback.");
					return;
				}
				// convert json to our representation
				Debug.Log(jsonResult);
				ASRResult result = ASRResult.CreateFromJSON(jsonResult);
				handler (result);
			}

			private void RecognizerReadyToListenAfterInterrupt() {
				Debug.Log ("Executing RecognizerReadyToListenAfterInterrupt");
				// todo execute callback
				RecognizerReadyToListenAfterInterruptHandler handler = KeenASR.Instance.onRecognizerReadyToListenAfterInterruptReceived;

				if (handler == null) {
					Debug.Log ("No handler for RecognizerReadyToListenAfterInterrupt is set in KeenASR.");
					return;
				}
				handler ();

			}

			private void UnwindAppAudioBeforeAudioInterrupt() {
				Debug.Log ("Executing unwindAppAudioBeforeAudioInterrupt");
				UnwindAppAudioBeforeAudioInterruptHandler handler = KeenASR.Instance.onUnwindAppAudioBeforeAudioInterruptReceived;

				if (handler == null) {
					Debug.Log ("No handler for unwindAppAudioBeforeAudioInterrupt is set in KeenASR.");
					return;
				}
				handler ();
			}

		}
	}
    
}