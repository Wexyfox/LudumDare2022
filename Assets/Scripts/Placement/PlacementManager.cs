using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private Vector3 resetPosition;
    public SpriteRenderer offsetRenderer;
    private GameObject selectedPrefab;
    private int towerCost;

    [Header("Required prefabs and sprites")]
    public GameObject crystal;
    public Sprite crystalGhost;
    public int crystalCost;

    public GameObject frog;
    public Sprite frogGhost;
    public int frogCost;

    public GameObject wizard;
    public Sprite wizardGhost;
    public int wizardCost;

    public GameObject spike;
    public Sprite spikeGhost;
    public int spikeCost;

    public GameObject bottle;
    public Sprite bottleGhost;
    public int bottleCost;

    public GameObject slime;
    public Sprite slimeGhost;
    public int slimeCost;

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
        economyScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<EconomySystem>();
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
        towerCost = 0;
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
                    if (towerCost <= economyScript.CurrentMoney())
                    {
                        economyScript.SpendMoney(towerCost);
                        SpawnPrefab();
                    }
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
                towerCost = crystalCost;
            }
            else if (Input.GetKeyDown("w"))
            {
                selectedPrefab = frog;
                groundWaterIdentifier = "water";
                offsetRenderer.sprite = frogGhost;
                towerCost = frogCost;
            }
            else if (Input.GetKeyDown("e"))
            {
                selectedPrefab = wizard;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = wizardGhost;
                towerCost = wizardCost;
            }
            else if (Input.GetKeyDown("r"))
            {
                selectedPrefab = spike;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = spikeGhost;
                towerCost = spikeCost;
            }
            else if (Input.GetKeyDown("t"))
            {
                selectedPrefab = bottle;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = bottleGhost;
                towerCost = bottleCost;
            }
            else if (Input.GetKeyDown("y"))
            {
                selectedPrefab = slime;
                groundWaterIdentifier = "ground";
                offsetRenderer.sprite = slimeGhost;
                towerCost = slimeCost;
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
