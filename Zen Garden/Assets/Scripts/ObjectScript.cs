using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectScript : MonoBehaviour
{
    public Transform point;
    GameObject getTarget;
    private bool isMouseDragging;
    Vector3 offsetValue;
    Vector3 positionOfScreen;

    private bool isInitMouseTouch;
    private bool followMouse;
    private bool mouseUp;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        initSize = transform.localScale.x;
        isInitMouseTouch = true;
        followMouse = false;
        mouseUp = false;
    }

    private float size;
    private float initSize;
    void Update()
    {
        //Debug.Log(isInitMouseTouch + " " + mouseUp + " " + followMouse);
        if (followMouse && (!mouseUp || isInitMouseTouch))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
        //size = initSize / Mathf.Pow(Vector3.Distance(point.position, transform.position), 1/5f);
        transform.localScale = new Vector3(initSize, initSize, 1);
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        if (Input.GetMouseButtonUp(0) && followMouse)
        {
            isInitMouseTouch = false;
            mouseUp = true;
            followMouse = false;
        }
    }

    private void OnMouseDown()
    {
        followMouse = true;
        mouseUp = false;
    }

    private void OnMouseOver()
    {
        if (isInitMouseTouch)
        {
            followMouse = true;
        }
    }

    private void OnMouseUp()
    {
        isInitMouseTouch = false;
        mouseUp = true;
        followMouse = false;
    }

}
