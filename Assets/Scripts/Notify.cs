using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleAndroidNotifications;
using System;

public class Notify : MonoBehaviour
{
    private string title = "LaserBoy";
    private string content = "Come on and beat the record!";

    void OnApplicationPause(bool isPause)
    {
#if UNITY_ANDROID
        NotificationManager.CancelAll();
        if(isPause)
        {
            DateTime timeToNotify = DateTime.Now.AddMinutes(90);
            TimeSpan time = timeToNotify - DateTime.Now;
            NotificationManager.SendWithAppIcon(time,title,content,Color.blue, NotificationIcon.Star);
        }

#endif
    }
}
