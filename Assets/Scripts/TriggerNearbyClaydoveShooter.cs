using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerNearbyClaydoveShooter : MonoBehaviour
{
    public InputActionReference fireAction;
    
    private void Start()
    {
        fireAction.action.performed += OnFirePerformed;
        fireAction.action.Enable();
    }

    private void OnDestroy()
    {
        fireAction.action.performed -= OnFirePerformed;
        fireAction.action.Disable();
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        LaunchClayDove nearestLauncher = FindNearestLauncher();
        
        if (nearestLauncher != null)
        {
            nearestLauncher.Fire();
        }
    }
    
    private LaunchClayDove FindNearestLauncher()
    {
        LaunchClayDove[] launchers = FindObjectsOfType<LaunchClayDove>();
        
        if (launchers.Length == 0)
        {
            return null;
        }
        
        LaunchClayDove nearestLauncher = launchers[0];
        float nearestDistance = Vector3.Distance(transform.position, nearestLauncher.transform.position);
        
        for (int i = 1; i < launchers.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, launchers[i].transform.position);
            
            if (distance < nearestDistance)
            {
                nearestLauncher = launchers[i];
                nearestDistance = distance;
            }
        }
        
        return nearestLauncher;
    }
}