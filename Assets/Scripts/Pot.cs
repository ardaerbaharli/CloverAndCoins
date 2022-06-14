using UnityEngine;
using UnityEngine.UI;

public class Pot : MonoBehaviour
{
    public int value;
    public bool isBiggest;
    [SerializeField] private Text text;


    public void OnClicked()
    {
        Vibration.Vibrate(2);
        onClicked?.Invoke(this);
    }

    public delegate void OnClickedDelegate(Pot pot);

    public event OnClickedDelegate onClicked;

    public void ShowValue()
    {
        text.gameObject.SetActive(true);
    }

    public void SetValue(int i)
    {
        value = i;
        text.text = value.ToString();
    }
}