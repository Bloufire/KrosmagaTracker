using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Enums
{
    public enum GameType
    {
	    [Description("Random Ranked")]
		RANDOM_RANKED = 1,
	    [Description("Random Unranked")]
		RANDOM_UNRANKED = 2,
	    [Description("Versus IA")]
		VERSUS_IA = 3,
	    [Description("Draft")]
		DRAFT = 4,
	    [Description("Tutorial 1")]
		TUTORIAL_STEP_1 = 5,
	    [Description("Tutorial 2")]
		TUTORIAL_STEP_2 = 6,
	    [Description("Tutorial 3")]
		TUTORIAL_STEP_3 = 7,
	    [Description("Friendly")]
		FRIENDLY = 8,
	    [Description("Dungeon")]
		DUNGEON_ROOM = 9,
	    [Description("Unlock Eca")]
		UNLOCK_ECAFLIP = 10,
	    [Description("Unlock Eni")]
		UNLOCK_ENIRIPSA = 11,
	    [Description("Unlock Sacri")]
		UNLOCK_SACRIER = 12,
	    [Description("Unlock Xel")]
		UNLOCK_XELOR = 13,
	    [Description("Unlock Sram")]
		UNLOCK_SRAM = 14,
	    [Description("Unlock Sadi")]
		UNLOCK_SADIDA = 15,
	    [Description("Unlock Cra")]
		UNLOCK_CRA = 16,
	    [Description("Unlock Enu")]
		UNLOCK_ENUTROF = 17,
	    [Description("Random Ranked vs Hidden IA")]
		RANDOM_RANKED_VS_HIDDEN_AI = 97,
	    [Description("Random Unranked vs Hidden IA")]
		RANDOM_UNRANKED_VS_HIDDEN_AI = 98,
	    [Description("IA vs IA")]
		IA_VS_IA = 99,
		[Description("2 vs 2")]
	    Deux_VS_Deux = 101
	}
}
