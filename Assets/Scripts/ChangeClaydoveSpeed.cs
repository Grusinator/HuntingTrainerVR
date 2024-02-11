using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction; // Required for working with UI

public class ChangeClaydoveSpeed : MonoBehaviour
{
    public void UpdateClayDoveLaunchSpeed(float speed)
    {
        LaunchClayDove[] launchers = FindObjectsOfType<LaunchClayDove>();

        foreach (LaunchClayDove launcher in launchers)
        {
            launcher.m_LaunchSpeed = speed;
            Debug.Log("Launcher Name: " + launcher.name + ", Speed: " + launcher.m_LaunchSpeed);
        }
    }
}