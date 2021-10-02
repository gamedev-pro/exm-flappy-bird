using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalRewardCalculator : MonoBehaviour
{
    [SerializeField] private MedalsSettings medalsSettings;

    public Medal GetMedalForScore(int score)
    {
        //TODO: Assumes medals are ordered. Order on awake.
        for (int i = medalsSettings.Medals.Length - 1; i >= 0; i--)
        {
            Medal medal = medalsSettings.Medals[i];
            if (medal.MinScore <= score)
            {
                return medal;
            }
        }
        return null;
    }
}
