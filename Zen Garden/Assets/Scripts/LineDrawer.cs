using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject line;
    public int frameWait;


    private Vector3 ogPos;
    void Start()
    {
        ogPos = new Vector3(transform.position.x, transform.position.y, 0);
        f = false;
    }

    private Vector3 dir;
    private bool f;
    private int frameCount;
    void Update()
    {
        if (RakeScript.canDraw)
        {
            if (!f)
            {
                f = true;
                ogPos = transform.position;
                frameCount = frameWait;
            }
        }
        else
        {
            f = false;
        }

        if (RakeScript.canDraw)
        {
            Vector3 dir = new Vector3((transform.position - ogPos).normalized.x, (transform.position - ogPos).normalized.y, 0);
            DrawLine(transform.position, ogPos, new Color32(255, 255, 255, 255), 0.2f);
            if (frameCount >= frameWait)
            {
                ogPos = transform.position;
                frameCount = 0;
            }
        }
        frameCount++;
    }

    void DrawLine(Vector3 start, Vector3 end, Color32 color, float duration = 0.2f)
    {
        GameObject myLine = Instantiate(line, transform.position, transform.rotation);
        myLine.transform.parent = transform;
        myLine.transform.position = start;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        //Destroy(myLine, duration);
    }
}
