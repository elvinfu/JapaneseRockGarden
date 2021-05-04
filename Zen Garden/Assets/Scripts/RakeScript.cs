using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeScript : MonoBehaviour
{
    private Vector3 updatedMousePos;
    private Vector3 initPos;
    public static bool rakeSelected;
    public Transform rakePoint;
    private Vector3 startPos;
    void Start()
    {
        canDraw = false;
        rakeSelected = false;
        mouseDown = false;
        mouseMoved = false;
        GetComponent<SpriteRenderer>().enabled = false;
        startPos = new Vector3(rakePoint.position.x, rakePoint.position.y, 0);
        straightRake = false;
    }

    public static bool canDraw;
    private bool mouseDown;
    private Vector3 mousePos;
    private bool mouseMoved;
    private Vector2 prevMousePos;
    private bool straightRake;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            straightRake = !straightRake;
            if (straightRake)
            {
                transform.eulerAngles = new Vector3(0, 0, -35);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (rakeSelected)
        {
            GetComponent<SpriteRenderer>().enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
                canDraw = true;
                if(!straightRake)
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                    transform.position.z);
                mouseDown = true;
                prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0) && mouseDown)
            {
                canDraw = false;
                rakeSelected = false;
                mouseDown = false;
            } else if (Input.GetMouseButton(0))
            {
                if (((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - prevMousePos).magnitude >= 0.01f && !mouseMoved)
                {
                    mouseMoved = true;
                }
                prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            } else
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                    transform.position.z);
                if (!straightRake)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        } else
        {
            mouseMoved = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        if (canDraw)
        {
            mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                transform.position.z);
            if (!straightRake)
            {
                transform.position = Vector3.MoveTowards(transform.position, mousePos,
                    Time.deltaTime * 0.8f);
            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, mousePos,
                    Time.deltaTime * 2.4f);
            }
            if (mouseMoved && !straightRake)
            {
                float angle = CalcAngle(transform.position, mousePos) - 240;
                if(angle == -240)
                {
                    angle = 0;
                }
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 270f);
            }

        }
    }

    float CalcAngle(Vector3 pos, Vector3 mousePos)
    {
        return Mathf.Atan2(pos.y - mousePos.y, pos.x - mousePos.x) * Mathf.Rad2Deg;
    }
}
