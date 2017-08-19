using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class PopupComponent : MonoBehaviour
{
    private Canvas canvas;

    public void Start()
    {
        canvas = GetComponent<Canvas>();
    }
}
