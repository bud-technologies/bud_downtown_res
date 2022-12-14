using System;
using BudEngine.NetEngine.src;
using BudEngine.NetEngine.src.SDK;


namespace BudEngine.NetEngine
{
    public class Global
    {
        // public static RoomInfo roomInfo;

        public static Room Room { get; set; }

        public static UserInfo UserInfo { get; set; }

        public static string OpenId { get; set; }

        public static string GameId { get; set; }

        public static string SecretKey { get; set; }

        public static string Server { get; set; }

        //public static void GetRoomList(GetRoomListPara para, Action<ResponseEvent> callback)
        //{
        //    Room.GetRoomList(para, callback);
        //}

        public static void UnInit()
        {
            Listener.Clear();
            Core.UnInitSdk();
        }

        public static bool IsInRoom()
        {
            return Room != null && Room.IsInRoom();
        }
    }

    public abstract class UserInfo
    {
        
    }
}