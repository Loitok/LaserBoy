using System.Collections;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManagerGame : MonoBehaviour
{
    private string APP_ID = "ca-app-pub-8911531474291395~5955887077";
    private string video_ID = "ca-app-pub-8911531474291395/5662520834";

    public GameObject spawner;
    public GameObject gameOverUI;
    private GameObject currentPlayer;
    private GameObject player;
    public GameObject controller;
    public GameObject mainCam;
    public GameObject score;
    public GameObject pauseButton;

    public Transform playerPosition;
    private GameObject GM;

    private RewardBasedVideoAd rewardBasedVideo;

    private GameObject capsule;
    public GameObject capsulePref;

    

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");

        MobileAds.Initialize(APP_ID);

        RequestVideoAD();

        HandleRewardBasedVideoEvents(true);
    }

    void RequestVideoAD()
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
        if (rewardBasedVideo.IsLoaded())
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
        RequestVideoAD();
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
         RequestVideoAD();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        spawner.SetActive(true);
        gameOverUI.SetActive(false);
        score.SetActive(true);
        pauseButton.SetActive(true);
        currentPlayer = GM.GetComponent<GameMaster>().Player();
        player = (GameObject)Instantiate(currentPlayer, playerPosition.position, Quaternion.identity);
        mainCam.GetComponent<CameraController>().Start();
        controller.GetComponent<Controlling>().Start(); 
        capsule = (GameObject)Instantiate(capsulePref, capsulePref.transform.position, Quaternion.identity);
        StartCoroutine(Wait());
    }
    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(capsule);
        gameOverUI.GetComponent<GameOver>().Destroy();
    }

    void HandleRewardBasedVideoEvents(bool subscribe)
    {
        if (subscribe)
        {
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // Called when the ad starts to play.
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            // Called when the user should be rewarded for watching a video.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            // Called when the ad click caused the user to leave the application.
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
