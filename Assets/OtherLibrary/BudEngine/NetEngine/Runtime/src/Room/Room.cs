using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudEngine.NetEngine.src.Net;
using BudEngine.NetEngine.src.Util;
using UnityEngine;

namespace BudEngine.NetEngine.src.Room
{
    public class Room : BaseNetUtil
    {
        private readonly ServerSendClientBstType _joinRoomBroadcastType = ServerSendClientBstType.EPushTypeJoinRoom;
        private readonly ServerSendClientBstType _leaveRoomBroadcastType = ServerSendClientBstType.EPushTypeLeaveRoom;
        private readonly ServerSendClientBstType _dismissRoomBroadcastType = ServerSendClientBstType.EPushTypeDismissRoom;
        private readonly ServerSendClientBstType _removeUserBroadcastType = ServerSendClientBstType.EPushTypeRemovePlayer;
        private readonly ServerSendClientBstType _roomUserNetworkBroadcastType = ServerSendClientBstType.EPushTypeNetworkState;
        private readonly ServerSendClientBstType _testBroadcastType = ServerSendClientBstType.EPushTypeTest;

        public Room(BstCallbacks bstCallbacks) : base(bstCallbacks)
        {
            // 注册广播
            this.SetBroadcastHandler(this._joinRoomBroadcastType, this.OnJoinRoom);
            this.SetBroadcastHandler(this._leaveRoomBroadcastType, this.OnLeaveRoom);
            this.SetBroadcastHandler(this._dismissRoomBroadcastType, this.OnDismissRoom);
            this.SetBroadcastHandler(this._removeUserBroadcastType, this.OnRemoveUser);
            this.SetBroadcastHandler(this._roomUserNetworkBroadcastType, this.OnChangePlayerNetworkState);
            this.SetBroadcastHandler(this._testBroadcastType, TestBroadcast);
        }

        ///////////////////////////////// 请求 //////////////////////////////////

        // 创建房间
        public string CreateRoom(CreateRoomReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdCreateRoomReq;
            var response = new NetResponseCallback(CreateRoomResponse);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("CreateRoom_Para {0} {1}", para, seq);
            return seq;
        }

        // 加入房间
        public string JoinRoom(JoinRoomReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdJoinRoomReq;
            var response = new NetResponseCallback(JoinRoomResponse);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("JoinRoom_Para {0} {1}", para, seq);
            return seq;
        }
        
        // 简化后进入房间
        public string EnterRoom(EnterRoomReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdEnterRoomReq;
            var response = new NetResponseCallback(EnterRoomResponse);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("EnterRoom_Para {0} {1}", para, seq);
            return seq;
        }

        // 离开房间
        public string LeaveRoom(LeaveRoomReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdQuitRoomReq;
            var response = new NetResponseCallback(LeaveRoomResponse);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("LeaveRoom_Para {0} {1}", para, seq);
            return seq;
        }

        // 解散房间
        public string DismissRoom(DismissRoomReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdDissmissRoomReq;
            var response = new NetResponseCallback(DismissRoomResponse);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("DismissRoom_Para {0} {1}", para, seq);
            return seq;
        }

        // 房间变更
        //public string ChangeRoom(ChangeRoomReq para, Action<ResponseEvent> callback)
        //{
        //    const int subcmd = (int)ClientSendServerReqCmd.ECmdChangeRoomPropertisReq;
        //    var response = new NetResponseCallback(ChangeRoomResponse);
        //    var seq = this.Send(para, subcmd, response, callback);
        //    Debugger.Log("ChangeRoom_Para {0} {1}", para, seq);
        //    return seq;
        //}
        // 移除房间内玩家
        public string RemoveUser(RemovePlayerReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdRemoveMemberReq;
            var response = new NetResponseCallback(RemoveUserResponse);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("RemoveUser_Para {0} {1}", para, seq);
            return seq;
        }
        
        // 查询房间详情
        public string GetRoomByRoomId(GetRoomByRoomIdReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdGetRoomDetailReq;
            var response = new NetResponseCallback(GetRoomByRoomIdRsp);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("GetRoomByRoomId_Para {0} {1}", para, seq);
            return seq;
        }

        // 客户端上报信息给服务器
        public string ClientSendToServer(SendToGameSvrReq para, Action<ResponseEvent> callback)
        {
            const int subcmd = (int)ClientSendServerReqCmd.ECmdRelayClientSendtoGamesvrReq;
            var response = new NetResponseCallback(ClientSendToServerRsp);
            var seq = this.Send(para, subcmd, response, callback);
            Debugger.Log("ClientSendToServer_Para {0} {1}", para, seq);
            return seq;
        }

