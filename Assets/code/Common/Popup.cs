using System;
using UnityEngine;

public class Popup
{
    private const float REGULAR_STATE_TIME = 4.0f;
    private const float FADE_SPEED = 0.7f;

    private enum States { REGULAR, FADE };
    private States state = States.REGULAR;

    private GameObject popupView;
    private SpriteRenderer spriteRenderer;
    private MeshRenderer meshTextRenderer;
    private TextMesh textMesh;
    private Color color;
    private float moveSpeed;
    private float startTime;

    public Popup(GameObject popupPrefab, GameObject popupsContainer, string text, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;

        popupView = GameObject.Instantiate(popupPrefab, popupsContainer.transform);
        spriteRenderer = (SpriteRenderer)popupView.GetComponent<SpriteRenderer>();
        textMesh = (TextMesh)popupView.GetComponentInChildren(typeof(TextMesh));
        meshTextRenderer = textMesh.GetComponent<MeshRenderer>();
        color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        popupView.transform.localPosition = new Vector3();
        textMesh.text = text;

        Bounds bounds = textMesh.GetComponent<Renderer>().bounds;
        spriteRenderer.size = bounds.size + new Vector3(.2f, .2f, 0.0f);

        startTime = Time.time;
    }

    public bool Update()
    {
        Vector3 deltaVector = new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
        popupView.transform.localPosition += deltaVector;
        popupView.transform.LookAt(Camera.main.transform);

        switch (state)
        {
            case States.REGULAR:
                if (Time.time - startTime >= REGULAR_STATE_TIME)
                {
                    state = States.FADE;
                    return false;
                }
                break;
            case States.FADE:
                if (color.a <= 0)
                {
                    return true;
                }
                color.a -= FADE_SPEED * Time.deltaTime;
                meshTextRenderer.material.color = color;
                spriteRenderer.color = color;
                break;
        }

        return false;
    }
}
