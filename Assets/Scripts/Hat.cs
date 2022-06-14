using UnityEngine;

public class Hat : MonoBehaviour
{
    [SerializeField] private int id;
    public bool isMoving;

    public delegate void OnClickedDelegate(int id);

    public event OnClickedDelegate onClicked;

    public RectTransform rect;

    private Vector3 targetPos, leftHatPos, rightHatPos;
    private float duration, hatLoweredPosY;

    public void StartMoving(float duration, Vector3 leftHatPos, Vector3 rightHatPos, float hatLoweredPosY)
    {
        this.duration = duration;
        this.leftHatPos = leftHatPos;
        this.rightHatPos = rightHatPos;
        this.hatLoweredPosY = hatLoweredPosY;
        targetPos =
            new Vector3(UnityEngine.Random.Range(leftHatPos.x, rightHatPos.x), hatLoweredPosY, 0);
    }

    private bool arrivedTarget;

    private void FixedUpdate()
    {
        if (duration <= 0)
        {
            isMoving = false;
            return;
        }

        isMoving = true;
        duration -= Time.fixedDeltaTime;
        if (Mathf.Abs(rect.anchoredPosition.x - targetPos.x) < 10f)
        {
            arrivedTarget = true;
        }

        if (arrivedTarget)
        {
            targetPos = new Vector3(UnityEngine.Random.Range(leftHatPos.x, rightHatPos.x), hatLoweredPosY, 0);
            arrivedTarget = false;
        }
        else
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, targetPos, Time.fixedDeltaTime * 10);
        }
    }


    public void OnClicked()
    {
        Vibration.Vibrate(2);

        onClicked?.Invoke(id);
    }
}