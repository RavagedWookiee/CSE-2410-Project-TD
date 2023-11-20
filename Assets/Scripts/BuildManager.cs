using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject turretToBuild;

    public LayerMask invalidPlacementLayer;
    public LayerMask groundLayer;

    void Awake ()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}

		instance = this;
	} 

    public void SelectTurretToBuild(GameObject turret)
    {
        turretToBuild = turret; // Set the selected turret
    }

    public bool CheckValidPlacement(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider.CompareTag("Buildable"))
            {   
                Collider[] colliders = Physics.OverlapSphere(position, 0.01f, invalidPlacementLayer);
                foreach (var collider in colliders)
                {
                    Debug.Log("Collider blocking placement: " + collider.gameObject.name);
                }
                return colliders.Length == 0;
            }
        }
        return false; // Not a valid place
    }
}
