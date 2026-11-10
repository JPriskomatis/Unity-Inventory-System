using UnityEngine;

public class CursorControl : MonoBehaviour
{
    /*
     * This script is responsible for handled the cursor visibility in our game.
     * There are many moments where we want to show the mouse and make the cursor visible, such as in UI scenes or in the Pause menu.\
     * I use a getter and setter to access the variable which upon change calls the SetCursor method.
     */
    [SerializeField] private bool showCursor;
    public bool ShowCursor
    {
        get
        {
            return showCursor;
        }
        set
        {
            showCursor = value;
            SetCursor(showCursor);
        }
    }

    private void Awake()
    {
        SetCursor(showCursor);
    }

    // The OnValidate is an Editor only function that get called whenever a value from the inspector changes. In our case, the showCursor variable is
    // marked with the SerializedField attribute, which exposes it to the editor. Every time we change it from the editor, the SetCursor gets called.
    // This mostly helps with testing.
    private void OnValidate()
    {
        SetCursor(showCursor);
    }
    private void SetCursor(bool cursor)
    {
        if (!cursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}