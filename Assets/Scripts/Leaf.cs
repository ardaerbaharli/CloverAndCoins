using UnityEngine;
using UnityEngine.EventSystems;

public class Leaf : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int id;

    public delegate void OnPointerDown_(int id);

    public event OnPointerDown_ onPointerDown_;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown_?.Invoke(id);
    }
}