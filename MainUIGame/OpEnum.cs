using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUIGame
{
    enum commOp
    {
        error = -1,
        availRoomsReq = 1,
        createRoomReq = 2,
        joinRoomAsPlayer = 3,
        joinRoomAsSpectator = 4,
        getWholeBoard = 5,
        playerMoveReq = 6,
        roomsResp = 7,
        Accept = 8,
        winLoss = 9

    }
}
