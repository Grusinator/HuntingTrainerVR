using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction; // Required for working with UI

public class ToggleGuideObject : MonoBehaviour
{
    public void ToggleGuideObjectOnShotguns(bool enabled)
    {
        ShootShotgunShell[] shotguns = FindObjectsOfType<ShootShotgunShell>();

        foreach (ShootShotgunShell shotgun in shotguns)
        {
            shotgun.m_guide_enabled = enabled;
        }
    }
}