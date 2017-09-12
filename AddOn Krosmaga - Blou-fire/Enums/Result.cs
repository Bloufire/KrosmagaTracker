using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Enums
{
    public enum Result
    {
        WIN_LOSE = 1,
        DRAW = 2,
        CANCELLED = 3
    }

	public enum GameResult
	{
		[LocDescription("Enum_GameResult_None")]
		[Description("None")]
		None = 3,
		[LocDescription("Enum_GameResult_Win")]
		[Description("Win")]
		Win = 1,
		[LocDescription("Enum_GameResult_Loss")]
		[Description("Loss")]
		Loss = 0,
		[LocDescription("Enum_GameResult_Draw")]
		[Description("Draw")]
		Draw = 2
	}

}
