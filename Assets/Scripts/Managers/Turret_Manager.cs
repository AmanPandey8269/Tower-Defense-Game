using System.Collections.Generic;
using UnityEngine;

public class Turret_Manager : MonoBehaviour
{
    public List<Turret> turrets = new List<Turret>();

    int currentTurret;
    bool canInstantiate = true;
    bool isDraging = false;
    bool purchasedCurrentItem = true;
    public bool dragOnViewSpace = true;
    Turret current_Turret;
    [SerializeField] Game_Manager game_Manager;

    void Update()
    {
        CreateTurret();

        EndDraging();
    }

    public void SetTurretID(int id)
    {
        currentTurret = id;
    }

    public void SetDraging(bool dragState)
    {
        dragOnViewSpace = false;
        if (game_Manager.GetScore >= turrets[currentTurret - 1].cost)
        {
            isDraging = dragState;
            canInstantiate = true;
            purchasedCurrentItem = false;
        }

    }

    public void EndDraging()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            dragOnViewSpace = true;
            isDraging = false;
            if (!purchasedCurrentItem && current_Turret != null)
            {
                game_Manager.GetScore -= turrets[currentTurret - 1].cost;
                purchasedCurrentItem = true;
                current_Turret = null;
            }
        }
    }

    void CreateTurret()
    {
        if (isDraging)
        {
            if (canInstantiate)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.transform.tag == "TurretHolder")
                    {
                        canInstantiate = false;
                        current_Turret = Instantiate(turrets[currentTurret - 1], hit.transform.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.transform.tag == "TurretHolder")
                    {
                        current_Turret.transform.position = hit.transform.position;
                    }
                }
            }
        }
    }
}
