using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADManager : MonoBehaviour
{
    private string APP_ID = "ca-app-pub-8911531474291395~5955887077";

    //real
    private string video_ID = "ca-app-pub-8911531474291395/7808024791";
    //testing
    //private string video_ID = "ca-app-pub-3940256099942544/5224354917";

    private RewardBasedVideoAd rewardBasedVideo;

    void Start()
    {
        MobileAds.Initialize(APP_ID);

        RequestVideoAd();

        HandleRewardBasedVideoEvents(true);
    }

    void RequestVideoAd()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        //FOR REAL APP
        AdRequest adRequest = new AdRequest.Builder().Build();
        //FOR TESTING
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        rewardBasedVideo.LoadAd(adRequest, video_ID);
    }
    public void Display_Reward_Video()
    {
        if(rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }

    //Handle Events
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestVideoAd();
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        RequestVideoAd();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        PlayerPrefsSafe.SetFloat("Coins", PlayerPrefsSafe.GetFloat("Coins") + 1000);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    void HandleRewardBasedVideoEvents(bool subscribe)
    {
        if(subscribe)
        {
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        }
        else
        {
            rewardBasedVideo.OnAdLoaded -= HandleRewardBasedVideoLoaded;
            rewardBasedVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideo.OnAdOpening -= HandleRewardBasedVideoOpened;
            rewardBasedVideo.OnAdStarted -= HandleRewardBasedVideoStarted;
            rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
            rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
            rewardBasedVideo.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
        }
        
    }
   
    void OnEnable()
    {
        HandleRewardBasedVideoEvents(true);
    }

    void OnDisable()
    {
        HandleRewardBasedVideoEvents(false);
    }
}
    

