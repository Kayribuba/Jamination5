using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isWerewolf;
    [SerializeField] WerewolfController wwController;
    [SerializeField] WolfDrag wDrag;
    [SerializeField] DragLine DL;
    [SerializeField] LineRenderer LR;

    void Update()
    {

    }
    private void Start()
    {
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
    }
    public void ChangeType()
    {
        if (isWerewolf)
            BecomeHuman();
        else
            BecomeWerewolf();
    }
}
