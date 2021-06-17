using System;
using UnityEngine;

public class Water : MonoBehaviour
{ 
   [Header("Water")]
   Vector3[] _verticies;
   int[] _triangles;
   private Vector2[] _uvs;
   [Range(0, 256)] public int size;
   [Range(1, 8)] public int octaves;
   public float height, speed, frequency;
   float _off = .0f;

   private void Start()
   {
      if (PlayerPrefs.GetInt("QualityIndex") == 0)
      {
         this.gameObject.SetActive(false);
      }
      else
      {
         this.gameObject.SetActive(true);
      }
   }

   private void Update()
   {
      _verticies = new Vector3[(size + 1) * (size + 1)];
      _triangles = new int[size * size * 6];
      _uvs = new Vector2[(size + 1) * (size + 1)];

      for (int x = 0, a = 0; x <= size; x++)
      {
         for (int y = 0; y <= size; y++, a++)
         {
            float z = 0.0f;
            for (int i = 0; i < octaves; i++)
            {
               z += Mathf.PerlinNoise(((float)x * frequency / 10 + _off) * i, ((float)y * frequency / 10 + _off) * i) * height * i;
            }
            _verticies[a] = new Vector3(x, z, y);
            _uvs[a] = new Vector2((float)x / (float)size, (float)y / (float)size);
         }
      }
      _off += speed / 100;
      for (int z = 0, vert = 0, tris = 0; z < size; z++, vert++)
      {
         for (int x = 0; x < size; x++, vert++, tris+=6)
         {
            _triangles[tris + 0] = vert + 0;
            _triangles[tris + 1] = vert + 1 + size;
            _triangles[tris + 2] = vert + 1;
            _triangles[tris + 3] = vert + 1;
            _triangles[tris + 4] = vert + 1 + size;
            _triangles[tris + 5] = vert + 2 + size;

         }
      }

      GetComponent<MeshFilter>().mesh = new Mesh()
      {
         vertices = _verticies,
         triangles = _triangles,
         uv = _uvs
      };
   }
}
