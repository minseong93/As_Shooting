using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField] private View view;
    private Model model;
    private void Start()
    {
        model = new Model();
        view.OnPlayerButtonClicked += PlayerButtonClicked;
    }
    private void ChangedStageText()
    {
        model.PlusStageIndex();
        StartCoroutine(view.UpdateStageText(model.currentStage));    
    }
    private void PlayerButtonClicked(int playernum)
    {
        model.ChangePlayerNum(playernum);
        UpdatePlayer();
    }
    private void UpdatePlayer()
    {
        view.UpdatePlayerActive(model.PlayerNum);
    }

    public void SetStage()
    {
        ChangedStageText();
    }
}
