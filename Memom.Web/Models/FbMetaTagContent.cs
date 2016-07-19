using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemomMvc52.Models
{
    public class FbMetaTagContent
    {
        public string Achievement { get; set; }
        public string StickerName { get; set; }
        public string StickerUrlTag { get; set; }
        public string Description { get; set; }
        public string Tagline { get; set; }
    }

    public static class FbMetaTags
    {
        private static readonly List<FbMetaTagContent> _tags;

        static FbMetaTags()
        {
            _tags = new List<FbMetaTagContent>();

            _tags.Add(new FbMetaTagContent { StickerUrlTag = "SpaceMaintainer", StickerName = "Space Maintainer", Achievement = "Killed 20 zombies", Tagline = "Tar-tar, zombies. Come back with even more attack", Description = "I just killed 20 zombies in Zombie Tooth!  Let's see if you can top that!" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "BigCruncher", StickerName = "Big Cruncher", Achievement = "Killed 40 zombies", Tagline = "Great job! Next time, use even more bite", Description = "I'm crunching zombies big-time in Zombie Tooth! Challenge me and see who has more bite!" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "CrowningGlory", StickerName = "Crowning Glory", Achievement = "Killed 60 zombies", Tagline = "Zombies better watch out when you come blazing in!", Description = "Rocking my new crown as I blaze through Zombie Tooth! Can you overthrow me?" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "ApexPredator", StickerName = "Apex Predator", Achievement = "Killed 100 zombies", Tagline = "Top of the food chain! You keep getting stronger.", Description = "Yowza! I'm at the top of the food chain in Zombie Tooth. Just try to take me down!" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "FlashCleaner", StickerName = "Flash Cleaner", Achievement = "Speed Run", Tagline = "Wow. You wiped them out in no time.", Description = "I just cleaned out zombies in one fell swoop. How fast can you do it in Zombie Tooth?" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "KnightinShiningEnamel", StickerName = "Knight in Shining Enamel", Achievement = "Player saves all citizens ", Tagline = "Your quick thinking immunised all civilians! Your Vital-Health ", Description = "I just saved an entire town from Zombies! Knock him off the Zombie Tooth Leaderboard!" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "AntisepticPatrol", StickerName = "Anti-septic Patrol ", Achievement = "Player cures all citizens ", Tagline = "You cured all civilians in the level! Your Vital-Health increases.", Description = "I just cured an entire town of Zombicitis! Join me in Zombie Tooth!" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "Necromancer", StickerName = "Necromancer", Achievement = "All citizens turn to zombies", Tagline = "Dental traitor! You’re part of Lord Edentulous’ vile plans", Description = "Dental traitor! You’re part of Lord Edentulous’ vile plans" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "TeethingProblems", StickerName = "Teething Problems", Achievement = "gets killed", Tagline = "You'll grow bigger teeth soon. Come back then", Description = "You'll grow bigger teeth soon. Come back then" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "ForeverAlone", StickerName = "Forever Alone", Achievement = "Companion dies", Tagline = "You gotta watch your step, they’re gunning for you now!", Description = "You gotta watch your step, they’re gunning for you now!" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "ForcedExtraction", StickerName = "Forced Extraction", Achievement = "Killed civilians by accident", Tagline = "Oh no! Are you turning rogue?", Description = "Oh no! Are you turning rogue?" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "BeatenPulp", StickerName = "Beaten Pulp", Achievement = "Beat High Score", Tagline = "Zombies for breakfast, anyone?", Description = "Zombies for breakfast, anyone?" });
            _tags.Add(new FbMetaTagContent { StickerUrlTag = "RinseAndRepeat", StickerName = "Rinse And Repeat", Achievement = "Album is played for 2 consecutive days", Tagline = "A game a day keeps the zombies away", Description = "A game a day keeps the zombies away" });

        }

        public static List<FbMetaTagContent> Instance { get { return _tags; } }

    }

}