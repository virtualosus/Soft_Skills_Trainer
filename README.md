# Virtual Reality Soft Skills Trainer - Restorative Justice for Teacher Training UK

This Virtual Reality project is a demonstration of a number of Artificial Interlligence elements brought together in Unity for the purpose of training and developement focusing on Soft Skills in the area of Restorative Justice for use in Teacher Training.

Set up for use on the  Meta/Oculus Quest 2, this project can be used as a template for any training application in VR. Follow the below guidance on how to amend the project to your needs.

### Artifical Intelligence Elements

* [Microsoft Azure Text to Speech](https://github.com/2030428/LawAndOrder/blob/master/README.md#microsoft-azure-text-to-speech).
* [Meta/Oculus Voice Recognition (Wit.ai)](https://github.com/2030428/LawAndOrder/blob/master/README.md#metaoculus-voice-recognition-witai)


### NPC and Dialogue

* [Yarn Spinner 2.0](https://github.com/2030428/LawAndOrder/blob/master/README.md#yarn-spinner-20)
* [Meta/Oculus Lip Sync](https://github.com/2030428/LawAndOrder/blob/master/README.md#metaoculus-lip-sync)
* [Rocketbox](https://github.com/2030428/LawAndOrder/blob/master/README.md#rocketbox)
* [Mixamo](https://github.com/2030428/LawAndOrder/blob/master/README.md#mixamo)

### Adapting for Your Project

* What you'll need to do
* Essential elements

## Microsoft Azure Text to Speech

The Microsoft TTS setup is based on [Active Nick's Unity-Test-To-Speech project](https://github.com/ActiveNick/Unity-Text-to-Speech). By adapting his Speech Manager script, it is possible to call realtime TTS from the Microsoft Azure Cognitive Services.

To implement this in your project, you will need to set up (at least) a [free account with the Azure services](https://azure.microsoft.com/en-us/free/cognitive-services/) which will permit up to 5000 requests per month.

Once your account has been set up, you will then find your 'Speech Service API Key' and 'Speech Service Region' in the Keys and Endpoint section of your account portal.

<p align = "center">
  <img src="https://github.com/2030428/Soft_Skills_Trainer/blob/master/Assets/READMEImages/speechServices.PNG" width="600" height="" />


This will then need to be added to each Speech Manager script in the Inspector window.

<p align = "center">
  <img src="https://github.com/2030428/Soft_Skills_Trainer/blob/master/Assets/READMEImages/speechManager.PNG" width="600" height="" />
  
You will find that a number of the voices referenced by the script are no longer available since Microsoft have upgraded the voices to 'Nueral'. A number have been added at the top of the list, but should you wish to add others from [the non-preview voices](https://azure.microsoft.com/en-us/services/cognitive-services/text-to-speech/#features), open the script 'TTSClient', add a new reference to the enum 'VoiceName' in the format shown below and a new case to the public string 'ConvertVoiceNametoString', as shown below. 

If we wanted to add the South African English voice Luke to the app...

<p align = "center">
  <img src="https://github.com/2030428/Soft_Skills_Trainer/blob/master/Assets/READMEImages/TTSvoices.PNG" width="600" height="" />
  
...we would add the new entry to the enum formatted as shown here...

<p align = "center">
  <img src="https://github.com/2030428/Soft_Skills_Trainer/blob/master/Assets/READMEImages/enum.PNG" width="300" height="" />
  
...and add a new case as show here.

<p align = "center">
  <img src="https://github.com/2030428/Soft_Skills_Trainer/blob/master/Assets/READMEImages/convertVoice.PNG" width="600" height="" />
  
The new voice is then available to select on the Speech Manager in the Inspector window.

<p align = "center">
  <img src="https://github.com/2030428/Soft_Skills_Trainer/blob/master/Assets/READMEImages/SALuke.PNG" width="600" height="" />
  

### Meta/Oculus Voice Recognition (Wit.ai)

### Yarn Spinner 2.0

### Meta/Oculus Lip Sync

### Rocketbox

### Mixamo
