using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public int currentStage { get; private set; } = 0;
    public int PlayerNum { get; private set; } = 0;
    public void PlusStageIndex()
    {
        currentStage++;
    }
    public void ChangePlayerNum(int playernum) => PlayerNum = playernum;
}
