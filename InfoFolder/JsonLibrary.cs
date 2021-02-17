using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonLibrary 
{
    private const string FileName = "JsonLibrary.json";
    private string _fullFileName;
    public JsonLibrary(string savePath)
    {
        _fullFileName = Path.Combine(savePath, FileName);
    }
	public void Write(Info data)
	{
		InfoSerializer serializer = new InfoSerializer();
		using (StreamWriter sw = new StreamWriter(_fullFileName))
		{
			serializer.SerializeToFile(sw, data);
		}
	}
	public Info Read()
	{
		if (!File.Exists(_fullFileName))
		{
			NewPlayer();
		}
		InfoSerializer serializer = new InfoSerializer();
		Info data = null;
		using (StreamReader sr = new StreamReader(_fullFileName))
		{
			data = serializer.DerializeFromFile(sr);
		}
		return data;
	}
	public void NewPlayer()
	{
		List<SkinsInfo> skins = new List<SkinsInfo>();
		skins.Add(new SkinsInfo(0, "MoonPlanetIdleanim", true, 0, 0));
		skins.Add(new SkinsInfo(1, "MarsPlanet", false, 1000, 10));
		skins.Add(new SkinsInfo(2, "FrostPlanet", false, 2000, 15));
		skins.Add(new SkinsInfo(3, "WaterIslandsPlanet", false, 3000, 20));
		skins.Add(new SkinsInfo(4, "PurplePlanet", false, 4000, 20));
		skins.Add(new SkinsInfo(5, "VioletClowdsPlanet", false, 5000, 20));
		skins.Add(new SkinsInfo(6, "StormPlanet", false, 6000, 25));
		skins.Add(new SkinsInfo(7, "IcePlanet", false, 7000, 30));
		skins.Add(new SkinsInfo(8, "OrangeGradientPlanetIdle", false, 8000, 35));
		skins.Add(new SkinsInfo(9, "RedIcePLanet", false, 10000, 35));
		skins.Add(new SkinsInfo(10, "GreenPlanet", false, 12000, 40));
		skins.Add(new SkinsInfo(11, "YellowBluePlanet", false, 13000, 40));
		skins.Add(new SkinsInfo(12, "CreamVioletPlanet", false, 15000, 40));
		skins.Add(new SkinsInfo(13, "PurplePlanetWithIslands", false, 18000, 45));
		skins.Add(new SkinsInfo(14, "SandPlanet", false, 20000, 50));
		skins.Add(new SkinsInfo(15, "PinkYellowGradientPlanet", false, 20000, 50));
		skins.Add(new SkinsInfo(16, "PurplePlanetWithMoon", false, 20000, 50));
		skins.Add(new SkinsInfo(17, "GasGiant", false, 20000, 50));
		skins.Add(new SkinsInfo(18, "DarkPlanetIdle", false, 25000, 60));
		skins.Add(new SkinsInfo(19, "RedLinePlanet", false, 25000, 60));
		skins.Add(new SkinsInfo(20, "DarkWaterPlanetIdle", false, 25000, 60));
		skins.Add(new SkinsInfo(21, "DarkBluePlanet", false, 30000, 70));
		skins.Add(new SkinsInfo(22, "EarthPlanet", false, 50000, 100));
		skins.Add(new SkinsInfo(23, "Sun", false, 75000, 150));
		skins.Add(new SkinsInfo(24, "WhiteDwarfStar", false, 100000, 200));

		List<QuestInfo> quests = new List<QuestInfo>();
		quests.Add(new QuestInfo(0, "TotalEatPlanets", 0, 0, new QuestStage(5, 20, 50, 100, 500), "Eat ", " planets.", 
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false,false,false,false,false)));
		quests.Add(new QuestInfo(1, "BestEatPlanets", 0, 0, new QuestStage(2, 4, 6, 8, 9), "Eat ", " planets per match.", 
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(2, "TotalScore", 0, 0, new QuestStage(10000, 20000, 50000, 100000, 200000), "Earn ", " total score .", 
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(3, "BestScore", 0, 0, new QuestStage(500, 1000, 5000, 10000, 20000), "Earn ", " best score.", 
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(4, "TotalEarnCoins", 0, 0, new QuestStage(1000, 5000, 20000, 50000, 100000), "Earn ", " coins.(primary)", 
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(5, "BestEarnCoins", 0, 0, new QuestStage(100, 200, 700, 1500, 3000), "Earn ", " coins per match.", 
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(6, "CollectSkins", 1, 0, new QuestStage(3, 5, 10, 15, 20), "Collect ", " planet skins.", new QuestStage(1, 2, 3, 4, 5), 
			new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(7, "ReachRank", 1, 0, new QuestStage(3, 5, 10, 15, 20), "Reach ", " rank.",
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(8, "WatchAdds", 0, 0, new QuestStage(1, 5, 10, 20, 50), "Watch ", " adds.",
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(9, "PlayMatches", 0, 0, new QuestStage(1, 5, 20, 50, 100), "Play ", " matches.", 
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		quests.Add(new QuestInfo(10, "StarPlayer", 0, 0, new QuestStage(1, 5, 10, 20, 50), "Be ''Star Player'' ", " times.",
			new QuestStage(1, 2, 3, 4, 5), new QuestStageClaimed(false, false, false, false, false)));
		

		Info data = new Info(skins,quests,false, false,false,PlayerPrefs.GetString("Nickname"), false,false);
		Write(data);
	}
	public bool GetAdsStatus()
	{
		Info data = Read();
		if(data.AdsOff==null)
		{
			data.AdsOff = false;
			Write(data);
		}
		return data.AdsOff;
	}
	public void SetAdsStatus()
	{
		Info data = Read();
		data.AdsOff = true;
		Write(data);
	}
	public void DeleteProgress()
	{
		Info data = Read();
		data.NewPlayer = false;
		Write(data);
	}
	public bool NewOrOldPlayer()
	{
		Info data = Read();
		return data.NewPlayer;
	}
	public void SetOldPlayer()
	{
		Info data = Read();
		data.NewPlayer = true;
		Write(data);
	}
	public void ActivateShieldSpell()
	{
		Info data = Read();
		data.ShieldUpSpell = true;
		Write(data);
	}
	public bool IsShieldUpUnlock()
	{
		Info data = Read();
		return data.ShieldUpSpell;
	}
	public bool IsSizeUpUnlock()
	{
		Info data = Read();
		return data.SizeUpSpell;
	}
	public void ActivateSizeSpell()
	{
		Info data = Read();
		data.SizeUpSpell = true;
		Write(data);
	}
	public void AddProgress(int progress, int questId, bool plusOrNew)
	{
		Info data = Read();
		if (plusOrNew)
		{
			data.Quests[questId].CurrentProgress += progress;
		}
		else
		{
			if (data.Quests[questId].CurrentProgress < progress)
			{
				data.Quests[questId].CurrentProgress = progress;
			}
		}
		Write(data);
		if (IsReachedThisGoal(questId))
		{
			GameProgress.QuestDone = true;
		}
		
	}
	public int GetProgress(int questId)
	{
		Info data = Read();
		return data.Quests[questId].CurrentProgress;
	}
	public string GetDiscription(int questId)
	{
		Info data = Read();
		int currentStage = data.Quests[questId].CurrentStage;
		int stageGoal = data.Quests[questId].QuestGoal.GetThisStageGoal(currentStage);
		//int progress = data.Quests[questId].CurrentProgress;
		//int stage = data.Quests[questId].QuestGoal.GetLevel(progress);
		//int stageGoal = data.Quests[questId].QuestGoal.GetThisStageGoal(stage);
		string discriptionBefore = data.Quests[questId].DiscriptionBefore;
		string discriptionAfter = data.Quests[questId].DiscriptionAfter;
		return discriptionBefore + stageGoal + discriptionAfter;
	}
	public int GetGemsCount(int questId)
	{
		Info data = Read();
		int currentStage = data.Quests[questId].CurrentStage;
		int stageGemsReward = data.Quests[questId].GemsReward.GetThisStageGoal(currentStage);
		return stageGemsReward;
	}
	public void StageUp(int questId)
	{
		Info data = Read();
		data.Quests[questId].CurrentStage += 1;
		Write(data);
	}
	public void NewShieldAnim()
	{
		Info data = Read();
		data.NewShield = true;
		Write(data);
	}
	public bool isNewShieldAnim()
	{
		Info data = Read();
		return data.NewShield;
	}
	public bool IsReachedThisGoal(int questId)
	{
		Info data = Read();
		int currentStage = data.Quests[questId].CurrentStage;
		int currentProgress = data.Quests[questId].CurrentProgress;
		int currentStageQuestGoal = data.Quests[questId].QuestGoal.GetThisStageGoal(currentStage);
		return (currentProgress >= currentStageQuestGoal);
		//if (currentProgress >= currentStageQuestGoal)
		//{
		//	return true;
		//}
		//else return false;
	}
	public bool MaxStage(int questId)
	{
		Info data = Read();
		int currentStage = data.Quests[questId].CurrentStage;
		if (currentStage == 5)
		{
			return true;
		}
		else
			return false;
		
	}
	public bool IsClaimed(int questId)
	{
		if(questId == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public string NameOfSkin(int skinID)
	{
		Info data = Read();
		return data.Skins[skinID].Name;
	}
	public bool HaveSkin(int skinID)
	{
		Info data = Read();
		return data.Skins[skinID].Have;
	}
	public void HaveOverride(int skinID)
	{
		Info data = Read();
		data.Skins[skinID].Have = true;
		Write(data);
	}
	public int SkinCost(int skinID, string bill)
	{
		Info data = Read();
		if (bill == "Coins")
		{
			return data.Skins[skinID].CoinsCost;
		}
		else
		{
			return data.Skins[skinID].GemsCost;
		}


	}
}
