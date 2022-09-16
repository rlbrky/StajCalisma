using UnityEngine;

public class ReadInput : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    bool isCanvasActive = false;
    public bool IsCanvasActive
    {
        get { return isCanvasActive; }
        set { IsCanvasActive = value; }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isCanvasActive)
        {
            isCanvasActive = true;
            canvas.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isCanvasActive)
        {
            isCanvasActive = false;
            canvas.SetActive(false);
        }
    }
}
