using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Slider slider;
    public GameObject wb;
    
    public bool isWerewolf;
    [SerializeField] WerewolfController wwController;
    [SerializeField] WolfDrag wDrag;
    [SerializeField] DragLine DL;
    [SerializeField] LineRenderer LR;

    public float maxTime = 3;

    void Update()
    {
        DecreaseBar();
    }
    private void Start()
    {
        wb.SetActive(false);
        slider.value = maxTime;
        BecomeHuman();
    }

    public void BecomeWerewolf()
    {
        isWerewolf = true;
        wwController.enabled = false;
        wDrag.enabled = true;
        DL.enabled = true;
        LR.enabled = true;
    }
    public void BecomeHuman()
    {
        isWerewolf = false;
        wwController.enabled = true;
        wDrag.enabled = false;
        DL.enabled = false;
        LR.enabled = false;
        GetComponent<Rigidbody2D>().drag = 0;
        slider.value = maxTime;
    }
    public void ChangeType()
    {
        if (isWerewolf)
            BecomeHuman();
        else
            BecomeWerewolf();
    }

    void DecreaseBar()
    {
        if (isWerewolf && slider.value > 0)
        {
            wb.SetActive(true);
            slider.value -= 1 / maxTime * Time.deltaTime;
        }
        else
        {
            BecomeHuman();
            wb.SetActive(false);
        }
      
    }
}
