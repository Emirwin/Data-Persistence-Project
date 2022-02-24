using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    //public static string playerName;
    public TextMeshProUGUI playerNameIn;
    public TextMeshProUGUI bestScores;
    // Start is called before the first frame update
    void Start()
    {
        if(LoadBestScore().score != -1)
        {
            bestScores.text = $"Best score: {LoadBestScore().playerName} : {LoadBestScore().score}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        PlayerDataManager.Instance.playerName = playerNameIn.text;
        Debug.Log("Hello "+ PlayerDataManager.Instance.playerName);

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

   [System.Serializable]
    public class BestScoreData
    {
        public string playerName;
        public int score;
        
        public BestScoreData(string pName, int pScore)
        {
            playerName = pName;
            score = pScore;
        }
    }

    public BestScoreData LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);

            return data;         
        }
        return new BestScoreData("null",-1);
    }
}
