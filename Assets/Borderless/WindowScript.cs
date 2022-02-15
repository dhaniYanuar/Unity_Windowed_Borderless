using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowScript : MonoBehaviour, IDragHandler
{
    public DisplayResolution displayResolution;
    public Vector2Int defaultWindowSize;
    public Vector2Int borderSize;

    private Vector2 _deltaValue = Vector2.zero;
    private bool _maximized;

    void Start()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
    public void OnBorderBtnClick()
    {
        StartCoroutine(ChangeDisplaySequence(true, FullScreenMode.Windowed));   
    }

    public void OnNoBorderBtnClick()
    {
        StartCoroutine(ChangeDisplaySequence(false, FullScreenMode.Windowed));
    }

    IEnumerator ChangeDisplaySequence(bool isFramed, FullScreenMode mode)
    {
        Screen.SetResolution(800, 600, mode);
        yield return new WaitForSeconds(0.3f);
        if(isFramed)
        {
            Debug.Log("Set Framed");
            BorderlessWindow.SetFramedWindow(defaultWindowSize);  
        }else
        {
            Debug.Log("Set Frameless");
            BorderlessWindow.SetFramelessWindow(defaultWindowSize);
        }
        displayResolution.ChangeTextRes();
    }

    public void OnFullScreenClick()
    {
        Debug.Log("Full Screen");
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
        displayResolution.ChangeTextRes();
    }

    public void ResetWindowSize()
    {
        BorderlessWindow.MoveWindowPos(Vector2Int.zero, defaultWindowSize.x, defaultWindowSize.y);
    }

    public void OnCloseBtnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Application.Quit();
    }

    public void OnMinimizeBtnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        BorderlessWindow.MinimizeWindow();
    }

    public void OnMaximizeBtnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (_maximized)
            BorderlessWindow.RestoreWindow();
        else
            BorderlessWindow.MaximizeWindow();

        _maximized = !_maximized;
    }

    public void OnDrag(PointerEventData data)
    {
        if (BorderlessWindow.framed)
            return;

        _deltaValue += data.delta;
        if (data.dragging)
        {
            BorderlessWindow.MoveWindowPos(_deltaValue, Screen.width, Screen.height);
        }
    }
}
