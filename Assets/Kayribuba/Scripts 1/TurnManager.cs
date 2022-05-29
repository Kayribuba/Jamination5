using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public Slider slider;
    public GameObject wb;
    public ShakeTrigger st;

    [SerializeField] GameObject wolfSkin;
    [SerializeField] GameObject humanSkin;
    [SerializeField] TextMeshProUGUI X5;
    
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
        slider.maxValue = maxTime;
        slider.value = slider.maxValue;
        BecomeHuman();
    }

    public void BecomeWerewolf()
    {
        isWerewolf = true;
        wwController.enabled = false;
        wDrag.enabled = true;
        DL.enabled = true;
        LR.enabled = true;
        wDrag.wasButtonDown = false;
        humanSkin.SetActive(false);
        wolfSkin.SetActive(true);
        X5.enabled = true;
        st.Shake();
    }
    public void BecomeHuman()
    {
        isWerewolf = false;
        wwController.enabled = true;
        wDrag.enabled = false;
        DL.enabled = false;
        LR.enabled = false;
        humanSkin.SetActive(true);
        wolfSkin.SetActive(false);
        X5.enabled = false;
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
            if (wDrag.isFacingLeft)
                wDrag.Flip();

            BecomeHuman();
            wb.SetActive(false);
        }
      
    }
}
