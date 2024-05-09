using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GenerationModeEnum
    {
        Line1D,
        Grid2D,
        Volume3D,
        Helix
    };

public enum NamingModeEnum
    {
        PrefabName,
        GeneratorName,
        CustomName
    }

public class Array : MonoBehaviour
{   
    [Header("Instantiation settings")]
    [Tooltip("The prefab (or gameobject) that will be instanciated on generation")]
    public GameObject prefab;
    [Tooltip("Specify if the link with the original prefab should be maintained or not on the instantiated objects")]
    public bool preservePrefab = true;
    [Tooltip("Specify if instances should be regenerated or not when entering play-mode (interesting only if you want a random generation each time)")]
    public bool generateOnStart=false;
    [Tooltip("Specify if the generated objects should be placed in a new GameObject, or be children of the present generator object")]
    public bool makeNewObject = false;
    [Tooltip("Specify if the newly created container object should be placed at the generator's local pivot point, or at the world space's origin")]
    public bool inPlace = true;
    GameObject newGameObject;
    [Tooltip("Specify how each individual generated object should be named. Doesn't affect the row and floor groups, nor the new container (if any)")]
    public NamingModeEnum namingMode;
    string goName;
    [Tooltip("Choose your custom name for each individual generated object. It will be followed by _0, _1, _2 etc. _x")]
    public string customName = "UnnamedObject";

    [Space]
    [Tooltip("Choose how your objects are placed")]
    public GenerationModeEnum generationMode;

    // 1D
    [Tooltip("The amount of objects generated")]
    public int count1D;
    [Tooltip("The amount of objects generated for each dimension")]
    public Vector2 count2D;
    [Tooltip("The amount of objects generated for each dimension")]
    public Vector3 count3D;

    [Space]

    [Header("Offset settings")]
    [Tooltip("The 3 dimensional position offset between each object of a single row")]
    public Vector3 objectOffset;
    [Tooltip("The 3 dimensional position offset between each row")]
    public Vector3 rowOffset;
    [Tooltip("The 3 dimensional position offset between each floor")]
    public Vector3 floorOffset;
    [Space]
    [Tooltip("Specify if the prefab's rotation should be reset. Only useful if the prefab's orientation is not (0,0,0)")]
    public bool resetRotation=false;
    [Tooltip("Specify if objects are generated according to the generator's pivot, or in absolute position")]
    public bool applyParentRotation=true;
    [Tooltip("Specify if objects are generated centered on their container, or extending away from the pivot (if you need a precise start point independently from the object count)")]
    public bool center=true;
    Vector3 centerOffset;
    Quaternion rotationBuffer;
    GameObject row;
    GameObject floor;
   
    [Space]
    // Randomization
    [Header("Randomizer")]
    [Tooltip("Specify if the generated prefab should be random within the specified list.")]
    public bool randomPrefab;
    [Tooltip("startPrefabs are generated only for the first generated object of the first dimension. Defaults to midPrefabs if empty")]
    public List<GameObject> startPrefabs;
    [Tooltip("Prefabs generated globally, except when it's a 'start' or 'end' case. Defaults to the original single prefab if empty")]
    public List<GameObject> midPrefabs;
    [Tooltip("endPrefabs are generated only for the last generated object of the last dimension. Defaults to midPrefabs if empty")]
    public List<GameObject> endPrefabs;
    public bool randomSprite;
    public List<Sprite> startSprites;
    public List<Sprite> midSprites;
    public List<Sprite> endSprites;

#if UNITY_EDITOR

    public void Generate()
    {
        rotationBuffer = transform.rotation;
        Clear();
        if (namingMode == NamingModeEnum.PrefabName)
        {goName = prefab.name;}
        if (namingMode == NamingModeEnum.GeneratorName)
        {goName = gameObject.name;}
        if (namingMode == NamingModeEnum.CustomName)
        {goName = customName;}
        if (applyParentRotation)
        {
            transform.rotation = Quaternion.identity;
        }

        newGameObject = gameObject;
        if (makeNewObject)
        {
            newGameObject = new GameObject($"{goName}_array");
            if (inPlace)
            {
                newGameObject.transform.position = transform.position;
            }
        }

        if (generationMode == GenerationModeEnum.Line1D)
        {
            Linear1D(newGameObject, count1D, objectOffset);
        }
        if (generationMode == GenerationModeEnum.Grid2D)
        {   
            Grid2D(newGameObject, count2D);
        }
        if (generationMode == GenerationModeEnum.Volume3D)
        {   
            Volume3D(newGameObject, count3D);
        }
        transform.rotation = rotationBuffer;
    }

