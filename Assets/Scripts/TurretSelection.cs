using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSelection : MonoBehaviour
{
   private Tower selectedTurret;
   public Camera mainCamera;
   public GameObject sellButton;
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

    private void DeselectTurret()
    {
        selectedTurret = null;
        sellButton.SetActive(false);
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
}
