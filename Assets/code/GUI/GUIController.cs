using System;
using Radio;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GUI
{
    public class GUIController : MonoBehaviour
    {
        public delegate void MessageSend(string text);
        public event MessageSend MessageSendEvent;

        public delegate void SwitchRadioMode();
        public event SwitchRadioMode SwitchRadioModeEvent;

        private Canvas canvas;
        private InputField inputField;
        private Text stateText;
        private Button sendMessageBtn;
        private Image sendMessageImage;
        private Dictionary<RadioStation.States, string> texts;

        public void Start()
        {
            canvas = GetComponent<Canvas>();
            GameObject inputFieldGO = canvas.transform.Find("InputField").gameObject;
            inputField = inputFieldGO.GetComponent<InputField>();

            GameObject stateTextGO = canvas.transform.Find("StateText").gameObject;
            stateText = stateTextGO.GetComponent<Text>();

            GameObject sendMessageGO = canvas.transform.Find("SendMsgBtn").gameObject;
            sendMessageBtn = sendMessageGO.GetComponent<Button>();
            sendMessageImage = sendMessageGO.GetComponent<Image>();

            texts = new Dictionary<RadioStation.States, string>();
            texts[RadioStation.States.TURNED_OFF] = "Off";
            texts[RadioStation.States.RECEIVING_MODE] = "Receiving";
            texts[RadioStation.States.DISPATCHING_MODE] = "Dispatching";
        }

        public void SendMsgBtnHandler()
        {
            if (inputField.text.Length > 0)
            {
                Debug.Log("inputField.text: " + inputField.text);
                if (MessageSendEvent != null) MessageSendEvent(inputField.text);
                inputField.text = "";
            }
        }

        public void NextBtnHandler()
        {
            Debug.Log("Next");
            if (SwitchRadioModeEvent != null) SwitchRadioModeEvent();
        }

        public void StateSwitched(RadioStation.States newState)
        {
            UpdateStateText(newState);
        }

        private void UpdateStateText(RadioStation.States state)
        {
            Debug.Log("UpdateStateText. state: " + state);
            stateText.text = texts[state];

            if (state == RadioStation.States.DISPATCHING_MODE)
            {
                sendMessageBtn.interactable = true;
                sendMessageImage.color = Color.white;
            }
            else
            {
                sendMessageBtn.interactable = false;
                sendMessageImage.color = Color.gray;
            }
        }
    }
}
