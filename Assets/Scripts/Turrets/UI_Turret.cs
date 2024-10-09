using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Turret : MonoBehaviour
{
    public int iD;
    public int cost;

    public TextMeshProUGUI turretName;
    public TextMeshProUGUI turretCost;
    Turret_Manager tManager;
    private void OnEnable()
    {
        tManager = FindAnyObjectByType<Turret_Manager>();
    }

    public void SetUpTurret(Turret turret)
    {
        iD = turret.iD;
        cost = turret.cost;
        turretName.SetText($"Turret {iD}");
        turretCost.SetText(cost.ToString());
    }

    public void SetTurretID()
    {
        tManager.SetTurretID(iD);
        tManager.SetDraging(true);
    }
}