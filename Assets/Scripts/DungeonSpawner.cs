﻿using System.Collections;
using UnityEngine;

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
    public IntRange numRooms = new IntRange(PlayerManager.Instance.m_currentLevel + 1, PlayerManager.Instance.m_currentLevel + 2);         
    public IntRange roomWidth = new IntRange(3, 10);         
    public IntRange roomHeight = new IntRange(3, 10);       
    public IntRange corridorLength = new IntRange(6, 10);   
    public GameObject[] floorTiles;                         
    public GameObject[] wallTiles;                         
    public GameObject[] outerWallTiles;                       
    public GameObject player;
    public GameObject slime;

    private TileType[][] tiles;                             
    private Room[] rooms;                                   
    private Corridor[] corridors;                            
    private GameObject tileHolder;                         


    private void Start()
    {
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

        for (int i = 1; i < rooms.Length; i++)
        {

            rooms[i] = new Room();


            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);


            if (i < corridors.Length)
            {

                corridors[i] = new Corridor();


                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }

            if (i == 1)
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
                Instantiate(player, playerPos, Quaternion.identity);

            }
            else
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);

                Instantiate(slime, playerPos, Quaternion.identity);
                Instantiate(slime, playerPos, Quaternion.identity);
                Instantiate(slime, playerPos, Quaternion.identity);
            }
        }

    }


    void SetTilesValuesForRooms()
    {

        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];


            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;


                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;


                    tiles[xCoord][yCoord] = TileType.Floor;
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

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            //gameover
        }
    }
}