using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using TMPro;

public class NotificationHandler : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI intentTxt;
    [SerializeField] public int waitTimer;

    private void Awake()
    {
        BuildDefaultNotationChannel();
        BuildRepeatingNotationChannel();
    }

    private void Start()
    {
        AndroidNotificationCenter.OnNotificationReceived += ReceivedData;
    }

    private void OnDisable()
    {
        AndroidNotificationCenter.OnNotificationReceived -= ReceivedData;
    }



    private void BuildDefaultNotationChannel()
    {
        //How we would refer the channel in the code;
        string channel_id = "default";

        //How the channel would appear in the settings;
        string title = "Default Channel";

        // Importance of the channel;   
        Importance importance = Importance.Default;

        //Description of the channel;
        string description = "Default Channel for this game";


        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);

    }

    public void BuildRepeatingNotationChannel()
    {
        //How we would refer the channel in the code;
        string channel_id = "repeat";

        //How the channel would appear in the settings;
        string title = "Repeating Channel";

        // Importance of the channel;   
        Importance importance = Importance.Default;

        //Description of the channel;
        string description = "Repeating Channel for this game";


        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);
    }

    public void SendSimpleNotif()
    {
        Debug.Log("Simple Notif Process");

        //Title of the notification
        string notif_title = "Simple Notif";

        //Message of the notification
        string notif_message = "Test: Simple Notifaction";

        //When will the notification be sent
        System.DateTime fireTime = System.DateTime.Now.AddSeconds(5);

        AndroidNotification notif =
            new AndroidNotification(notif_title, notif_message, fireTime);

        //notif.LargeIcon = "small";
        //notif.SmallIcon = "large";

        AndroidNotificationCenter.SendNotification(notif, "default");

    }


    public void SendRepeatNotif()
    {
        Debug.Log("Repeat Notif Process");

        //Title of the notification
        string notif_title = "Repeat Notif";

        //Message of the notification
        string notif_message = "Test: Repeat notifaction";

        //Timing
        System.TimeSpan interval = new System.TimeSpan(0, 0, 10);

        //When will the notification be sent
        System.DateTime fireTime = System.DateTime.Now.AddSeconds(5);

        AndroidNotification notif =
            new AndroidNotification(notif_title, notif_message, fireTime, interval);

        //Enable them after Settling for one pic
        //notif.LargeIcon = "small";
        //notif.SmallIcon = "large";

        AndroidNotificationCenter.SendNotification(notif, "repeat");

    }

    public void SendDataNotif()
    {
        //Title of the notification
        string notif_title = "Data Notif";

        //Message of the notification
        string notif_message = "Test Data notifaction";

        //When will the notification be sent
        System.DateTime fireTime = System.DateTime.Now.AddSeconds(5);

        AndroidNotification notif =
            new AndroidNotification(notif_title, notif_message, fireTime);

        //Enable them after Settling for one pic
        //notif.LargeIcon = "small";
        //notif.SmallIcon = "large";

        notif.IntentData = "omewa no shinderu";
        AndroidNotificationCenter.SendNotification(notif, "default");

    }

    private void ReceivedData(AndroidNotificationIntentData data)
    {
        Debug.Log("Data Received: " + data);
        Debug.Log($"Intent Data: {data.Notification.IntentData}");

        if (data.Notification.IntentData != null)
        {
            //intentTxt.text = data.Notification.IntentData;
        }
    }
}
