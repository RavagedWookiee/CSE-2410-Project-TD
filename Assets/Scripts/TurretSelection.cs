using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSelection : MonoBehaviour
{
   private Tower selectedTurret;
   public Camera mainCamera;
   public GameObject sellButton;
   public GameObject rangeButton;
   public GameObject fasterFireButton;
   public GameObject damageUpButton;
   private Shop shop;

   void Start()
    {
        shop = FindObjectOfType<Shop>(); // Find the Shop instance in the scene
    }

   void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if(hit.collider.CompareTag("Turret"))
                {
                    Debug.Log("hit");
                    selectedTurret = hit.collider.GetComponent<Tower>();
                    PositionSellButton(hit.transform.position);
                    PositionRangeButton(hit.transform.position);
                    PositionDamageButton(hit.transform.position);
                    PositionFireRateButton(hit.transform.position);
                }
                else
                {
                    DeselectTurret();
                }
            }
            else
            {
                DeselectTurret();
            }
        }
    }

    private void PositionSellButton(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        screenPosition.y -= 40f;
        sellButton.transform.position = screenPosition;

        sellButton.SetActive(true);
    }

    private void PositionRangeButton(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        screenPosition.y += 80f;
        rangeButton.transform.position = screenPosition;

        rangeButton.SetActive(true);
    }

    private void PositionDamageButton(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        screenPosition.x -= 80f;
        screenPosition.y += 20f;
        damageUpButton.transform.position = screenPosition;

        damageUpButton.SetActive(true);
    }

    private void PositionFireRateButton(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        screenPosition.x += 80f;
        screenPosition.y += 20f;
        fasterFireButton.transform.position = screenPosition;

        fasterFireButton.SetActive(true);
    }

    private void DeselectTurret()
    {
        selectedTurret = null;
        sellButton.SetActive(false);
        fasterFireButton.SetActive(false);
        damageUpButton.SetActive(false);
        rangeButton.SetActive(false);
    }

    public void OnSellButtonClick()
    {
        if(selectedTurret != null)
        {
            shop.SellTurret(selectedTurret);
            selectedTurret = null;
            DeselectTurret();
        }
    }

    public void OnRangeButtonClick()
    {
        if(selectedTurret != null && PlayerStats.Currency >= 250 && selectedTurret.rangeUp != true)
        {
            selectedTurret.range += 10f;
            PlayerStats.Currency -= 250;
            selectedTurret.rangeUp = true;
            selectedTurret = null;
            DeselectTurret();
        }
    }

    public void OnFasterFireButtonClick()
    {
        if(selectedTurret != null && PlayerStats.Currency >= 250 && selectedTurret.fireRateUp != true)
        {
            selectedTurret.fireRate += 2f;
            PlayerStats.Currency -= 250;
            selectedTurret.fireRateUp = true;
            selectedTurret = null;
            DeselectTurret();
        }
    }

    public void OnDamageButtonClick()
    {
        if(selectedTurret != null && PlayerStats.Currency >= 250 && selectedTurret.damageUp != true)
        {
            selectedTurret.damage += 2f;
            PlayerStats.Currency -= 250;
            selectedTurret.damageUp = true;
            selectedTurret = null;
            DeselectTurret();
        }
    }
}
