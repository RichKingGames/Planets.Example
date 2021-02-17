using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{
    public Frame[] Frames;
    public Frame PlayerFrame;
    public GameController Controller;
    

    public void AttachFrame()
    {
        PlayerFrame.Planet = Controller.Player;
        for (int i = 0; i < Controller.Bots.Count; i++)
        {
            Frames[i].Planet = Controller.Bots[i];
        }
    }
    public void FrameInitizialize(string[] nicknames, int[] ranks)
    {
        for (int i = 0; i < Controller.Bots.Count; i++)
        {
            Frames[i].Nickname.text = nicknames[i];
            Frames[i].RankImage.sprite = Resources.Load<Sprite>("Ranks/Rank" + ranks[i].ToString());
        }
    }
    public void SetStarPlayer(string nickname)
    {
        PlayerFrame.SetActivePurpleFrame();
        for(int i = 0; i < Frames.Length; i++)
        {
            Frames[i].SetActivePurpleFrame();
        }
        if(PlayerFrame.PlayerNickname.text == nickname)
        {
            Controller.gameManager.GetComponent<PlanetSoundsController>().StarPlayerInMatch();
            PlayerFrame.SetActiveGoldFrame();
            FloatingTextController.CreatStarPlayerFloatingImage();
            Controller.StarPlayer = true;
            return;
        }
        for(int i = 0; i < Frames.Length; i++)
        {
            if(Frames[i].Nickname.text == nickname)
            {
                Frames[i].SetActiveGoldFrame();
                Controller.StarPlayer = false;
            }
        }
    }
}
