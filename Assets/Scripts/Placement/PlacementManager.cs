using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private Vector3 resetPosition;
    public SpriteRenderer offsetRenderer;
    private GameObject selectedPrefab;

    [Header("Required prefabs and sprites")]
    public GameObject crystal;
    public Sprite crystalGhost;

    public GameObject frog;
    public Sprite frogGhost;

    public GameObject wizard;
    public Sprite wizardGhost;

    public GameObject spike;
    public Sprite spikeGhost;

    public GameObject bottle;
    public Sprite bottleGhost;

    public GameObject slime;
    public Sprite slimeGhost;

    private DetectionGround groundCheck;
    private DetectionWater waterCheck;
    private bool validPlacement;

    private string groundWaterIdentifier;
    private bool placingMode;

    private float tempX;
    private float tempY;

    private EconomySystem economyScript;

    private void Start()
    {
        resetPosition = new Vector3(transform.position.x, transform.position.y);
        groundCheck = gameObject.GetComponent<DetectionGround>();
        waterCheck = gameObject.GetComponent<DetectionWater>();
        economyScript = GameObject.FindGameObjectWithTag("TowerSpawner").GetComponent<EconomySystem>();
    }

    private void SpawnPrefab()
    {
        Instantiate(selectedPrefab, gameObject.transform.position, gameObject.transform.rotation);
        ResetgameObject();
    }

    private void ResetgameObject()
    {
        gameObject.transform.position = resetPosition;
        selectedPrefab = null;
        groundWaterIdentifier = null;
        offsetRenderer.sprite = null;
        validPlacement = false;
        placingMode = false;
    }

    private void Update()
    {
        if (placingMode)
        {
            if (Input.GetKeyDown("q") || Input.GetKeyDown("w") || Input.GetKeyDown("e") || Input.GetKeyDown("r") || Input.GetKeyDown("t") || Input.GetKeyDown("y"))
            {
                placingMode = false;
                ResetgameObject();
            }

            if (Input.GetKeyDown("left"))
            {
                tempX = gameObject.transform.position.x - 0.5f;
                tempY = gameObject.transform.position.y - 0.25f;
                gameObject.transform.position = new Vector3(tempX, tempY);
            }
            else if (Input.GetKeyDown("right"))
            {
                tempX = gameObject.transform.position.x + 0.5f;
                tempY = gameObject.transform.position.y + 0.25f;
                gameObject.transform.position = new Vector3(tempX, tempY);
            }
            else if (Input.GetKeyDown("up"))
            {
                tempX = gameObject.transform.position.x - 0.5f;
                tempY = gameObject.transform.position.y + 0.25f;
                gameObject.transform.position = new Vector3(tempX, tempY);
            }
            else if (Input.GetKeyDown("down"))
            {
                tempX = gameObject.transform.position.x + 0.5f;
                tempY = gameObject.transform.position.y - 0.25f;
                gameObject.transform.position = new Vector3(tempX, tempY);
            }

            if (Input.GetKeyDown("space"))
            {
                PlacementCheck();
                if (validPlacement)
                {
                    SpawnPrefab();
                }
            }
        }
        else if (Input.GetKeyDown("q") || Input.GetKeyDown("w") || Input.GetKeyDown("e") || Input.GetKeyDown("r") || Input.GetKeyDown("t") || Input.GetKeyDown("y"))
        {
            placingMode = true;
            if (Input.GetKeyDown("q"))
            {
                selectedPrefab = crystal;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = crystalGhost;
            }
            else if (Input.GetKeyDown("w"))
            {
                selectedPrefab = frog;
                groundWaterIdentifier = "water";
                offsetRenderer.sprite = frogGhost;
            }
            else if (Input.GetKeyDown("e"))
            {
                selectedPrefab = wizard;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = wizardGhost;
            }
            else if (Input.GetKeyDown("r"))
            {
                selectedPrefab = spike;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = spikeGhost;
            }
            else if (Input.GetKeyDown("t"))
            {
                selectedPrefab = bottle;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = bottleGhost;
            }
            else if (Input.GetKeyDown("y"))
            {
                selectedPrefab = slime;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = slimeGhost;
            }
        }
    }

    private void PlacementCheck()
    {
        if (groundWaterIdentifier == "water")
        {
            validPlacement = waterCheck.WaterPlacementCheck();
        }
        else if (groundWaterIdentifier == "ground")
        {
            validPlacement = groundCheck.GroundPlacementCheck();
        }
        else 
        {
            validPlacement = false;
        }
    }
}