    public void Clear()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void Volume3D(GameObject parent, Vector3 count)
    {
        for (int i = 0 ; i<count.z ; i++)
            {
                floor = new GameObject($"Floor_{i}");
                floor.transform.parent = parent.transform;
                Grid2D(floor, new Vector2(count3D.x, count3D.y));
                if (center)
                {
                    centerOffset = new Vector3((count3D.z-1)*floorOffset.x/2*-1, (count3D.z-1)*floorOffset.y/2*-1, (count3D.z-1)*floorOffset.z/2*-1);
                }
                floor.transform.position = transform.position + floorOffset*i + centerOffset;
            }
    }

    public void Grid2D(GameObject parent, Vector2 count)
    {
        for (int i = 0 ; i<(int)count.y ; i++)
            {
                row = new GameObject($"Row_{i}");
                row.transform.parent = parent.transform;
                Linear1D(row, (int)count.x, objectOffset);
                if (center)
                {
                    centerOffset = new Vector3((count.y-1)*rowOffset.x/2*-1, (count.y-1)*rowOffset.y/2*-1, (count.y-1)*rowOffset.z/2*-1);
                }
                row.transform.position = parent.transform.position + rowOffset*i + centerOffset;
            }
    }

    public void Linear1D(GameObject parent, int count, Vector3 offset)
    {
        GameObject instanciatedPrefab;
        for (int i=0 ; i < count ; i++){
            GameObject prefabToSpawn = this.prefab;
            Quaternion rotation = prefabToSpawn.transform.rotation;
            if (resetRotation)
            {
                rotation = Quaternion.identity;
            }
            // Find the center offset
            if (center)
            {
                centerOffset = new Vector3((count-1)*offset.x/2*-1, (count-1)*offset.y/2*-1, (count-1)*offset.z/2*-1);
            }
            // Set the prefab to spawn to a random one
            if(randomPrefab)
            {
                if (i == 0 && startPrefabs.Count > 0)
                {
                    prefabToSpawn = startPrefabs[Random.Range(0, startPrefabs.Count)];
                }
                else if (i == count-1 && endPrefabs.Count > 0)
                {
                    prefabToSpawn = endPrefabs[Random.Range(0, endPrefabs.Count)];
                }
                else if (midPrefabs.Count > 0)
                {
                    prefabToSpawn = midPrefabs[Random.Range(0, midPrefabs.Count)];
                }
                
            }
            // Generate the gameobject (with a link to prefab if specified)
            if (preservePrefab && PrefabUtility.IsPartOfPrefabAsset(prefabToSpawn))
            {
                instanciatedPrefab = PrefabUtility.InstantiatePrefab(prefabToSpawn) as GameObject;
                instanciatedPrefab.transform.position = transform.position;
                instanciatedPrefab.transform.rotation = rotation;
            }
            else
            {
                instanciatedPrefab = Instantiate(prefabToSpawn, transform.position, rotation);
            }
            // Place the prefab in the destined postion
            instanciatedPrefab.transform.position = parent.transform.position + objectOffset*i + centerOffset;
            // Set the parent
            instanciatedPrefab.transform.parent = parent.transform;
            // Naming
            instanciatedPrefab.name = goName + "_" + i.ToString();
            // Random sprites
            if(randomSprite && instanciatedPrefab.GetComponent<SpriteRenderer>())
            {
                if (i == 0 && startSprites.Count > 0)
                {
                    instanciatedPrefab.GetComponent<SpriteRenderer>().sprite = startSprites[Random.Range(0, startSprites.Count)];
                }
                else if (i == count-1 && endSprites.Count > 0)
                {
                    instanciatedPrefab.GetComponent<SpriteRenderer>().sprite = endSprites[Random.Range(0, endSprites.Count)];
                }
                else
                {
                    instanciatedPrefab.GetComponent<SpriteRenderer>().sprite = midSprites[Random.Range(0, midSprites.Count)];
                }
                
            }
        }
    }
#endif

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
        if (generateOnStart && prefab != null)
        {
            Generate();
        }
        #endif
    }
}
