using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIKeyboardShortcut : MonoBehaviour
{
    public KeyCode key;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
            SendEvent();

    }

    protected virtual void SendEvent() //can override this for different functionality
    {
        ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler); //hook into the button's code, and cause it to act as if it was pressed
    }
}

