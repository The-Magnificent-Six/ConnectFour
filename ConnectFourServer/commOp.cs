using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    enum commOp
    {
        error = -1,
        availRoomsReq = 1,
        createRoomReq = 2,
        joinRoomAsPlayer= 3,
        joinRoomAsSpectator = 4,
        WholeBoard = 5,
        playerMoveReq = 6, 
        roomsResp = 7,
        accept = 8,
        winLoss = 9,
        rematch = 10

    }
}
