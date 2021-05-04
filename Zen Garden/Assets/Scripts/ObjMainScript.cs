using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMainScript : MonoBehaviour
{
    List<GameObject> rocks; 
    private GameObject go;
    void Start()
    {
        rocks = new List<GameObject>();
        numChange = true;
        placed = false;
        selectedNum = 0;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            rocks.Add(child.gameObject);
        }
    }

    private int selectedNum;
    private bool numChange;
    private Vector3 mousePos;
    private bool placed;
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
            {
                selectedNum = number;
                numChange = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedNum = 10;
            numChange = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedNum = 11;
            numChange = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (numChange && !placed)
            {
                foreach(Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                if (transform.childCount > selectedNum)
                {
                    transform.GetChild(selectedNum).gameObject.SetActive(true);
                    transform.GetChild(selectedNum).transform.position = new Vector3(mousePos.x, mousePos.y, -5);
                    numChange = false;
                }
            }
        } else if (Input.GetMouseButtonUp(0))
        {
            if(transform.childCount <= selectedNum)
            {
                Destroy(go);
            }
            placed = true;
        }
    }



    
}
