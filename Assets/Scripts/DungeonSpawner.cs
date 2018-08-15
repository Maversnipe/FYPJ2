using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSpawner : MonoBehaviour
{
    // The type of tile that will be laid in a specific position.
    public enum TileType
    {
        Wall = 0,
        Floor
    }


    public int columns = 100;                                 
    public int rows = 100;
    public IntRange numRooms;         
    public IntRange roomWidth = new IntRange(3, 10);         
    public IntRange roomHeight = new IntRange(3, 10);       
    public IntRange corridorLength = new IntRange(6, 10);   
    public GameObject[] floorTiles;                         
    public GameObject[] wallTiles;                         
    public GameObject[] outerWallTiles;                       
    public GameObject player;
    public GameObject slime;
    public Text objectiveText;

    private TileType[][] tiles;                             
    private Room[] rooms;                                   
    private Corridor[] corridors;                            
    private GameObject tileHolder;

    private bool GameWin = false;
    private bool GameLose = false;      


    private void Start()
    {

        numRooms = new IntRange(PlayerManager.Instance.m_currentLevel + 1, PlayerManager.Instance.m_currentLevel + 2);
        // Create the board holder.
        tileHolder = new GameObject("tileHolder");

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();
    }


    void SetupTilesArray()
    {

        tiles = new TileType[columns][];


        for (int i = 0; i < tiles.Length; i++)
        {

            tiles[i] = new TileType[rows];
        }
    }


    void CreateRoomsAndCorridors()
    {

        rooms = new Room[numRooms.Random];


        corridors = new Corridor[rooms.Length - 1];


        rooms[0] = new Room();
        corridors[0] = new Corridor();

   
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);


        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);
        Vector3 playerPos = new Vector3(rooms[0].xPos, rooms[0].yPos, 0);


        GameObject.FindGameObjectsWithTag("Player")[0].transform.position = playerPos;

        for (int i = 1; i < rooms.Length; i++)
        {

            rooms[i] = new Room();


            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);


            if (i < corridors.Length)
            {

                corridors[i] = new Corridor();


                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }
        }

    }


    void SetTilesValuesForRooms()
    {

        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            int xCoord = 0;
            int yCoord = 0;

            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                xCoord = currentRoom.xPos + j;


                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    yCoord = currentRoom.yPos + k;


                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }


            Vector2 start = new Vector2(currentRoom.xPos, currentRoom.yPos);
            Vector2 end = new Vector2(currentRoom.xPos + currentRoom.roomWidth - 1, currentRoom.yPos + currentRoom.roomHeight - 1);


            if (i != 0)
            {
                Vector3 playerPos = new Vector3(Random.Range(start.x, end.x), Random.Range(start.y, end.y), 0);
                Instantiate(slime, playerPos, Quaternion.identity);
                playerPos = new Vector3(Random.Range(start.x, end.x), Random.Range(start.y, end.y), 0);
                Instantiate(slime, playerPos, Quaternion.identity);
                for (int m = 0; m < GameObject.FindGameObjectsWithTag("Enemy").Length; m++)
                {
                    if (m > GameObject.FindGameObjectsWithTag("Enemy").Length - 3)
                    {
                        GameObject.FindGameObjectsWithTag("Enemy")[m].GetComponent<AI>().roomStart.Set(start.x, start.y);
                        GameObject.FindGameObjectsWithTag("Enemy")[m].GetComponent<AI>().roomEnd.Set(end.x, end.y);
                    }
                }
            }
        }
    }


    void SetTilesValuesForCorridors()
    {

        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];


            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {

                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;


                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }


                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }


    void InstantiateTiles()
    {

        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {

                InstantiateFromArray(floorTiles, i, j);


                if (tiles[i][j] == TileType.Wall)
                {

                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }


    void InstantiateOuterWalls()
    {
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;


        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);


        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }


    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {

        float currentY = startingY;


        while (currentY <= endingY)
        {

            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = tileHolder.transform;
    }

    void Update()
    {
        if (!PlayerManager.Instance.pause)
        {
            objectiveText.text = " Number of enemies left: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (PlayerManager.Instance.invulnerable)
            {
                objectiveText.text += "\n INVULNERABLE";
            }
            if (PlayerManager.Instance.unlimited)
            {
                objectiveText.text += "\n UNLIMITED";
            }
            if (PlayerManager.Instance.allSkills)
            {
                objectiveText.text += "\n ALL SKILLS ACTIVATED";
            }
            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            {
                //gameover
                GameWin = true;
            }
            if(PlayerManager.Instance.m_currentHealth <= 0)
            {
                GameLose = true;
            }

            if(GameWin)
            {
                PlayerManager.Instance.pause = true;
                GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(0).gameObject.SetActive(true);
                Rune newItem;
                // Randomise Rune
                int randNum = (int)Random.Range(0, 10);
                if(randNum % 2 == 0)
                { // Attack Rune
                    newItem = new Rune("Weapon Rune", Item.ItemType.Weapon_Rune);
                    newItem.RandomiseRune();
                    GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(0).GetChild(0).GetComponent<WinSlot>().m_item
                        = newItem;
                }
                else
                { // Defense Rune
                    newItem = new Rune("Armour Rune", Item.ItemType.Armour_Rune);
                    newItem.RandomiseRune();
                    GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(0).GetChild(0).GetComponent<WinSlot>().m_item
                        = newItem;
                }
                GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite
                    = GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(0).GetChild(0).GetComponent<WinSlot>().m_item.m_itemIcon;

                GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text 
                    = newItem.m_itemName;

                GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text
                    = newItem.m_itemDesc;
            }

            if (GameLose)
            {
                PlayerManager.Instance.pause = true;
                GameObject.FindGameObjectWithTag("GameoverScreen").transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}