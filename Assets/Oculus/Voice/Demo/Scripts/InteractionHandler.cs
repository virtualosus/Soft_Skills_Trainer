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
using System.Collections;



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

        public bool buttonPressed, tryAgainRunning;

        Coroutine tryAgain, YARNVoiceAttempt;

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

        public void ToggleActivation()
        {
            if (appVoiceExperience.Active)
            {
                appVoiceExperience.Deactivate();
                ClassroomSpeechToYarn.indicator.SetActive(false);
                StopCoroutine(tryAgain);
                StopCoroutine(YARNVoiceAttempt);
                textArea.text = freshStateText;

            }
            else
            {
                appVoiceExperience.Activate();
                textArea.text = "I'm listening...";
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

                StopCoroutine(YARNVoiceAttempt);
                if (tryAgainRunning)
                {
                    StopCoroutine(tryAgain);
                    tryAgainRunning = false;
                }

                ClassroomSpeechToYarn.indicator.SetActive(false);
                ClassroomSpeechToYarn.PlayerFinishedTalking.Invoke();

                previouslySpokenList.Add(counter + ". " + response["text"]);        //debugging
                DisplayPreviouslySpokenList();                                      //debugging
            }
            else
            {
                if (!buttonPressed)
                {
                    ClassroomSpeechToYarn.indicator.SetActive(false);
                    tryAgain = StartCoroutine(NothingHeardRetry());
                }
                else
                {
                    ClassroomSpeechToYarn.indicator.SetActive(false);
                }
            }
        }

        public void OnError(string error, string message)
        {
            textArea.text = $"<color=\"red\">Error: {error}\n\n{message}</color>";
        }



        public void YarnVoiceAttempt()
        {
            YARNVoiceAttempt = StartCoroutine(AttemptActivation());
        }

        public void CancelVoiceAttempt()
        {
            appVoiceExperience.Deactivate();
            ClassroomSpeechToYarn.indicator.SetActive(false);
            if (tryAgainRunning)
            {
                StopCoroutine(tryAgain);
                tryAgainRunning = false;
            }
            StopCoroutine(YARNVoiceAttempt);
            textArea.text = "If you prefer, you can read the option you want to select out loud, or continue to press the option.";

        }

        public IEnumerator AttemptActivation()
        {
            ClassroomSpeechToYarn.indicator.SetActive(true);
            appVoiceExperience.Activate();
            textArea.text = "I'm listening...";
            yield return new WaitForSeconds(8f);
            appVoiceExperience.Deactivate();
            ClassroomSpeechToYarn.indicator.SetActive(false);
            tryAgain = StartCoroutine(NothingHeardRetry());
        }

        public IEnumerator NothingHeardRetry()
        {
            tryAgainRunning = true;
            textArea.text = "Sorry, I didn't hear a recognised response. Trying again in 3...";
            yield return new WaitForSeconds(1f);
            textArea.text = "Sorry, I didn't hear a recognised response. Trying again in 2...";
            yield return new WaitForSeconds(1f);
            textArea.text = "Sorry, I didn't hear a recognised response. Trying again in 1...";
            yield return new WaitForSeconds(1f);
            ClassroomSpeechToYarn.ActivateVoiceRecognition();
            tryAgainRunning = false;
        }

        public IEnumerator ButtonPressed()
        {
            buttonPressed = true;
            yield return new WaitForSeconds(1);
            buttonPressed = false;
        } 
    }
}
