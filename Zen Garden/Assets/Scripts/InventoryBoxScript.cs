using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBoxScript : MonoBehaviour
{

    private Color32 ogColor;
    public GameObject obj;
    public enum Type
    {
        thing,
        rake
    }
    public Type type;
    private bool isSelected;
    private bool mouseDown;
    private int selectedNum;

    void Start()
    {
        ogColor = GetComponent<SpriteRenderer>().color;
        isSelected = false;
        mouseDown = false;
        selectedNum = 0;
    }

    private Vector3 mousePos;
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        isSelected = true;
        if (type == Type.thing)
        {
            Instantiate(obj, new Vector3(mousePos.x, mousePos.y, transform.position.z), transform.rotation);
        } else if (type == Type.rake)
        {
            mouseDown = true;
        }
    }

    private void OnMouseUp()
    {
        if (mouseDown)
        {
            RakeScript.rakeSelected = true;
        }

        mouseDown = false;
    }

    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = ogColor;
    }
}
