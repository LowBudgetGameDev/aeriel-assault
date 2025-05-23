using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap map;

    [SerializeField] private TileBase waterTile;
    [SerializeField] private RuleTile grassTile;
    [SerializeField] private RuleTile dirtTile;

    private int width = 500;
    private int height = 500;
    private float noiseScale = 25f;

    private float xOffset;
    private float yOffset;

    private void Awake()
    {
        xOffset = Random.Range(0f, 9999f);
        yOffset = Random.Range(0f, 9999f);
    }

    private void Start()
    {
        GenerateIslands();
    }

    private void GenerateIslands()
    {
        map.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float) x / width * noiseScale;
                float yCoord = (float) y / height * noiseScale;

                float noise = Mathf.PerlinNoise(xCoord + xOffset, yCoord + yOffset);
                TileBase chosenTile = waterTile;

                if (noise < 0.55f)
                    chosenTile = grassTile;

                map.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), chosenTile);
            }
        }

        map.RefreshAllTiles(); // Ensures RuleTiles update visually
    }
}
