using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mendelbrotTextureGenerator : MonoBehaviour
{
	public enum ObjectMode {Julia, MendelbrotSet};
    public ObjectMode objectMode;

    public enum ColorMode {basic, escapedTime, potential};
    public ColorMode colorMode;

   	public enum AlgorithmMode {normal, hyperbolic, boundaryDetection};
    public AlgorithmMode algorithmMode;

    public int height;
    public int width;

    /*public Vector2<double> coordinates = new Vector2<double>(0,0);*/

    public int N = 10000;
    public int zoom = 0;

    /*public Vector2<double> radius = new Vector2<double>(2 / pow(2, zoom),(2 / pow(2, zoom) * height) / width);
	public Vector2<double> two_radius = new Vector2<double>(radius.x * 2,radius.y * 2;*/

    public float K = 1f;
    public int P = 100 * 100;

    public double epsilon = 0.001;

    public float r = 1f;
    public float g = 0.5f;
    public float b = 0.3f;

    public float marg = 0.05f;

    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height){
		Texture2D texture = new Texture2D(width,height);
		texture.filterMode = FilterMode.Point;
		/*texture.warpMode = TextureWarpMode.Clamp;*/
		texture.SetPixels(colourMap);
		texture.Apply();
		return texture;
	}

    public Texture2D mendelbrotTexture(){
    	Color[]	colourMap = new Color[width * height];
    	for(int y = 0; y < height; y++){
 			for(int x = 0; x < width; x++){
 				float c  = 0;
 				float z = c;
 				int i = 0;
 				int power = 1;
 				while(i < N && z < P){
 					z += z * z + c;
 					i++;
 					power *= 2;
 				}
 				colourMap[y * width + x] = Color.Lerp(Color.black,Color.white, i);
 			}
 		}
 		return TextureFromColourMap(colourMap, width, height);
    }
}
