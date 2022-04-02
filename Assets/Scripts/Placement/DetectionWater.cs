using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionWater : MonoBehaviour
{
    private bool placementTilesValid = false;
    private bool obstruction = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LiquidWater"))
        {
            placementTilesValid = true;
            Debug.Log("Tower is over water");
        }

        if (collision.gameObject.CompareTag("TowerBaseLiquid"))
        {
            obstruction = true;
            Debug.Log("Tower has obstruction");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LiquidWater"))
        {
            placementTilesValid = false;
            Debug.Log("Tower is no longer over water");
        }

        if (collision.gameObject.CompareTag("TowerBaseLiquid"))
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
