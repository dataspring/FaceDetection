using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MemomMvc52.Models
{
    public class ScoreItem
    {
        public int ID { get; set; }
        public int Ranking { get; set; }
        public string DeptName { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public string UserCode { get; set; }
    }

    public class ScoreCard
    {
        public int Count { get; set; }
        public User Player { get; set; }
        public List<ScoreItem> LeaderboardScores { get; set; }
        public ScoreItem PlayerScore { get; set; }
    }


    public class PlayerBadges
    {
        public List<Badge> Badges { get; set; }
    }

    public class PlayerMetas
    {
        public List<BadgeMeta> BadgeMetas { get; set; }
    }

    public class Badge
    {
        // also acts as title string for badge display
        public string LevelName { get; set; }
        public string BadgeName { get; set; }
        public int BadgeStatus { get; set; }

        public string RegexBadgeName()
        {
           return Regex.Replace(this.BadgeName, @"[^A-Za-z0-9]+", "");
        }

    }



    public class BadgeMeta : Badge
    {
        //master data related stuff
        public string SubTitle { get; set; }
        public string BadgeType { get; set; }
        public string ButtonText { get; set; }

        public string GetPictureNamefromBadgeName()
        {
            if (this.BadgeStatus == 0)
                return BadgeType.ToLower().ToString() + "-" + "Blank";
            else
                return BadgeType.ToLower().ToString() + "-" + RegexBadgeName();
        }

    }



    public class LaunchScoreCard
    {
        public LaunchScoreCard()
        {
            this.DisplayFbLike = false;
            this.PlayerScores = new ScoreCard();
            this.PlayerBadges = new PlayerBadges();
            this.BadgeMetaData = new PlayerMetas();
            this.BadgeDisplayItems = new List<BadgeDisplay>();
            this.StickerDisplayItems = new List<BadgeDisplay>();

        }

        public LaunchScoreCard(bool DisplayFbLike)
        {
            this.DisplayFbLike = DisplayFbLike;
            this.PlayerScores = new ScoreCard();
            this.PlayerBadges = new PlayerBadges();
            this.BadgeMetaData = new PlayerMetas();
            this.BadgeDisplayItems = new List<BadgeDisplay>();
            this.StickerDisplayItems = new List<BadgeDisplay>();
        }

        public bool DisplayFbLike { get; set; }
        public ScoreCard PlayerScores { get; set; }
        public PlayerBadges PlayerBadges { get; set; }
        public PlayerMetas BadgeMetaData { get; set; }
        public List<BadgeDisplay> BadgeDisplayItems { get; set; }
        public List<BadgeDisplay> StickerDisplayItems { get; set; }
        public int TotalBadges { get; set; }
        public int TotalStickers { get; set; }

        private int GetBadgeDisplayOrder(string LevelName)
        {
            switch (LevelName)
            {
                case "Scenario 1": 
                        return 1;
                case "Scenario 2":
                        return 2;
                case "Scenario 3":
                        return 3; 
                default:
                    return 999;
            }
        }

        public void PrepareBagesForDisplay()
        {

            var levelSummary = PlayerBadges.Badges.GroupBy(l => l.LevelName)
                                      .Select(lg =>
                                            new
                                            {
                                                LevelName = lg.Key,
                                                LevelCount = lg.Count(),
                                                LevelBadgesGotten = lg.Sum(w => w.BadgeStatus)
                                            });

            //check for showing centum badge if all badges were claimed
            TotalBadges = PlayerBadges.Badges.Where(w => w.LevelName != "STICKER").Sum(x => x.BadgeStatus);

            //prepare all badges for display
            foreach (var level in levelSummary.Where(w => w.LevelName != "STICKER"))
            {
                BadgeDisplay badgeLevelDisplay = new BadgeDisplay();
                badgeLevelDisplay.LevelHeading = PlayerLevelDisplay.Name[Array.IndexOf(PlayerLevelDb.Name, level.LevelName)];
                badgeLevelDisplay.BadgeCollected = level.LevelBadgesGotten;
                badgeLevelDisplay.TotalLevelBadges = level.LevelCount;
                badgeLevelDisplay.BadgeDisplayOrder = GetBadgeDisplayOrder(level.LevelName);

                var levelZombie = PlayerBadges.Badges.Where(l => l.LevelName == level.LevelName);
                foreach (Badge badge in levelZombie)
                {
                    BadgeMeta badgeMetaItem = new BadgeMeta();

                    if (BadgeMetaData.BadgeMetas.Where(c => c.BadgeType == "BADGE").Where(c => c.LevelName == badge.LevelName).Where(c => c.BadgeName.Trim() == badge.BadgeName.Trim()).Count() > 0)
                        badgeMetaItem.SubTitle = BadgeMetaData.BadgeMetas.Where(c => c.BadgeType == "BADGE").Where(c => c.LevelName == badge.LevelName).Where(c => c.BadgeName.Trim() == badge.BadgeName.Trim()).FirstOrDefault().SubTitle;
                    badgeMetaItem.ButtonText = badge.BadgeStatus == 1 ? "Activate" : "UnAvailable";
                    badgeMetaItem.BadgeName = badge.BadgeName;
                    badgeMetaItem.LevelName = badge.LevelName;
                    badgeMetaItem.BadgeStatus = badge.BadgeStatus;
                    badgeMetaItem.BadgeType = "BADGE";

                    badgeLevelDisplay.BadgeItems.Add(badgeMetaItem);
                }
                this.BadgeDisplayItems.Add(badgeLevelDisplay);
            }


            //prepare all stickers for display
            List<Badge> stickers = PlayerBadges.Badges.Where(w => w.LevelName == "STICKER").Where(c => c.BadgeStatus == 1).ToList();
            TotalStickers = stickers.Count;

            if (TotalStickers > 0)
            {
                int quadcount = 0;
                if (stickers.Count < 4)
                    quadcount = 1;
                else
                    quadcount = (TotalStickers / 4) + (TotalStickers % 4 > 0 ? 1 : 0);

                for (int i = 0; i < quadcount; i++)
                {
                    BadgeDisplay badgeLevelDisplay = new BadgeDisplay();
                    badgeLevelDisplay.Badges = stickers.Skip(i * 4).Take(4).ToList();
                    this.StickerDisplayItems.Add(badgeLevelDisplay);
                }

                //fill in the subtitle as well for stickers - so need to create the badgeItem rather/// done on 2 Oct 2013 - as addon and not optimized!!!
                foreach (BadgeDisplay bdd in this.StickerDisplayItems)
                {
                    foreach (Badge badge in bdd.Badges)
                    {
                        BadgeMeta badgeMetaItem = new BadgeMeta();

                        if (BadgeMetaData.BadgeMetas.Where(c => c.BadgeType == "STICKER").Where(c => c.LevelName == badge.LevelName).Where(c => c.BadgeName.Trim() == badge.BadgeName.Trim()).Count() > 0)
                            badgeMetaItem.SubTitle = BadgeMetaData.BadgeMetas.Where(c => c.BadgeType == "STICKER").Where(c => c.LevelName == badge.LevelName).Where(c => c.BadgeName.Trim() == badge.BadgeName.Trim()).First().SubTitle;
                        badgeMetaItem.BadgeName = badge.BadgeName;
                        badgeMetaItem.LevelName = badge.LevelName;
                        badgeMetaItem.BadgeStatus = badge.BadgeStatus;
                        badgeMetaItem.BadgeType = "STICKER";

                        bdd.BadgeItems.Add(badgeMetaItem);
   
                    }
                }

            }

        }
    }


        public class BadgeDisplay
        {
            public BadgeDisplay()
            {
                this.BadgeItems = new List<BadgeMeta>();
            }

            public string LevelName { get; set; }
            public string LevelHeading { get; set; }
            public int BadgeCollected { get; set; }
            public int TotalLevelBadges { get; set; }
            public int BadgeDisplayOrder { get; set; }

            public List<BadgeMeta> BadgeItems { get; set; }
            public List<Badge> Badges { get; set; }
        }



        public enum LevelNo
        {
            Scenario1 = 0,
            Scenario2 = 1,
            Scenario3 = 2,
            Scenario4 = 3
        }


        public static class PlayerLevelDb
        {
            static string[] dbName = new string[] { "JHSScenario1", "JHSScenario2", "JHSScenario3", "JHSScenario4" };

            public static string[] Name
            {
                get { return dbName; }
            }

        }

        public static class PlayerLevelDisplay
        {
            static string[] displayName = new string[] { "Scenario 1", "Scenario 2", "Scenario 3", "Scenario 4" };

            public static string[] Name
            {
                get { return displayName; }
            }
        }

    

}