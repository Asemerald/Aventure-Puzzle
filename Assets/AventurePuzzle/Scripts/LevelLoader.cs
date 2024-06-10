using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [SerializeField] List<LevelRoom> levels;
    public int startingRoom;
    public int currentRoom;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        currentRoom = startingRoom;

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].roomNum = i;
            levels[i].gameObject.SetActive(false);
        }

        for (int i = -1; i < 2; i++)
        {
            if (currentRoom + i >= 0 && currentRoom + i < levels.Count)
            {
                levels[currentRoom + i].gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        ManageLevels();
    }

    public void ManageLevels()
    {

        for (int i = -1; i < 2; i++)
        {
            if ((currentRoom + i) >= 0 && (currentRoom + i) < levels.Count)
            {
                if (!levels[i].gameObject.activeSelf)
                    levels[currentRoom + i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < levels.Count; i++)
        {
            if (i < (currentRoom - 1) || i > (currentRoom + 1))
                if(levels[i].gameObject.activeSelf)
                    levels[i].gameObject.SetActive(false);
        }

    }

    public void ActiveAllLevel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].gameObject.SetActive(true);
        }

    }
}
