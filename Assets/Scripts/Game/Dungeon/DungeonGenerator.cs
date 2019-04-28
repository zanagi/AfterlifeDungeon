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
    [Header("Dungeon settings")]
    public int rows;
    public int cols;
    public IntRange roomCountRange, roomSizeRange;
    public TileToObject[] tilePrefabs;

    [Header("Player")]
    public PlayerSpawnEvent playerSpawnEvent;

    [Header("Interactables")]
    public IntRange interactableCountRange;
    public BaseAI[] interactablePrefabs;

    [Header("Enemies")]
    public EnemySpawnEvent enemySpawnEvent;
    public IntRange enemyCountRange;
    public BaseAI[] enemyPrefabs;

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

        SpawnPlayer(roomArray);
        SpawnInteractables(roomArray);
        SpawnEnemies(roomArray);
        CreateCorridors(roomArray, ref tiles);
    }

    private void SpawnPlayer(Room[] rooms)
    {
        Room spawnRoom = Static.GetRandom(rooms);
        playerSpawnEvent.Spawn(new Vector3(spawnRoom.RandomX, 0, spawnRoom.RandomY));
    }

    private void SpawnInteractables(Room[] rooms)
    {
        int count = interactableCountRange.Random;

        // TODO: Check for spawn on same space?
        for(int i = 0; i < count; i++)
        {
            Room spawnRoom = Static.GetRandom(rooms);
            BaseAI prefab = interactablePrefabs.GetRandom();
            Vector3 spawnPos = new Vector3(spawnRoom.RandomX, 0, spawnRoom.RandomY);
            enemySpawnEvent.Spawn(prefab, spawnPos);
        }
    }

    private void SpawnEnemies(Room[] rooms)
    {
        int count = enemyCountRange.Random;

        // TODO: Check for spawn on same space?
        for (int i = 0; i < count; i++)
        {
            Room spawnRoom = Static.GetRandom(rooms);
            BaseAI prefab = enemyPrefabs.GetRandom();
            Vector3 spawnPos = new Vector3(spawnRoom.RandomX, 0, spawnRoom.RandomY);
            enemySpawnEvent.Spawn(prefab, spawnPos);
        }
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
        for(int x = 0; x < cols; x++)
        {
            for(int y = 0; y < rows; y++)
            {
                if(tiles[x, y] == Tile.Empty && 
                    (IsTileFloor(tiles, x + 1, y) || IsTileFloor(tiles, x, y + 1) ||
                    IsTileFloor(tiles, x - 1, y) || IsTileFloor(tiles, x, y - 1)))
                {
                    tiles[x, y] = Tile.Wall;
                }
            }
        }
    }

    private bool IsTileFloor(Tile[,] tiles, int x, int y)
    {
        if(x < 0 || x >= cols || y < 0 || y >= rows)
        {
            return false;
        }
        return tiles[x, y].IsFloor();
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
