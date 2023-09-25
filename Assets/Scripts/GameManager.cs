using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public bool isGameActive { get; set; }
    public float highestScore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {   BC_MusicManager.instance.IngameMusic();   
        isGameActive = true;
        SceneManager.LoadScene(1);
        Debug.Log(Application.persistentDataPath);
    }

    public void RestartGame()
    {
        BC_MusicManager.instance.IngameMusic();   
        isGameActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Set_IsGameActive(bool value)
    {
        isGameActive = value;
        if (!isGameActive)
        {
            GameObject.Find("Canvas").gameObject.GetComponent<InGame_UI>().GameOverUI();
        }
    }

    public float GetHighestScore()
    {
        LoadHighestScore();
        return highestScore;
    }

    [System.Serializable]
    class SaveData
    {
        public float highestScore;
    }

    public void SaveHighestScore()
    {
        SaveData data = new SaveData();
        data.highestScore = highestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highestScore = data.highestScore;
        }
    }

    public void SetScore(float _highestScore)
    {
        LoadHighestScore();
        if (_highestScore > highestScore)
        {
            highestScore = _highestScore;
            SaveHighestScore();
        }
    }
}