        // 查询房间列表
        //public string GetRoomList(GetRoomListReq para, Action<ResponseEvent> callback)
        //{
        //    const int subcmd = (int)ClientSendServerReqCmd.ECmdGetRoomListV2Req;
        //    var response = new NetResponseCallback(GetRoomListResponse);
        //    var seq = this.Send(para, subcmd, response, callback);
        //    Debugger.Log("GetRoomList_Para {0} {1}", para, seq);
        //    return seq;
        //}
        ///////////////////////////////// 响应 //////////////////////////////////

        // 创建房间
        private void CreateRoomResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("CreateRoomResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }

        // 加入房间
        private void JoinRoomResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("JoinRoomResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }
        
        // 简化后加入房间
        private void EnterRoomResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("EnterRoomResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }

        // 离开房间
        private void LeaveRoomResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("LeaveRoomResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }
        // 解散房间
        private void DismissRoomResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("DismissRoomResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }
        // 房间变更
        private void ChangeRoomResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("ChangeRoomResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }


        // 踢人操作
        private void RemoveUserResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("RemoveUserResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }

        // 查询房间详情
        private void GetRoomByRoomIdRsp(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("GetRoomByRoomIdRsp {0}", eve);
            callback?.Invoke(eve);
            return;
        }

        // 查询房间列表
        private void GetRoomListResponse(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("GetRoomListResponse {0}", eve);
            callback?.Invoke(eve);
            return;
        }

        private void ClientSendToServerRsp(bool send, DecodeRspResult res, Action<ResponseEvent> callback)
        {
            var rspPacket = res.RspPacket;
            var eve = new ResponseEvent(rspPacket.ErrCode, rspPacket.ErrMsg, rspPacket.Seq, res.Body);
            Debugger.Log("ClientSendToServerRsp {0}", eve);
            callback?.Invoke(eve);
            return;
        }

        ////////////////////////////////////// 广播  /////////////////////////////////////////
        private void OnJoinRoom(DecodeBstResult bst, string seq)
        {
            var eve = new BroadcastEvent(bst.Body, seq);
            Debugger.Log("onJoinRoom {0}", eve);
            this.bstCallbacks.Room.OnJoinRoom(eve);
        }

        private void OnLeaveRoom(DecodeBstResult bst, string seq)
        {
            var eve = new BroadcastEvent(bst.Body, seq);
            Debugger.Log("OnLeaveRoom {0}", eve);
            this.bstCallbacks.Room.OnLeaveRoom(eve);
        }

        private void OnDismissRoom(DecodeBstResult bst, string seq)
        {
            var eve = new BroadcastEvent(bst.Body, seq);
            Debugger.Log("OnDismissRoom {0}", eve);
            this.bstCallbacks.Room.OnDismissRoom(eve);
        }

        //private void OnChangeRoom(DecodeBstResult bst, string seq)
        //{
        //    var eve = new BroadcastEvent(bst.Body, seq);
        //    Debugger.Log("OnChangeRoom {0}", eve);
        //    this.bstCallbacks.Room.OnChangeRoom(eve);
        //}

        private void OnRemoveUser(DecodeBstResult bst, string seq)
        {
            var eve = new BroadcastEvent(bst.Body, seq);
            Debugger.Log("OnRemoveUser {0}", eve);
            this.bstCallbacks.Room.OnRemovePlayer(eve);
        }

        //private void OnChangeCustomPlayerStatus(DecodeBstResult bst, string seq)
        //{
        //    var eve = new BroadcastEvent(bst.Body, seq);
        //    Debugger.Log("OnChangeCustomPlayerStatus {0}", eve);
        //    this.bstCallbacks.Room.OnChangeCustomPlayerStatus(eve);
        //}

        //private void OnChangeRoomPlayerProfile(DecodeBstResult bst, string seq)
        //{
        //    var eve = new BroadcastEvent(bst.Body, seq);
        //    Debugger.Log("OnChangeRoomPlayerProfile {0}", eve);
        //    this.bstCallbacks.Room.OnChangeRoomPlayerProfile(eve);
        //}

        private void OnChangePlayerNetworkState(DecodeBstResult bst, string seq)
        {
            var eve = new BroadcastEvent(bst.Body, seq);
            Debugger.Log("OnChangePlayerNetworkState {0}", eve);
            this.bstCallbacks.Room.OnChangePlayerNetworkState(eve);

            //var groupIdList = changePlayerNetworkStateBst.GroupIdList;
            
            //if (groupIdList != null && groupIdList.Count > 0)
            //{
            //    this.bstCallbacks.Group.OnChangeGroupPlayerNetworkState(eve);
            //}
        }

        private static void TestBroadcast(DecodeBstResult bst, string seq)
        {
            Debugger.Log("TestBroadcast {0} {1}", bst, seq);
        }

    }
}
