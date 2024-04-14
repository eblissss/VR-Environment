using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class WristMenu : MonoBehaviour
{
    private Inputs _input;
    
    public GameObject WristMenuGo;
    public DynamicMoveProvider MoveProvider;
    
    private void Start()
    {
        _input = new Inputs();
        _input.Enable();
        _input.Player.Menu.performed += Menu_clicked;
        WristMenuGo.SetActive(false);
    }

    private void Menu_clicked(InputAction.CallbackContext obj)
    {
        DisplayWristMenu();
    }

    private void DisplayWristMenu()
    {
        var active = WristMenuGo.activeInHierarchy;
        WristMenuGo.SetActive(!active);
    }

    public void ChangeWalkSpeed(float speed)
    {
        MoveProvider.moveSpeed = speed;
    }

    public void ChangeGravity(float gravity)
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }
}
