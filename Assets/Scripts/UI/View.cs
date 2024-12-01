using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class View : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private List<GameObject> player;
    [SerializeField] private List<Button> playerButton;

    public Action<int> OnPlayerButtonClicked;

    private void Awake()
    {
        for(int i = 0; i < playerButton.Count; i++)
        {
            int index = i;
            playerButton[i].onClick.AddListener(() => PlayerButtonEvent(index));
            playerButton[i].gameObject.SetActive(true);
        }
        Time.timeScale = 0;
    }
    private void PlayerButtonEvent(int playernum)
    {
        OnPlayerButtonClicked?.Invoke(playernum);
    }
    public IEnumerator UpdateStageText(int currentstage)
    {
        text.gameObject.SetActive(true);
        if (currentstage < 40)
            text.text = currentstage.ToString() +" Stage";
        else
            text.text = "모든 스테이지 클리어";
        yield return Utilities.SetWait(0.5f);
        text.gameObject.SetActive(false);
    }
    public void UpdatePlayerActive(int playernum)
    {
        player[playernum].SetActive(true);
        for (int i = 0; i < playerButton.Count; i++)
        {
            playerButton[i].gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }
}
