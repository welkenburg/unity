using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class createplane : MonoBehaviour
{

    public int xSize, ySize, tSize, height,octaves;
    public Vector3[] vertices;

    public Color[] colors;
    public Gradient gradient;

    public float scale,persistance, lacunarity,maxHeight,minHeight;

    public GameObject Camera;

    private void Awake()
    {
        Generate();
    }

    private void Update()
    {
        transform.position = new Vector3(Camera.transform.position.x - ((xSize * tSize) / 2), transform.position.y, Camera.transform.position.z - ((ySize * tSize) / 2));
        /*if (Input.GetKeyDown(KeyCode.Space)) */Generate();
    }

    private Mesh mesh;

    private void Generate()
    {

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        if (scale < 0)
        {
            scale = 0.02f;
        }

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for(int j = 0; j < octaves; j++)
                {
                    float sampleX = (x + transform.position.x) * scale * frequency;
                    float sampleY = (y + transform.position.z) * scale * frequency;
                    float noise = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += noise * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                vertices[i] = new Vector3(x * tSize, noiseHeight * height, y * tSize);
                if (noiseHeight * height > maxHeight) maxHeight = noiseHeight * height;
                if (noiseHeight * height < minHeight) minHeight = noiseHeight * height;
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;

        colors = new Color[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float cheight = Mathf.InverseLerp(minHeight, maxHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(cheight);
            }
        }
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }
}