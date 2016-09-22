using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TRGameUtils
{
    public class MobileLog : MonoBehaviour
    {
        public bool show;

        private Transform logPanel;
        private Transform logContainer;
        private GameObject logItem;
        private Button closeButton;
        private Button logButton;
        private Button clearButton;
        void Start()
        {
            init();
        }

        void init()
        {
            logPanel = transform.Find("Canvas/LogPanel");
            logContainer = logPanel.Find("LogContainer");
            logButton = transform.Find("Canvas/Button_Log").GetComponent<Button>();
            closeButton = transform.Find("Canvas/Button_Close").GetComponent<Button>();
            clearButton = transform.Find("Canvas/LogPanel/Button_Clear").GetComponent<Button>();
            logButton.onClick.AddListener(Button_Log_Click);
            closeButton.onClick.AddListener(Button_Close_Click);
            clearButton.onClick.AddListener(Button_Clear_Click);
            Application.logMessageReceived += onLogChange;
            logPanel.gameObject.SetActive(false);

            Debug.Log("Log");
            Debug.LogWarning("LogWarning");
            Debug.LogError("LogError");
        }

        public void Button_Log_Click()
        {
            logPanel.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(true);
            logButton.gameObject.SetActive(false);
        }

        public void Button_Close_Click()
        {
            logPanel.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            logButton.gameObject.SetActive(true);
        }

        public void Button_Clear_Click()
        {
            int itemCount = logContainer.childCount;
            for (int i = 0; i < itemCount; i++)
            {
                GameObject.Destroy(logContainer.GetChild(i).gameObject);
            }
        }
        public void onLogChange(string message, string stackTrace, LogType type)
        {
            if (!show)
            {
                return;
            }
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/LogItem") as Object, logContainer) as GameObject;
            obj.transform.localScale = Vector3.one;
            switch (type)
            {
                case LogType.Error:
                    obj.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/icon_error");
                    break;
                case LogType.Assert:
                    obj.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/icon_info");
                    break;
                case LogType.Warning:
                    obj.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/icon_warning");
                    break;
                case LogType.Log:
                    obj.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/icon_info");
                    break;
                case LogType.Exception:
                    obj.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/icon_error");
                    break;
                default:
                    break;
            }
            Text logContent = obj.transform.Find("Content/LogContent").GetComponent<Text>();
            logContent.text = message;
            Text fileLocation = obj.transform.Find("Content/FileLocation").GetComponent<Text>();
            if (string.IsNullOrEmpty(stackTrace.Trim()))
            {
                GameObject.Destroy(fileLocation.gameObject);
            }
            else
            {
                fileLocation.text = stackTrace;
            }

        }
    }
}
