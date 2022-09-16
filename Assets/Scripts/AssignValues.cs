using UnityEngine;
using UnityEngine.UI;

public class AssignValues : MonoBehaviour
{
    [SerializeField] Slider speedSlider;
    [SerializeField] Slider heightSlider;
    [SerializeField] Text speedValTxt;
    [SerializeField] Text heightValTxt;
    public float GetSpeed()
    {
        return speedSlider.value;
    }
    public float GetHeight()
    {
        return heightSlider.value;
    }
    public void SetSpeedTxt()
    {
        speedValTxt.text = GetSpeed().ToString();
    }
    public void SetHeightTxt()
    {
        heightValTxt.text = GetHeight().ToString();
    }
}
