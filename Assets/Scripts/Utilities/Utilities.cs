using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    private static Dictionary<float, WaitForSeconds> waitDictionary = new();
    public static WaitForSeconds SetWait(float second)
    {
        if (!waitDictionary.ContainsKey(second))
            waitDictionary.Add(second, new WaitForSeconds(second));

        return waitDictionary[second];
    }
}
