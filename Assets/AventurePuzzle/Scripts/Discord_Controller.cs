using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Discord;

public class Discord_Controller : MonoBehaviour
{
    
    

    public long applicationID;
    public string details = "Explore le marché";
    [Space]
    public string largeImage = "game_logo";
    public string largeText = "The Last Arcana";

    private long time;
    
    private static bool instanceExists;
    public Discord.Discord discord;

    private void Awake()
    {
        if (instanceExists)
        {
            Destroy(gameObject);
        }
        else
        {
            instanceExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        discord = new Discord.Discord(applicationID, (UInt64)CreateFlags.NoRequireDiscord);
        time = DateTimeOffset.Now.ToUnixTimeSeconds();

        UpdateStatus();
        
        //Make it not destroy on load
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch (Exception e)
        {
            Destroy(gameObject);
        }
    }

    void UpdateStatus()
    {
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Activity
            {
                Details = details,
                Timestamps =
                {
                    Start = time
                },
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res == Result.Ok)
                {
                    Debug.Log("Discord Rich Presence Updated");
                }
                else
                {
                    Debug.LogError("Error updating Discord Rich Presence");
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Destroy(gameObject);
        }
    }
}
