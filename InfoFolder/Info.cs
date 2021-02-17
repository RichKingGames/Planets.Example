using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info
{
    public bool ShieldUpSpell;
    public bool SizeUpSpell;
    public bool NewPlayer = false;
    public string Nickname;
    public bool NewShield;
    public bool AdsOff;
    public List<SkinsInfo> Skins { get; set; }
    public List<QuestInfo> Quests { get; set; }
    public Info(List<SkinsInfo> skins, List<QuestInfo> quests,
        bool shieldSpell, bool sizespell,bool newPlayer,
        string nickname, bool newShield, bool adsoff)
    {
        Skins = skins;
        Quests = quests;
        ShieldUpSpell = shieldSpell;
        SizeUpSpell = sizespell;
        NewPlayer = newPlayer;
        Nickname = nickname;
        NewShield = newShield;
        AdsOff = adsoff;
    }
}
