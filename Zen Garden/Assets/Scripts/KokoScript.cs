using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokoScript : MonoBehaviour
{
    public GameObject textBox;
    public Sprite[] expressions;
    private List<GameObject> texts;
    private int num;
    private bool isMoving;
    private bool isMovingBack;
    private bool doneMoving;
    private Vector2 initPos;
    private Vector2 targetPos;
    private Vector2 velocity;
    public GameObject rockGarden;
    private bool gardenFlash;
    private bool f;
    private byte i;
    private bool iIncrease;
    private bool permaHide;
    void Start()
    {
        num = -1;
        doneMoving = false;
        isMoving = false;
        f = false;
        i = 0;
        iIncrease = true;
        permaHide = false;
        targetPos = transform.position;
        initPos = new Vector2(targetPos.x, targetPos.y - 5);
        GetComponent<SpriteRenderer>().enabled = false;
        textBox.SetActive(false);
        gardenFlash = false;
        texts = new List<GameObject>();
        rockGarden.GetComponent<SpriteRenderer>().enabled = false;
        foreach(Transform child in textBox.transform)
        {
            texts.Add(child.gameObject);
        }
        foreach (GameObject go in texts)
        {
            go.SetActive(false);
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.SmoothDamp(transform.position,
                targetPos, ref velocity, 0.12f, 12, Time.deltaTime);
            if(((Vector2)transform.position - targetPos).magnitude <= 0.005f)
            {
                isMoving = false;
                doneMoving = true;
            }
        }
        if (isMovingBack)
        {
            transform.position = Vector2.SmoothDamp(transform.position,
                initPos, ref velocity, 0.12f, 12, Time.deltaTime);
            if (((Vector2)transform.position - initPos).magnitude <= 0.005f)
            {
                isMovingBack = false;
                doneMoving = true;
            }
        }
        if (doneMoving && !f)
        {
            f = true;
            
            if (expressions[num] != null)
            {
                foreach (GameObject go in texts)
                {
                    go.SetActive(false);
                }
                texts[num].SetActive(true);
                textBox.SetActive(true);
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (gardenFlash)
        {
            rockGarden.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, i);
            if (iIncrease && !permaHide)
            {
                if (i < 255)
                {
                    i+=1;
                }
                else
                {
                    i = 255;
                    iIncrease = false;
                }
            } else
            {
                if(i > 0)
                {
                    i-=1;
                } else
                {
                    i = 0;
                    iIncrease = true;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            permaHide = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(num == -1 || expressions[num] == null)
            {
                isMoving = true;
                doneMoving = false;
                f = false;
                transform.position = initPos;
            } else if(num+1 >= expressions.Length || expressions[num+1] == null)
            {
                isMovingBack = true;
                doneMoving = false;
                f = false;
                transform.position = targetPos;
            } else
            {
                isMoving = false;
                doneMoving = true;
            }

            if (num < expressions.Length - 1)
                num++;

            if (expressions[num] != null)
            {
                GetComponent<SpriteRenderer>().sprite = expressions[num];
                GetComponent<SpriteRenderer>().enabled = true;
                if(num == expressions.Length - 2)
                {
                    gardenFlash = true;
                    i = 0;
                    rockGarden.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (!isMoving)
                {
                    foreach (GameObject go in texts)
                    {
                        go.SetActive(false);
                    }
                    texts[num].SetActive(true);
                    textBox.SetActive(true);
                }
                if (isMovingBack)
                {
                    textBox.SetActive(false);
                    foreach (GameObject go in texts)
                    {
                        go.SetActive(false);
                    }
                }
            }
            else
            {
                textBox.SetActive(false);
                foreach (GameObject go in texts)
                {
                    go.SetActive(false);
                }
            }

        } else if (Input.GetKeyDown(KeyCode.A))
        {
            if (num > 0)
                num--;
            if (expressions[num] != null)
            {
                foreach (GameObject go in texts)
                {
                    go.SetActive(false);
                }
                texts[num].SetActive(true);
                GetComponent<SpriteRenderer>().sprite = expressions[num];
                GetComponent<SpriteRenderer>().enabled = true;
                textBox.SetActive(true);
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
                textBox.SetActive(false);
                foreach (GameObject go in texts)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
