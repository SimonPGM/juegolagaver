using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] 
    private GameObject previousButton;

    [SerializeField]
    private GameObject nextButton;

    public GameObject[] descriptions;

    public int idx = 0;
    private int _numberOfDescriptions;
    
    void Start()
    {
        previousButton.SetActive(false);
        nextButton.SetActive(true);
        _numberOfDescriptions = descriptions.Length;
        descriptions[idx].SetActive(true);
    }

    void Update()
    {
        //descriptions[idx].SetActive(true);
        previousButton.SetActive(idx > 0);
        nextButton.SetActive(idx != (_numberOfDescriptions - 1));
        if (Input.GetKeyDown(KeyCode.LeftArrow) && idx > 0) PreviousDescription();
        if (Input.GetKeyDown(KeyCode.RightArrow) && idx != (_numberOfDescriptions - 1)) NextDescription();
    }

    public void NextDescription()
    {
        descriptions[idx].SetActive(false);
        idx += 1;
        descriptions[idx].SetActive(true);
    }
    
    public void PreviousDescription()
    {
        descriptions[idx].SetActive(false);
        idx -= 1;
        descriptions[idx].SetActive(true);
    }

    public void ResetMenu()
    {
        descriptions[idx].SetActive(false);
        idx = 0;
        previousButton.SetActive(false);
        nextButton.SetActive(true);
        descriptions[idx].SetActive(true);
    }
    
}
