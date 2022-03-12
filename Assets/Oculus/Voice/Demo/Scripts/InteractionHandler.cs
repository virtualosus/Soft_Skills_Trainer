/**************************************************************************************************
 * Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.
 *
 * Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
 * under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 **************************************************************************************************/

using Facebook.WitAi;
using Facebook.WitAi.Lib;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



namespace Oculus.Voice.Demo.UIShapesDemo
{
    public class InteractionHandler : MonoBehaviour
    {
        [Header("Default States"), Multiline]
        [SerializeField] private string freshStateText;

        [Header("UI")]
        [SerializeField] private Text textArea, previouslySpokenListText;
        [SerializeField] private bool showJson;

        [Header("Voice")]
        [SerializeField] private AppVoiceExperience appVoiceExperience;

        public YarnCommandController ClassroomSpeechToYarn;

        public SpeechToOptionCompare speechToOptionCompare;

        private string pendingText;

        public List<string> previouslySpokenList = new List<string>();

        public string previouslySpokenString;

        private int counter;

        public string[] previouslySpokenArray;

        private void Start()
        {
            textArea.text = pendingText;
        }

        private void OnEnable()
        {
            appVoiceExperience.events.OnRequestCreated.AddListener(OnRequestStarted);
        }

        private void OnDisable()
        {
            appVoiceExperience.events.OnRequestCreated.RemoveListener(OnRequestStarted);
        }

        private void OnRequestStarted(WitRequest r)
        {
            // The raw response comes back on a different thread. We store the
            // message received for display on the next frame.
            if (showJson) r.onRawResponse = (response) => pendingText = response;
        }

        private void Update()
        {
            if (null != pendingText)
            {
                textArea.text = pendingText;
                pendingText = null;
            }
        }

        void DisplayPreviouslySpokenList()
        {
            if (previouslySpokenList == null)
            {
                previouslySpokenListText.text = "Nothing spoken yet.";
            }
            else
            {
                previouslySpokenArray = previouslySpokenList.ToArray();
                previouslySpokenString = string.Join("\n", previouslySpokenArray);
                previouslySpokenListText.text = previouslySpokenString;
                counter++;
            }
        }

        public void OnResponse(WitResponseNode response)
        {
            if (!string.IsNullOrEmpty(response["text"]))
            {
                textArea.text = "I heard: " + response["text"];
                speechToOptionCompare.currentLine = response["text"];
                speechToOptionCompare.LineComparison();
                previouslySpokenList.Add(counter + ". " + response["text"]);
                DisplayPreviouslySpokenList();
                ClassroomSpeechToYarn.indicator.SetActive(false);
                ClassroomSpeechToYarn.PlayerFinishedTalking.Invoke();
            }
            else
            {
                ClassroomSpeechToYarn.indicator.SetActive(false);
                StartCoroutine(NothingHeardRetry());
            }
        }

        public void OnError(string error, string message)
        {
            textArea.text = $"<color=\"red\">Error: {error}\n\n{message}</color>";
        }

        public void ToggleActivation()
        {
            if (appVoiceExperience.Active) 
            {
                appVoiceExperience.Deactivate();
                textArea.text = freshStateText;

            }
            else
            {
                appVoiceExperience.Activate();
                textArea.text = "I'm listening...";
            }
        }

        public System.Collections.IEnumerator AttemptActivation()
        {
            appVoiceExperience.Activate();
            textArea.text = "I'm listening...";
            yield return new WaitForSeconds(10f);
            appVoiceExperience.Deactivate();
        }

        public System.Collections.IEnumerator NothingHeardRetry()
        {
            textArea.text = "Sorry, I didn't hear a recognised response. Trying again in 3...";
            yield return new WaitForSeconds(1f);
            textArea.text = "Sorry, I didn't hear a recognised response. Trying again in 2...";
            yield return new WaitForSeconds(1f);
            textArea.text = "Sorry, I didn't hear a recognised response. Trying again in 1...";
            yield return new WaitForSeconds(1f);
            ClassroomSpeechToYarn.ActivateVoiceRecognition();
        }
    }
}
