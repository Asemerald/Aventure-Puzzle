using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }
    
    [SerializeField] private List<AssetReferenceGameObject> levelReferences;
    private Dictionary<int, GameObject> loadedLevels = new Dictionary<int, GameObject>();
    public int currentRoom;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        currentRoom = 0;
        LoadLevelsAround(currentRoom);
    }

    public void ManageLevels()
    {
        LoadLevelsAround(currentRoom);

        foreach (var key in new List<int>(loadedLevels.Keys))
        {
            if (key < currentRoom - 2 || key > currentRoom + 2)
            {
                UnloadLevel(key);
            }
            else
            {
                loadedLevels[key].SetActive(key >= currentRoom - 1 && key <= currentRoom + 1);
            }
        }
    }

    private void LoadLevelsAround(int index)
    {
        for (int i = -2; i <= 2; i++)
        {
            int targetIndex = index + i;
            if (targetIndex >= 0 && targetIndex < levelReferences.Count)
            {
                if (!loadedLevels.ContainsKey(targetIndex))
                {
                    LoadLevel(targetIndex);
                }
            }
        }
    }

    private void LoadLevel(int index)
    {
        if (index < 0 || index >= levelReferences.Count) return;

        levelReferences[index].LoadAssetAsync<GameObject>().Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject levelInstance = Instantiate(handle.Result);
                levelInstance.SetActive(index >= currentRoom - 1 && index <= currentRoom + 1);
                loadedLevels[index] = levelInstance;

                LevelRoom levelRoom = levelInstance.GetComponent<LevelRoom>();
                if (levelRoom != null)
                {
                    levelRoom.roomNum = index;
                }
            }
        };
    }

    private void UnloadLevel(int index)
    {
        if (loadedLevels.ContainsKey(index))
        {
            Destroy(loadedLevels[index]);
            loadedLevels.Remove(index);
            levelReferences[index].ReleaseAsset();
        }
    }
}
