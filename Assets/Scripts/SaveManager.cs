using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public string playerName;
    public string bestPlayerName;
    public int bestScore;
    public TextMeshProUGUI playerInputField;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        playerName = playerInputField.text;
    }

    [System.Serializable]
    class SaveData
    {
        public int savedBestScore;
        public string savedBestPlayerName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.savedBestScore = bestScore;
        data.savedBestPlayerName = playerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.savedBestScore;
            bestPlayerName = data.savedBestPlayerName;
        } else
        {
            bestScore = 0;
            bestPlayerName = "Chuck Norris";
        }
    }
}
