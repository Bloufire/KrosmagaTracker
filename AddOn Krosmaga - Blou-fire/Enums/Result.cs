using System;
using System.Collections.Generic;
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
		None,
		[LocDescription("Enum_GameResult_Win")]
		Win,
		[LocDescription("Enum_GameResult_Loss")]
		Loss,
		[LocDescription("Enum_GameResult_Draw")]
		Draw
	}

}
