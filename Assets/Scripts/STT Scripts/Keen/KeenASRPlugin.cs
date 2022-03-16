//  Created by Ognjen Todic <ogi@keenresearch.com>
//  Copyright © 2018 Keen Research LLC. All rights reserved.
//
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace KeenResearch  {
	interface KeenASRPlugin {

		bool StartListening();

		void StopListening();

		bool PrepareForListeningWithCustomDecodingGraph(string dgName);

		bool CreateCustomDecodingGraphFromSentences (string dgName, string[] sentences);
			
		bool CustomDecodingGraphWithNameExists(string dgName);

		bool SetVADParameter (int parameter, float value);

//		static void SetLogLevel(int logLevel);

		void AdaptToSpeakerWithName (string speakerName);

		void ResetSpeakerAdaptation ();

		void SaveSpeakerAdaptationProfile ();

		bool RemoveAllSpeakerAdaptationProfiles ();

		bool RemoveSpeakerAdaptationProfiles (string speakerName);

		// Audio Recording Management
		void SetCreateAudioRecordings(bool value);

		string GetRecordingsDir ();

		string GetLastRecordingFilename ();

		void SetCreateJSONMetadata (bool value);

		string GetLastJSONMetadataFilename ();

		bool IsEchoCancellationAvaialable ();

		void PerformEchoCancellation (bool value);

		int GetRecognizerState();

		// Misc
		float InputLevel();

		bool IsListening ();

		// Uploader -- all of these will be static methods
//		bool CreateDataUploadThread(string appKey);
//		void SetRemoveDataAfterUpload (bool value);
//		void PauseUpload()
//		bool ResumeUpload();
//		bool IsUploadPaused ();
					
	}
}

