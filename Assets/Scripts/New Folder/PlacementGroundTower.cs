using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementGroundTower : MonoBehaviour
{
    private bool placementTilesValid;
    private int obstructions = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LiquidWater"))
        {
            placementTilesValid = false;
            Debug.Log("Tower is over water");
        }

        if (collision.gameObject.CompareTag("EnemyTrack"))
        {
            placementTilesValid = false;
            Debug.Log("Tower is over the enemy track");
        }

        if (collision.gameObject.CompareTag("SolidGround"))
        {
            placementTilesValid = true;
            Debug.Log("Tower is over ground");
        }

        if (collision.gameObject.CompareTag("TowerBaseSolid") || collision.gameObject.CompareTag("TowerBaseLiquid"))
        {
            obstructions += 1;
            Debug.Log(obstructions);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SolidGround"))
        {
            placementTilesValid = false;
            Debug.Log("Tower is no longer over ground");
        }

        if (collision.gameObject.CompareTag("TowerBaseSolid") || collision.gameObject.CompareTag("TowerBaseLiquid"))
        {
            obstructions -= 1;
            Debug.Log(obstructions);
        }
    }

    public bool ValidPlacementCheck()
    {
        if (obstructions == 0 & placementTilesValid == true)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }
}
