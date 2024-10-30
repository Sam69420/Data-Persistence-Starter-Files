using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

#if UNITY_EDITOR
// Il n'est pas possible de builder une appli avec cette reference !! juste pour l'editeur ;)
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bestScore;

    [SerializeField]
    private TMP_InputField playerNameInput;


    private void Start()
    {
        if(bestScore != null)
        {
            int score = 0;
            string player = "";

            if (DataManager.Instance != null)
            {
                score = DataManager.Instance.BestScore;
                player = DataManager.Instance.BestPlayerName;
            }

            if(score == 0)
            {
                bestScore.text = "Best Score : -";
            }
            else
            {
                bestScore.text = $"Best Score : {score}"
                    + (player != ""? $" by {player}" : "");
            }

        }
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void NewPlayer()
    {
        if(DataManager.Instance == null) return;

        DataManager.Instance.playerName = playerNameInput.text;
    }

}
