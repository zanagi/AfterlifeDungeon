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
    public PlayerSpawnEvent playerSpawnEvent;

    private void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        // Define dungeon variables
        Tile[,] tiles = new Tile[cols, rows];

        CreateRooms(roomCountRange.Random, ref tiles);
        CreateWalls(ref tiles);
        CreateTiles(tiles);
    }

    private void CreateRooms(int roomCount, ref Tile[,] tiles)
    {
        List<Room> rooms = new List<Room>();

        for (int i = 0; i < roomCount; i++)
        {
            // Initialize room
            Room room = new Room(
                Random.Range(1, rows - 1 - roomSizeRange.m_Max), Random.Range(1, cols - 1 - roomSizeRange.m_Max), 
                roomSizeRange.Random, roomSizeRange.Random);

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
        Room[] roomArray = rooms.ToArray();

        // Spawn player
        Room spawnRoom = Static.GetRandom(roomArray);
        playerSpawnEvent.Spawn(new Vector3(spawnRoom.RandomX, 0, spawnRoom.RandomY));

        CreateCorridors(roomArray, ref tiles);
    }

    private void CreateCorridors(Room[] rooms, ref Tile[,] tiles)
    {
        Static.Shuffle(rooms);

        for(int i = 0; i < rooms.Length; i++)
        {
            Room room = rooms[i];
            Room other = rooms[(i + 1) % rooms.Length];

            // Get coordinate values
            int x1 = room.RandomX, y1 = room.RandomY,
                x2 = other.RandomX, y2 = other.RandomY;

            int deltaX = x2 > x1 ? 1 : -1;
            int deltaY = y2 > y1 ? 1 : -1;

            for (int x = x1; x != x2; x += deltaX)
            {
                tiles[x, y1] = tiles[x, y1] == Tile.Floor ? Tile.Floor : Tile.Corridor;
            }

            for(int y = y1; y != y2; y += deltaY)
            {
                tiles[x2, y] = tiles[x2, y] == Tile.Floor ? Tile.Floor : Tile.Corridor;
            }
        }
    }

    private void CreateWalls(ref Tile[,] tiles)
    {
        for(int x = 1; x < cols - 1; x++)
        {
            for(int y = 1; y < rows - 1; y++)
            {
                if(tiles[x, y] == Tile.Empty && 
                    (tiles[x + 1, y].IsFloor() || tiles[x, y + 1].IsFloor() || 
                    tiles[x - 1, y].IsFloor() || tiles[x, y - 1].IsFloor()))
                {
                    tiles[x, y] = Tile.Wall;
                }
            }
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
