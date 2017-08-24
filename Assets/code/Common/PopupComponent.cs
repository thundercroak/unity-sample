using UnityEngine;
using System.Collections.Generic;

public class PopupComponent : MonoBehaviour
{
    public GameObject popupPrefab;
    public GameObject popupsContainer;
    public float moveSpeed = 1.0f;

    private List<Popup> popups;

    public void Start()
    {
        popups = new List<Popup>();
    }

    public void Update()
    {
        for (int i = 0; i < popups.Count; i++)
        {
            Popup popup = popups[i];
            if (popup.Update())
            {
                popups.Remove(popup);
            }
        }
    }

    public void AddNewPopup(string text)
    {
        Popup popup = new Popup(popupPrefab, popupsContainer, text, moveSpeed);
        popups.Add(popup);
    }
}
