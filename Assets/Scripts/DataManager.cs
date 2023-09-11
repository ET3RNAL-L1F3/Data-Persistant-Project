using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public TMP_InputField playerNameInput;
    public string playerName;

    public int currentHighScore;
    public string highScoreName;
   

    // Start is called before the first frame update
    void Start()
    {
        currentHighScore = 0;
        highScoreName = "John";

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (playerName != null)
        {
            playerName = playerNameInput.text;
        }
        SceneManager.LoadScene("main");
       
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string HighScoreName;
    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        data.HighScore = MainManager.Instance.m_Points;
        data.HighScoreName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            currentHighScore = data.HighScore;
            highScoreName = data.HighScoreName;
        }
    }
}
