using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ToggleMenu : MonoBehaviour
{
    public InputActionReference activateActionReference; // Assign in the inspector
    public GameObject menu;

    private void Start()
    {
        activateActionReference.action.performed += OnActivatePerformed;
        activateActionReference.action.Enable();
    }

    private void OnDestroy()
    {
        activateActionReference.action.performed -= OnActivatePerformed;
        activateActionReference.action.Disable();
    }

    private void OnActivatePerformed(InputAction.CallbackContext context)
    {
        // Your function here
        Debug.Log("Activate button pressed");
        menu.SetActive(!menu.activeSelf);
    }
}

// {
//     public InputActionAsset actionAsset; // Assign this in the inspector
//     private InputAction toggleMenuAction;

//     void Start()
//     {
//         var actionMap = actionAsset.FindActionMap("VRControls"); // Replace "VRControls" with your action map name
//         toggleMenuAction = actionMap.FindAction("ToggleMenu"); // Replace "ToggleMenu" with your action name
        
//         // Add event listener for the action
//         toggleMenuAction.performed += ToggleMenu;
//         toggleMenuAction.Enable(); // Enable the action
//     }

//     private void OnDestroy()
//     {
//         // Clean up
//         toggleMenuAction.performed -= ToggleMenu;
//         toggleMenuAction.Disable();
//     }

//     private void ToggleMenu(InputAction.CallbackContext context)
//     {
//         // Toggle your menu here
//         Debug.Log("Menu toggled");
//         // Example: 
//     }
// }