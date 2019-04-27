using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileToObject
{
    public Tile tile;
    public GameObject prefab;
}

public class DungeonGenerator : MonoBehaviour
{
    public int rows, cols;
    public IntRange roomCountRange, roomSizeRange;
    public TileToObject[] tilePrefabs;

    private void Awake()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        // Define dungeon variables
        Tile[,] tiles = new Tile[rows, cols];

        CreateRooms(roomCountRange.Random, ref tiles);
        CreateTiles(tiles);
    }

    private void CreateRooms(int roomCount, ref Tile[,] tiles)
    {
        List<Room> rooms = new List<Room>();

        for (int i = 0; i < roomCount; i++)
        {
            // Initialize room
            Room room = new Room(
                Random.Range(1, rows - 1), Random.Range(1, cols - 1), roomSizeRange.Random, roomSizeRange.Random);

            // Set tile values
            for (int x = room.x; x <= room.x + room.width; x++)
            {
                for (int y = room.y; y <= room.y + room.height; y++)
                {
                    tiles[x, y] = Tile.Floor;
                }
            }
            // Add room to list
            rooms.Add(room);
        }
    }

    private void CreateTiles(Tile[,] tiles)
    {
        for(int x = 0; x < rows; x++)
        {
            for(int y = 0; y < cols; y++)
            {
                Tile tile = tiles[x, y];
                GameObject tilePrefab = GetTilePrefab(tile);

                if(tilePrefab)
                {
                    GameObject tileObject = Instantiate(tilePrefab, transform);
                    tileObject.transform.localPosition = new Vector3(x, 0, y);
                }
            }
        }
    }

    private GameObject GetTilePrefab(Tile tile)
    {
        for(int i = 0; i < tilePrefabs.Length; i++)
        {
            if (tilePrefabs[i].tile == tile)
                return tilePrefabs[i].prefab;
        }
        return null;
    }
}
