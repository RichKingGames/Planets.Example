using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public GameObject Planet;
    public GameController Controller;
    public GameObject PurpleFrame;
    public GameObject GoldFrame;
    public Image RankImage;
    public Text Nickname;
    public Text PlayerNickname;

    void Start()
    {
        if(PlayerNickname!=null)
        {
            PlayerNickname.text = PlayerPrefs.GetString("Nickname");
            int rank = PlayerPrefs.GetInt("Rank");
            RankImage.sprite = Resources.Load<Sprite>("Ranks/Rank" + rank.ToString());
        }
        
        
    }
    void Update()
    {
        transform.position = Planet.transform.position;
        transform.localScale = Planet.transform.localScale;
        if(!Planet.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        //else
        //{
        //    this.gameObject.SetActive(true);
        //}
    }
    public void SetActiveGoldFrame()
    {
        GoldFrame.SetActive(true);
        PurpleFrame.SetActive(false);
    }
    public void SetActivePurpleFrame()
    {
        GoldFrame.SetActive(false);
        PurpleFrame.SetActive(true);
    }
    
}
