using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBlossomScript : MonoBehaviour
{
    private Color32 ogColor;
    public Color32 newColor;
    private float colWidth;
    private float colHeight;
    private Vector3 mousePos;
    void Start()
    {
        ogColor = GetComponent<SpriteRenderer>().color;
        colWidth = GetComponent<BoxCollider2D>().bounds.size.x / 2;
        colHeight = GetComponent<BoxCollider2D>().bounds.size.y / 2;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x >= transform.position.x - colWidth && mousePos.x <= transform.position.x + colWidth &&
            mousePos.y >= transform.position.y - colHeight && mousePos.y <= transform.position.y + colHeight)
        {
            GetComponent<SpriteRenderer>().color = newColor;
        } else
        {
            GetComponent<SpriteRenderer>().color = ogColor;
        }
    }

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {
        
    }
}
