using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    enum commOp
    {
        availRoomsReq = 1,
        createRoomReq = 2,
        joinRoomAsPlayer= 3,
        joinRoomAsSpectator = 4,
        getWholeBoard = 5,
        playerMoveReq = 6, 
        roomsResp = 7,
        ErrorOrAccept = 8

    }
}
