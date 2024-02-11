using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeepTrackOfTargetsHit : MonoBehaviour
{
    public int targetsHit = 0;
    public int totalTargets = 0;

    public int totalShotsFired = 0;

    public TMP_Text statisticsDisplay;

    void Start() 
    {
        UpdateStatistics();
    }

    private void UpdateStatistics()
    {
        Debug.Log("Updating statistics");
        statisticsDisplay.text = "Targets Hit: " + targetsHit + " / " + totalTargets + " Shots Fired: " + totalShotsFired;
    }

    public void ResetStatistics()
    {
        targetsHit = 0;
        totalTargets = 0;
        totalShotsFired = 0;
        UpdateStatistics();
    }

    public void TargetHit()
    {
        targetsHit++;
        UpdateStatistics();
    }

    public void TargetLaunched()
    {
        totalTargets++;
        UpdateStatistics();
    }

    public void ShotFired()
    {
        totalShotsFired++;
        UpdateStatistics();
    }
}
