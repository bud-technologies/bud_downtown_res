using System;


namespace BudEngine.NetEngine.src.Util.Def
{
    [Serializable]
    public static class Config
    {
        private static int pingTimeout = 6000;
        private static int reconnectInterval = 500;
        private static int reconnectMaxTimes = 15;
        private static int _resendInterval = 1000;
        private static int _resendTimeout = 20000;
        private const int KcpInterval = 15;
        private static string url = "";
        private static bool _enableUdp = true;
        private static bool _isAutoRequestFrame = false;

        public static int PingTimeout
        {
            get => pingTimeout;
            set => pingTimeout = value;
        }

        public static int ReconnectInterval
        {
            get => reconnectInterval;
            set => reconnectInterval = value;
        }

        public static int ReconnectMaxTimes
        {
            get => reconnectMaxTimes;
            set => reconnectMaxTimes = value;
        }

        public static int ResendInterval
        {
            get => _resendInterval;
            set => _resendInterval = value;
        }

        public static int ResendTimeout
        {
            get => _resendTimeout;
            set => _resendTimeout = value;
        }

        public static int K => KcpInterval;

        public static string Url
        {
            get => url;
            set => url = value;
        }

        public static bool EnableUdp
        {
            get => _enableUdp;
            set => _enableUdp = value;
        }

        public static bool IsAutoRequestFrame
        {
            get => _isAutoRequestFrame;
            set => _isAutoRequestFrame = value;
        }

        public static void Assign(ConfigPara config)
        {
            ReconnectMaxTimes = config.ReconnectMaxTimes;
            ReconnectInterval = config.ReconnectInterval;
            Url = config.Url;
            ResendInterval = config.ResendInterval;
            ResendTimeout = config.ResendTimeout;
            IsAutoRequestFrame = config.IsAutoRequestFrame;

            Port.TcpRelayPort = config.Port;
            Port.TcpRelayPort2 = config.FramePort;
            Port.UdpRelayPort = config.Port;
        }
    }

    public static class Port
    {
        //private const int _tcpRelayPort = 5443;
        //private const int _udpRelayPort = 8585;
        private static int _tcpRelayPort = 12345;
        private static int _tcpRelayPort2 = _tcpRelayPort+2000;
        private static int _udpRelayPort = 12345;

        //public static int TcpRelayPort => _tcpRelayPort;

        //public static int UdpRelayPort => _udpRelayPort;

        public static int TcpRelayPort { get { return _tcpRelayPort; } set { _tcpRelayPort = value; } }

        public static int TcpRelayPort2 { get { return _tcpRelayPort2; } set { _tcpRelayPort2 = value; } }

        public static int UdpRelayPort { get { return _udpRelayPort; } set { _udpRelayPort = value; } }

        public static int GetRelayPort()
        {
            return Config.EnableUdp ? UdpRelayPort : TcpRelayPort;
        }
    }

    public static class RequestHeader
    {
        private const string _version = "1.3.3.1";
        private const string _appName = "";
        private const string _cmd = "";
        private const string _seq = "";
        private const string _clientIp = "";
        private const string _serviceIp = "";
        private const string _business = "";
        private const uint _authType = 0;
        private const string _authIp = "";
        private const uint _uid = 0;
        private const byte[] _body = null;

        public static string Version => _version;

        public static string AppName => _appName;

        public static string Cmd => _cmd;

        public static string Seq => _seq;

        public static string ClientIp => _clientIp;

        public static string ServiceIp => _serviceIp;

        public static string Business => _business;

        public static string AuthKey { get; set; } = "";

        public static uint AuthType => _authType;

        public static string AuthIp => _authIp;

        public static string GameId { get; set; } = "";

        public static uint Uid => _uid;

        public static string PlayerId { get; set; } = "";

        public static byte[] Body => _body;
    }

}