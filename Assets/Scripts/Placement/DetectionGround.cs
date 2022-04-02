using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionGround : MonoBehaviour
{
    private bool placementTilesValid;
    private bool obstruction = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SolidGround"))
        {
            placementTilesValid = true;
            Debug.Log("Tower is over ground");
        }

        if (collision.gameObject.CompareTag("TowerBaseSolid"))
        {
            obstruction = true;
            Debug.Log("Tower has obstruction");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SolidGround"))
        {
            placementTilesValid = false;
            Debug.Log("Tower is no longer over ground");
        }

        if (collision.gameObject.CompareTag("TowerBaseSolid"))
        {
            obstruction = false;
            Debug.Log("Tower no longer has obstruction");
        }
    }

    public bool ValidPlacementCheck()
    {
        if (obstruction == false & placementTilesValid == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
