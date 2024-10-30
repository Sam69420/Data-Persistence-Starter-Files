using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class DataManager : MonoBehaviour
{
    #region singleton
    public static DataManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            // En cas de retour sur la scene, il ne faut pas conserver la nouvelle instance...
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }
    #endregion


    #region Persistent Data
    //inter scene => via singleton 

    [HideInInspector]
    public string playerName = "Anonymous";

    private int m_BestScore;
    private string m_BestPlayerName;

    public int BestScore {  get { return m_BestScore; } }
    public string BestPlayerName { get { return m_BestPlayerName; } }

    // inter session => via serialisation
    [System.Serializable]
    class SaveScoreData
    {
        public int BestScore;
        public string BestPlayerName;
    }


    private string json_file
    {
        get
        {
            return Application.persistentDataPath + "/savefile.json";
        }
    }

    public void UpdateScore(int curScore)
    {
        if (curScore < m_BestScore) return;

        m_BestScore = curScore;
        m_BestPlayerName = playerName;
    }


    public void SaveBestScore()
    {
        SaveScoreData data = new SaveScoreData()
        {
            BestScore = m_BestScore,
            BestPlayerName = m_BestPlayerName
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(json_file, json);
    }
    public void LoadBestScore()
    {
        if (File.Exists(json_file))
        {
            string json = File.ReadAllText(json_file);
            SaveScoreData data = JsonUtility.FromJson<SaveScoreData>(json);

            m_BestScore = data.BestScore;
            m_BestPlayerName = data.BestPlayerName;
        }
    }

    #endregion
}
