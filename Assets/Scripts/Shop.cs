using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    BuildManager buildManager;
    private GameObject turretToBuild;
    public GameObject basicTurret;

    private GameObject turretGhost;

    void Start()
    {
        buildManager = BuildManager.instance; // Reference the BuildManager
    }

    public int GetSellAmount (int cost)
	{
		return cost / 2;
	}

    public void BasicTurret()
    {   
        TurretData turretData = basicTurret.GetComponent<TurretData>();
        if (PlayerStats.Currency < turretData.cost)
        {
            Debug.Log("Not enough currency to build this turret!");
            turretToBuild = null;
        }
            else
        {
            Debug.Log(PlayerStats.Currency);
            turretToBuild = turretData.turretPrefab; // Set it locally for drag-and-drop
            //buildManager.SelectTurretToBuild(turretData.turretPrefab); // IN BuildManager not in use rn
            PlayerStats.Currency -= turretData.cost;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {   
        
        if (turretToBuild == null) 
        {
            return;
        }

        Vector3 tempPos = GetWorldPoint(eventData);
        tempPos.y += 0.5f;

        turretGhost = Instantiate(turretToBuild, tempPos, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (turretGhost != null)
        {
            Vector3 TempDragPos = GetWorldPoint(eventData);
            TempDragPos.y += 0.5f;

            turretGhost.transform.position = TempDragPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (turretToBuild != null && buildManager.CheckValidPlacement(turretGhost.transform.position))
        {

             Vector3 placementPosition = turretGhost.transform.position;
             placementPosition.y += 0.5f;

            Instantiate(turretToBuild, placementPosition, Quaternion.identity);
        }
        Destroy(turretGhost); // Clean up the ghost object
    }

    private Vector3 GetWorldPoint(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
