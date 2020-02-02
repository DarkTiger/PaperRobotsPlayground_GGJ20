using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
     GameObject[] button;
    [SerializeField]
    GameObject imgMenu;
    [SerializeField]
    GameObject imgRules;

    bool selectAxis = false;
    bool imgR = false;
    // Start is called before the first frame update
    void Start()
    {
        button[0].SetActive(true);
        button[1].SetActive(true);
        button[2].SetActive(true);
        button[0].GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (imgR)
        {
            ;
            if (Input.GetButton("Continue"))
            {
                //Debug.Log(Input.GetButton("Select"));
                SceneManager.LoadScene(1);
            }
        }
        float tmpP1 = Input.GetAxis("VerticalP1");
        float tmpP2 = Input.GetAxis("VerticalP2");
        if (Input.GetAxis("VerticalP1") != 0 && Input.GetAxis("VerticalP2") != 0)
        {
            ResetAxes(selectAxis);
        }
        if (button[0].GetComponent<Image>().color == Color.green)
        {
            if ((tmpP1 <0 || tmpP2 <0) && !selectAxis)
            {
                button[0].GetComponent <Image>().color = Color.white;
                button[1].GetComponent<Image>().color = Color.green;
            }
            selectAxis = true;
            if (Input.GetButton("Select"))
            {
                //inserire panel regole
                imgMenu.SetActive(false);
                imgRules.SetActive(true);
                imgR = true;
                //Debug.Log(Input.GetButton("Select"));
            }
            ResetAxes(selectAxis);
            //Debug.Log("Button 0");
        }
        if (button[1].GetComponent<Image>().color == Color.green)
        {
            if ((tmpP1 < 0 || tmpP2 < 0) && !selectAxis)
            {
                button[1].GetComponent<Image>().color = Color.white;
                button[2].GetComponent<Image>().color = Color.green;
            }
            if ((tmpP1 > 0 || tmpP2 > 0) && !selectAxis)
            {
                button[1].GetComponent<Image>().color = Color.white;
                button[0].GetComponent<Image>().color = Color.green;
            }
            selectAxis = true;
            if (Input.GetButton("Select"))
            {
                //inserire pannello crediti
                Debug.Log("Credits");
            }
            ResetAxes(selectAxis);
            //Debug.Log("Button 1");

        }
        if (button[2].GetComponent<Image>().color == Color.green)
        {
            if (tmpP1 > 0 || tmpP2 > 0 && !selectAxis)
            {
                button[2].GetComponent<Image>().color = Color.white;
                button[1].GetComponent<Image>().color = Color.green;
            }
            selectAxis = true;
            if (Input.GetButton("Select"))
            {
                Application.Quit();
            }
            ResetAxes(selectAxis);
            //Debug.Log("Button 2");
        }
    }
    private void ResetAxes(bool tmp)
    {
            if (tmp && (Input.GetAxis("VerticalP1") != 0 && Input.GetAxis("VerticalP2") != 0))
            {
            //Debug.Log("Reverse true");
            selectAxis = true;
            }
            if (tmp && (Input.GetAxis("VerticalP1") == 0 && Input.GetAxis("VerticalP2") == 0))
            {
            //Debug.Log("Reverse false");
            selectAxis = false;
            }
    }
}
