using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    BuildManager buildManager;
    private int selectedTurretCost;
    private TurretData selectedTurret;
    private GameObject turretToBuild;
    public GameObject basicTurret;
    public GameObject quadTurret;
    public GameObject slowTurret;
    public GameObject supportTurret;
    public GameObject artyTurret;

    private GameObject turretGhost;

    [SerializeField] private LayerMask buildableLayerMask;

    void Start()
    {
        buildManager = BuildManager.instance; // Reference the BuildManager
    }

    public int GetSellAmount (Tower turret)
	{
        TurretData turretData = turret.GetComponent<TurretData>();
		return turretData.cost / 2;
	}

     public void SellTurret(Tower turret)
    {
        int sellAmount = GetSellAmount(turret);
        PlayerStats.Currency += sellAmount;
        Destroy(turret.gameObject);
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
            turretToBuild = turretData.turretPrefab; // Set it locally for drag-and-drop
            selectedTurretCost = turretData.cost;
            //buildManager.SelectTurretToBuild(turretData.turretPrefab); // IN BuildManager not in use rn
        }
    }

    public void QuadTurret()
    {   
        TurretData turretData = quadTurret.GetComponent<TurretData>();
        if (PlayerStats.Currency < turretData.cost)
        {
            Debug.Log("Not enough currency to build this turret!");
            turretToBuild = null;
        }
        else
        {
            turretToBuild = turretData.turretPrefab;
            selectedTurretCost = turretData.cost;
        }
    }

    public void SlowTurret()
    {   
        TurretData turretData = slowTurret.GetComponent<TurretData>();
        if (PlayerStats.Currency < turretData.cost)
        {
            Debug.Log("Not enough currency to build this turret!");
            turretToBuild = null;
        }
        else
        {
            turretToBuild = turretData.turretPrefab;
            selectedTurretCost = turretData.cost;
        }
    }

    public void SupportTurret()
    {   
        TurretData turretData = supportTurret.GetComponent<TurretData>();
        if (PlayerStats.Currency < turretData.cost)
        {
            Debug.Log("Not enough currency to build this turret!");
            turretToBuild = null;
        }
        else
        {
            turretToBuild = turretData.turretPrefab;
            selectedTurretCost = turretData.cost;
        }
    }

    public void ArtyTurret()
    {   
        TurretData turretData = artyTurret.GetComponent<TurretData>();
        if (PlayerStats.Currency < turretData.cost)
        {
            Debug.Log("Not enough currency to build this turret!");
            turretToBuild = null;
        }
        else
        {
            turretToBuild = turretData.turretPrefab;
            selectedTurretCost = turretData.cost;
        }
    }    

    
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject selectedTurret = eventData.pointerCurrentRaycast.gameObject;

        if (selectedTurret != null) 
        {
            if(selectedTurret.name == "BasicTurret")
            {
                BasicTurret();
            }
            if(selectedTurret.name == "QuadTurret")
            {
                QuadTurret();
            }
            if(selectedTurret.name == "SlowTower")
            {
                SlowTurret();
            }
            if(selectedTurret.name == "SupportTower")
            {
                SupportTurret();
            }
            if(selectedTurret.name == "ArtyTower")
            {
                ArtyTurret();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {   
        
        if (turretToBuild == null) 
        {
            return;
        }

        Vector3 tempPos = GetWorldPoint(eventData);

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

            Instantiate(turretToBuild, placementPosition, Quaternion.identity);
            PlayerStats.Currency -= selectedTurretCost;
        }
        else
        {
            //Debug.Log("failed to place");
            //Debug.Log(turretToBuild);
            //Debug.Log(buildManager.CheckValidPlacement(turretGhost.transform.position));
        }

        Destroy(turretGhost); // Clean up the ghost object
    }

    private Vector3 GetWorldPoint(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildableLayerMask))
        {
            //Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
            return hit.point;
        }
        else
        {
           //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
        }
        return Vector3.zero;
    }
}
