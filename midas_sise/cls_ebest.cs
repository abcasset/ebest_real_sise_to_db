using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices; //DllImport
using System.Text;
using System.Threading.Tasks;

namespace midas_sise
{

    public class cls_ebest
    {
        [DllImport("xingapi.dll")]
        public static extern bool ETK_AdviseRealData(IntPtr _Handle, string _TransactionCode, string ptr_input, int _Size);

        [DllImport("xingapi.dll")]
        public static extern bool ETK_Connect(IntPtr _Handle, string _Address, int _Port, int _MessageID, int _TimeOut, int _PacketSize);


        [DllImport("xingapi.dll")]
        public static extern bool ETK_Disconnect();


        [DllImport("xingapi.dll")]
        public static extern bool ETK_Login(IntPtr _Handle, string _ID, string _LoginPassword, string _CertificationPassword, int _Type, bool _DialogueWindow);
        
        [DllImport("xingapi.dll")]
        public static extern int ETK_Request(IntPtr _Handle, string _TransactionCode, IntPtr ptr_input, int ptr_inputSize, bool _Continue, string _ContinueKey, int _TimeOut);
        
        [DllImport("xingapi.dll")]
        public static extern void ETK_ReleaseMessageData(IntPtr _lParam);  //lParam

        [DllImport("xingapi.dll")]
        public static extern void ETK_ReleaseRequestData(int _RequestID);  //lParam.ToInt32()

        public static char[] FillBlock(ref char[] _CharArray, string _Value, int _Length) // 송신 패킷을 쉽게 만들기 위한 건데 예제에서는 이용되지 않았음
        {
            _CharArray = _Value.PadRight(_Length).ToCharArray();

            return _CharArray;
        }


        //IXingAPI.h
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct request_packet
        {
            public int RequestID; //4
            public int DataLength;//4
            public int TotalDataBufferSize;//4
            public int ElapsedTime;//4
            public int DataMode;//4
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10 + 1)]
            public string TransactionCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string Continue;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18 + 1)]
            public string ContinueKey;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30 + 1)]
            public string UserData;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string BlockName;
            public IntPtr Data; //문자열 포인터
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct real_msg
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3 + 1)]
            public string TransactionCode;
            public int KeyLength; //4
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 + 1)]
            public string Key;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 + 1)]
            public string RegisterKey;
            public int DataLength; //4
            public IntPtr ptr_Data; //문자열 포인터
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct YS3_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] hotime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] yeprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yeprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] yevolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yevolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] jnilysign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilysign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] preychange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _preychange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] jnilydrate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilydrate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] yofferho0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yofferho0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] ybidho0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ybidho0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] yofferrem0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yofferrem0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] ybidrem0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ybidrem0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct YK3_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] hotime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] yeprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yeprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] yevolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yevolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] jnilysign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilysign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] preychange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _preychange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] jnilydrate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilydrate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] yofferho0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yofferho0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] ybidho0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ybidho0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] yofferrem0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yofferrem0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] ybidrem0;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ybidrem0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct JH0_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] futcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _futcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] hotime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] offerho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bidho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] totofferrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totofferrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] totbidrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] totoffercnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totoffercnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] totbidcnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] danhochk;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _danhochk;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] alloc_gubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _alloc_gubun;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct JC0_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] futcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] chetime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] drate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] price;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] open;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] high;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] low;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] cgubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] cvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] value;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] mdvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] mdchecnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] msvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] mschecnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)] public char[] cpower;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] openyak;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] k200jisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] theoryprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] kasis;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] sbasis;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] ibasis;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] openyakcha;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public char[] jgubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] jnilvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] basprice;
        }



        // 시간조회
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t0167_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] id;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t0167_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] dt;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] time;
        }








        //주식종목 마스터 조회
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t8430_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t8430_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] expcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] etfgubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] uplmtprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] dnlmtprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] jnilclose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] memedan;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] recprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
        }

        // 아주 주의 API는 더미칼럼이 없어야 함..  아주아주주주의
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t8436_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] gubun;
        }

        // 아주 주의 API는 더미칼럼이 없어야 함..  아주아주주주의
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t8436_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public char[] hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] expcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] etfgubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] uplmtprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] dnlmtprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] jnilclose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)] public char[] memedan;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] recprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public char[] bu12gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] spac_gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public char[] filler;
        }

        //50종목 멀티 현재가
        [StructLayout(LayoutKind.Sequential, Pack = 1)]  //input에도 더미가 필요.  devcenter에 길이를 보니 303아닌 305라서 찾음
        public struct t8407_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] nrec;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _nrec;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 300)] public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t8407_OutBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)] public char[] hname;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] price;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _price;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] diff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _diff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] offerho;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] bidho;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] cvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _cvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)] public char[] chdegree;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _chdegree;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] open;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _open;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] high;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _high;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] low;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _low;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] value;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _value;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] offerrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] bidrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] totofferrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _totofferrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] totbidrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _totbidrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] jnilclose;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilclose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] uplmtprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _uplmtprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] dnlmtprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _dnlmtprice;
        }


        // 업종 지수조회 001 , 101: ㅏ=코스피200
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t1511_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] upcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _upcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t1511_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] gubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public char[] hname;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] pricejisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _pricejisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] jniljisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jniljisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] diffjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _diffjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] jnilvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] volumechange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _volumechange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] volumerate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _volumerate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] jnilvalue;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilvalue;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] value;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _value;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] valuechange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _valuechange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] valuerate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _valuerate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] openjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _openjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] opendiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _opendiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] opentime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _opentime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] highjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _highjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] highdiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _highdiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] hightime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _hightime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] lowjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _lowjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] lowdiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _lowdiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] lowtime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _lowtime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] whjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _whjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] whchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _whchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] whjday;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _whjday;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] wljisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _wljisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] wlchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _wlchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] wljday;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _wljday;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] yhjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yhjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] yhchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yhchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] yhjday;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yhjday;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] yljisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yljisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] ylchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ylchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] yljday;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _yljday;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] firstjcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _firstjcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public char[] firstjname;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _firstjname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] firstjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _firstjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] firsign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _firsign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] firchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _firchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] firdiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _firdiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] secondjcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _secondjcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public char[] secondjname;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _secondjname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] secondjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _secondjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] secsign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _secsign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] secchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _secchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] secdiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _secdiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] thirdjcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _thirdjcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public char[] thirdjname;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _thirdjname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] thirdjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _thirdjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] thrsign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _thrsign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] thrchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _thrchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] thrdiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _thrdiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] fourthjcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _fourthjcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public char[] fourthjname;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _fourthjname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] fourthjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _fourthjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] forsign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _forsign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] forchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _forchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] fordiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _fordiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public char[] highjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _highjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public char[] upjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _upjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public char[] unchgjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _unchgjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public char[] lowjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _lowjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public char[] downjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _downjo;
        }



        //종목 현재가
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t1101_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] shcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t1101_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]            public char[] hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _hname;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] price;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _price;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] diff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _diff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] jnilclose;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jnilclose;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] offerho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] bidho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offer;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bid;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] preoffercha;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _preoffercha;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prebidcha;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _prebidcha;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] hotime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] yeprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _yeprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] yevolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _yevolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] yesign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _yesign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] yechange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _yechange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] yediff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _yediff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] tmoffer;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _tmoffer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] tmbid;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _tmbid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ho_status;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _ho_status;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] uplmtprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _uplmtprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] dnlmtprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _dnlmtprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] open;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _open;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] high;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _high;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] low;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _low;

        }


        // 코스피 주식호가
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct H1__InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] shcode;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct H1__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] hotime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] totofferrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totofferrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] totbidrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] donsigubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _donsigubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] alloc_gubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _alloc_gubun;
        }



        // 코스닥 주식호가  ?? In 이 없네???? 
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HA__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem6;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem6;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem7;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem7;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem8;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem8;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem9;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem9;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidho10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] offerrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] bidrem10;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem10;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] totofferrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totofferrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] totbidrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] donsigubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _donsigubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] alloc_gubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _alloc_gubun;
        }




        //==


        // 코스피 주식호가

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct S3__InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct S3__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] chetime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _chetime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] drate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _drate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] price;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _price;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] opentime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _opentime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] open;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _open;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] hightime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _hightime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] high;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _high;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] lowtime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _lowtime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] low;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _low;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] cgubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _cgubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] cvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _cvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] value;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _value;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] mdvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _mdvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] mdchecnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _mdchecnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] msvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _msvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] mschecnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _mschecnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)] public char[] cpower;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _cpower;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] w_avrg;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _w_avrg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] offerho;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] bidho;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public char[] status;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _status;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] public char[] jnilvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _jnilvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
        }


        //== == 1515

        /// 라6. 거래원  /////////  


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct K1__InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct K1__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] offerno1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] bidno1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] offertrad1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offertrad1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] bidtrad1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidtrad1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdvol1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdvol1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmsvol1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsvol1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmdrate1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdrate1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmsrate1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsrate1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdcha1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdcha1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmscha1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmscha1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] offerno2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] bidno2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] offertrad2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offertrad2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] bidtrad2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidtrad2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdvol2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdvol2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmsvol2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsvol2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmdrate2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdrate2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmsrate2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsrate2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdcha2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdcha2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmscha2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmscha2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] offerno3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerno3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] bidno3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidno3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] offertrad3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offertrad3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] bidtrad3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidtrad3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdvol3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdvol3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmsvol3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsvol3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmdrate3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdrate3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmsrate3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsrate3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdcha3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdcha3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmscha3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmscha3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] offerno4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerno4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] bidno4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidno4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] offertrad4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offertrad4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] bidtrad4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidtrad4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdvol4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdvol4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmsvol4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsvol4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmdrate4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdrate4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmsrate4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsrate4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdcha4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdcha4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmscha4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmscha4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] offerno5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerno5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public char[] bidno5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidno5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] offertrad5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offertrad5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] bidtrad5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidtrad5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdvol5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdvol5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmsvol5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsvol5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmdrate5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdrate5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] tradmsrate5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsrate5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmdcha5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdcha5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] tradmscha5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmscha5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] ftradmdvol;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmdvol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] ftradmsvol;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmsvol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] ftradmdrate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmdrate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] ftradmsrate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmsrate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] ftradmdcha;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmdcha;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)] public char[] ftradmscha;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmscha;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmdval1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdval1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmsval1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsval1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmdavg1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdavg1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmsavg1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsavg1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmdval2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdval2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmsval2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsval2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmdavg2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdavg2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmsavg2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsavg2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmdval3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdval3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmsval3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsval3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmdavg3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdavg3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmsavg3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsavg3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmdval4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdval4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmsval4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsval4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmdavg4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdavg4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmsavg4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsavg4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmdval5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdval5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] tradmsval5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsval5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmdavg5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmdavg5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] tradmsavg5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _tradmsavg5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] ftradmdval;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmdval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)] public char[] ftradmsval;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmsval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] ftradmdavg;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmdavg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)] public char[] ftradmsavg;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _ftradmsavg;
        }





        //== == 1515

        //== ==

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct S2__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] offerho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerho;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] bidho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidho;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct KS__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] offerho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _offerho;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public char[] bidho;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _bidho;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public char[] shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public char[] _shcode;
        }





        //==


        //옵션전광판
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t2301_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] yyyymm;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _yyyymm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gubun;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t2301_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] histimpv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _histimpv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] jandatecnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jandatecnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] cimpv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _cimpv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] pimpv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _pimpv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] gmprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gmprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gmsign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gmsign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] gmchange;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gmchange;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] gmdiff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gmdiff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] gmvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gmvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] gmshcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gmshcode;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t2301_OutBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] actprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _actprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] optcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _optcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] price;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _price;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] diff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _diff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] iv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _iv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] mgjv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _mgjv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] mgjvupdn;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _mgjvupdn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] cvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _cvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] delt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _delt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] gama;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gama;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] vega;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _vega;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] ceta;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _ceta;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] rhox;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _rhox;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] theoryprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _theoryprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] impv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _impv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] timevl;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _timevl;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] jvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] parpl;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _parpl;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] jngo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jngo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] open;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _open;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] high;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _high;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] low;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _low;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] atmgubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _atmgubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] jisuconv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jisuconv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] value;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _value;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t2301_OutBlock2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] actprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _actprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] optcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _optcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] price;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _price;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] diff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _diff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] iv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _iv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] mgjv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _mgjv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] mgjvupdn;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _mgjvupdn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] cvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _cvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] delt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _delt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] gama;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _gama;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] vega;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _vega;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] ceta;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _ceta;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] rhox;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _rhox;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] theoryprice;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _theoryprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] impv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _impv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] timevl;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _timevl;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] jvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] parpl;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _parpl;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] jngo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jngo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] open;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _open;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] high;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _high;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] low;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _low;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] atmgubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _atmgubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] jisuconv;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _jisuconv;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] value;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _value;
        }


        // 선물 호가
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FH0_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] hotime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] totofferrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totofferrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] totbidrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] totoffercnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totoffercnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] totbidcnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] futcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _futcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] danhochk;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _danhochk;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] alloc_gubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _alloc_gubun;
        }


        // 옵션 호가
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OH0_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] hotime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _hotime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt1;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt2;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt3;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt3;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt4;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt4;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] offerho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] bidho5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidho5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] offerrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offerrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] bidrem5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidrem5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] offercnt5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _offercnt5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bidcnt5;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _bidcnt5;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] totofferrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totofferrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] totbidrem;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidrem;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] totoffercnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totoffercnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] totbidcnt;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _totbidcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] optcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _optcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] danhochk;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _danhochk;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] alloc_gubun;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _alloc_gubun;
        }


        // 주식주문
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAT00600_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] InptPwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] IsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] OrdPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BnsTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdprcPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] MgntrnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] LoanDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] OrdCndiTpCode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAT00600_OutBlock2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] OrdTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] ShtnIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] MgempNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] SpareOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] CvrgSeqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] RsvOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] SpotOrdQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] RuseOrdQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MnyOrdAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] SubstOrdAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] RuseOrdAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] AcntNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] IsuNm;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAT00800_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] InptPwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] IsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdQty;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAT00800_OutBlock2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] PrntOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] OrdTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] ShtnIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] PrgmOrdprcPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] StslOrdprcTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] StslAbleYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] MgntrnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] LoanDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] CvrgOrdTp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] LpYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] MgempNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BnsTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] SpareOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] CvrgSeqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] RsvOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] AcntNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] IsuNm;
        }


        // 주식잔고
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAQ12300_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Pwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BalCreTp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] CmsnAppTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] D2balBaseQryTp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] UprcTpCode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAQ12300_OutBlock3
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] IsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] IsuNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] SecBalPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] SecBalPtnNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BalQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BnsBaseBalQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] CrdayBuyExecQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] CrdaySellExecQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            public char[] SellPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            public char[] BuyPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] SellPnlAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] PnlRat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] NowPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] CrdtAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] DueDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] PrdaySellExecPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PrdaySellQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] PrdayBuyExecPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PrdayBuyQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] LoanDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] AvrUprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] SellAbleQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] SellOrdQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] CrdayBuyExecAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] CrdaySellExecAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PrdayBuyExecAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PrdaySellExecAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BalEvalAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] EvalPnl;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MnyOrdAbleAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdAbleAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] SellUnercQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] SellUnsttQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BuyUnercQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BuyUnsttQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] UnsttQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] UnercQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] PrdayCprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PchsAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] RegMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] LoanDtlClssCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] DpspdgLoanQty;
        }



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOFQ02400_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Pwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] RegMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] BuyDt;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOFQ02400_OutBlock4
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] IsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] IsuNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BnsTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] BnsTpNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BalQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
            public char[] FnoAvrPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BgnAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ThdayLqdtQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] Curprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] EvalAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] EvalPnlAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] EvalErnrat;
        }


        ///////////////
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC0InBlock
        {
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC0_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] len;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compress;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] encrypt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] offset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] compid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] userid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] media;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] ifid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] trid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] pubip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prvip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] pcbpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] termno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lang;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] proctm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] msgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] outgu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compreq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] funckey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] reqcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cont;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] contkey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varhdlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varmsglen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trsrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] eventid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ifinfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public char[] filler1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordchegb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] marketgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] accno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] passwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] expcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] shtcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] hogagb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] etfhogagb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] pgmtype;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gmhogagb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gmhogayn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] singb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] loandt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cvrgordtp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] strtgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] groupid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] prtno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] basketno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] trchno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] itemno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] brwmgmyn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mbrno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] procgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] admbrchno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] futaccno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] futmarketgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] tongsingb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lpgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] dummy;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] ordtm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] prntordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] mgempno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordundrqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordmdfyqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordordcancelqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] nmcpysndno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] spareordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] cvrgseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] rsvordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] mtordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spareordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orduserid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordruseqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mnyordamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordsubstamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ruseordamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordcmsnamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtuseamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotordableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordableruseqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] flctqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqtyd2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] sellableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercsellordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] avrpchsprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] pchsamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] deposit;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] substamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnmnymgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnsubstmgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgruseamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablemny;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablesubstamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ruseableamt;
        }

        /////////////////
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC1_InBlock
        {
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC1_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] len;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compress;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] encrypt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] offset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] compid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] userid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] media;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] ifid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] trid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] pubip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prvip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] pcbpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] termno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lang;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] proctm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] msgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] outgu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compreq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] funckey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] reqcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cont;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] contkey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varhdlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varmsglen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trsrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] eventid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ifinfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public char[] filler1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordxctptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgmtbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] accno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] acntnm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] Isuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] Isunm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] execno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] execqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] execprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] canccnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rjtqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ordtrxptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] mtiordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ordcndi;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordprcptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] nsavtrdqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] shtnIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] opdrtnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cvrgordtp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordunercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordmdfyqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordcancqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordavrexecprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] stdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bfstdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordtrdptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgntrncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] adduptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] commdacode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Loandt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mbrnmbrno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] ordacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] agrgbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] mgempno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] futsLnkbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] futsLnkacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] futsmkttp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] regmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] mnymgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] substmgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mnyexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ubstexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] cmsnamtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] prdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] stslexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] strtgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] grpId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ptflno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bskno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] trchno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] itemno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orduserId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] brwmgmtYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] frgrunqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trtzxLevytp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] exectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] rcptexectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rmndLoanamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotordableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordableruseqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] flctqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqtyd2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] sellableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercsellordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] avrpchsprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] pchsant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] deposit;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] substamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnmnymgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnsubstmgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgruseamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablemny;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablesubstamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ruseableamt;
        }


        ///////////

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC2_InBlock
        {
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC2_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] len;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compress;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] encrypt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] offset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] compid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] userid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] media;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] ifid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] trid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] pubip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prvip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] pcbpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] termno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lang;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] proctm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] msgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] outgu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compreq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] funckey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] reqcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cont;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] contkey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varhdlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varmsglen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trsrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] eventid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ifinfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public char[] filler1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordxctptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgmtbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] accno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] acntnm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] Isuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] Isunm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] execno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] execqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] execprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] canccnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rjtqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ordtrxptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] mtiordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ordcndi;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordprcptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] nsavtrdqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] shtnIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] opdrtnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cvrgordtp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordunercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordmdfyqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordcancqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordavrexecprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] stdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bfstdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordtrdptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgntrncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] adduptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] commdacode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Loandt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mbrnmbrno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] ordacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] agrgbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] mgempno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] futsLnkbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] futsLnkacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] futsmkttp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] regmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] mnymgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] substmgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mnyexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ubstexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] cmsnamtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] prdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] stslexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] strtgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] grpId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ptflno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bskno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] trchno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] itemno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orduserId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] brwmgmtYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] frgrunqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trtzxLevytp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] exectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] rcptexectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rmndLoanamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotordableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordableruseqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] flctqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqtyd2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] sellableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercsellordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] avrpchsprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] pchsant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] deposit;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] substamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnmnymgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnsubstmgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgruseamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablemny;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablesubstamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ruseableamt;
        }


        ///////

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC3_InBlock
        {
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC3_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] len;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compress;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] encrypt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] offset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] compid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] userid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] media;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] ifid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] trid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] pubip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prvip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] pcbpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] termno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lang;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] proctm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] msgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] outgu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compreq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] funckey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] reqcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cont;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] contkey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varhdlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varmsglen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trsrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] eventid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ifinfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public char[] filler1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordxctptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgmtbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] accno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] acntnm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] Isuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] Isunm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] execno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] execqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] execprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] canccnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rjtqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ordtrxptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] mtiordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ordcndi;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordprcptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] nsavtrdqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] shtnIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] opdrtnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cvrgordtp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordunercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordmdfyqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordcancqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordavrexecprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] stdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bfstdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordtrdptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgntrncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] adduptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] commdacode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Loandt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mbrnmbrno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] ordacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] agrgbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] mgempno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] futsLnkbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] futsLnkacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] futsmkttp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] regmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] mnymgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] substmgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mnyexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ubstexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] cmsnamtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] prdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] stslexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] strtgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] grpId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ptflno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bskno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] trchno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] itemno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orduserId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] brwmgmtYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] frgrunqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trtzxLevytp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] exectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] rcptexectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rmndLoanamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotordableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordableruseqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] flctqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqtyd2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] sellableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercsellordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] avrpchsprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] pchsant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] deposit;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] substamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnmnymgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnsubstmgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgruseamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablemny;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablesubstamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ruseableamt;
        }



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC4_InBlock
        {
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SC4_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] len;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compress;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] encrypt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] offset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] compid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] userid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] media;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] ifid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] trid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] pubip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prvip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] pcbpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] termno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lang;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] proctm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] msgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] outgu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compreq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] funckey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] reqcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cont;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] contkey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varhdlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varmsglen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trsrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] eventid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ifinfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public char[] filler1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordxctptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgmtbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] accno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] acntnm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] Isuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] Isunm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] execno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] execqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] execprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mdfycnfprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] canccnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rjtqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ordtrxptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] mtiordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ordcndi;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordprcptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] nsavtrdqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] shtnIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] opdrtnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cvrgordtp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordunercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordmdfyqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordcancqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordavrexecprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] stdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] bfstdIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordtrdptncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mgntrncode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] adduptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] commdacode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Loandt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] mbrnmbrno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] ordacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] agrgbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] mgempno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] futsLnkbrnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] futsLnkacntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] futsmkttp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] regmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] mnymgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] substmgnrat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mnyexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ubstexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] cmsnamtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] prdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdayruseexecval;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] stslexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] strtgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] grpId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ptflno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bskno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] trchno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] itemno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orduserId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] brwmgmtYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] frgrunqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trtzxLevytp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lptp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] exectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] rcptexectime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] rmndLoanamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] spotordableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordableruseqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] flctqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] secbalqtyd2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] sellableqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercsellordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] avrpchsprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] pchsant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] deposit;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] substamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnmnymgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] csgnsubstmgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] crdtpldgruseamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablemny;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordablesubstamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ruseableamt;
        }



        /////

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAQ13700_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] InptPwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BnsTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] IsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ExecYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] OrdDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] SrtOrdNo2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BkseqTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdPtnCode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CSPAQ13700_OutBlock3
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] OrdDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] MgmtBrnNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] IsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] IsuNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BnsTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] BnsTpNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] OrdPtnNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] OrdTrxPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public char[] OrdTrxPtnNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] MrcTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] MrcTpNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MrcQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MrcAbleQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] OrdPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ExecQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] ExecPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] ExecTrxTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] LastExecTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdprcPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] OrdprcPtnNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] OrdCndiTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] AllExecQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] RegCommdaCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] CommdaNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] MbrNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] RsvOrdYn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] LoanDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] OrdTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] OpDrtnNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OdrrId;
        }


        ///

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t8401_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] dummy;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t8401_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] shcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] expcode;
        }

        ///




        //
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAT00100_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Pwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FnoIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] BnsTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] FnoOrdprcPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] OrdPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdQty;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAT00100_OutBlock2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] BrnNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] AcntNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public char[] IsuNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdAbleAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MnyOrdAbleAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdMgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MnyOrdMgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdAbleQty;
        }

        //
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAT00200_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Pwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FnoIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] FnoOrdprcPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] OrdPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MdfyQty;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAT00200_OutBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Pwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FnoIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] FnoOrdPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] FnoOrdprcPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] OrdPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MdfyQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] CommdaCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] DscusBnsCmpltTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] GrpId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdSeqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] PtflNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] BskNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] TrchNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ItemNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] MgempNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FundId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] FundOrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] FundOrdNo;
        }


        //선물취소
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAT00300_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Pwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FnoIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] CancQty;
        }


        //선물취소
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAT00300_OutBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] OrdMktCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] Pwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FnoIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] FnoOrdPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] CancQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] CommdaCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] DscusBnsCmpltTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] GrpId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdSeqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] PtflNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] BskNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] TrchNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ItemNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] MgempNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FundId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] FundOrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] FundOrdNo;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAT00300_OutBlock2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] BrnNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] AcntNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public char[] IsuNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdAbleAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MnyOrdAbleAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdMgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] MnyOrdMgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdAbleQty;
        }


        //
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAQ00600_InBlock1
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] RecCnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AcntNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] InptPwd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] QrySrtDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] QryEndDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] FnoClssCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] PrdgrpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] PrdtExecTpCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] StnlnSeqTp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] CommdaCode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CFOAQ00600_OutBlock3
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] OrdDt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrgOrdNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] OrdTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] FnoIsuNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] IsuNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] BnsTpNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] MrcTpNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] FnoOrdprcPtnCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] FnoOrdprcPtnNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] OrdPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] OrdQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] OrdTpNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ExecTpNm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ExecPrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ExecQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] CtrctTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] CtrctNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ExecNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] BnsplAmt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] UnercQty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] UserId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] CommdaCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] CommdaCodeNm;
        }

        //
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct O01_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] len;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] gubun;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compress;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] encrypt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] offset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] compid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] userid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] media;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] ifid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] trid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] pubip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] prvip;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] pcbpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] termno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] lang;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] proctm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] msgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] outgu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] compreq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] funckey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] reqcnt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] cont;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] contkey;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varhdlen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] varmsglen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] trsrc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] eventid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] ifinfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public char[] filler1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] trcode1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] firmno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] acntno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] acntno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] acntnm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] brnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ordmktcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] ordno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] orgordno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] prntordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] prntordno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] isuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] fnoIsuno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] fnoIsunm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] pdgrpcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] fnoIsuptntp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] mrctp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] hogatype;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] mmgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] ordprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] commdacode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] peeamtcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] mgempno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
            public char[] fnotrdunitamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] trxtime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] strtgcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] grpId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordseqno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ptflno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] bskno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] trchno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] Itemno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] userId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] opdrtnno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] rjtcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mrccnfqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordunercqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] orgordmrcqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] ctrcttime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ctrctno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] execprc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] execqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] newqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] qdtqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] lastqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] lallexecqty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] allexecamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] fnobalevaltp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] bnsplamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] fnoIsuno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] execprc1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] newqty1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] qdtqty1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] allexecamt1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] fnoIsuno2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] execprc2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] newqty2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] lqdtqty2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] allexecamt2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] dps;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ftsubtdsgnamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mnymgn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ordableamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] mnyordableamt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] fnoIsuno_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unsttqty_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] lqdtableqty_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] avrprc_1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] fnoIsuno_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] bnstp_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] unsttqty_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] lqdtableqty_2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public char[] avrprc_2;
        }




        //
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct C01_InBlock
        {
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct C01_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] megrpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] boardid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] memberno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] expcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] yakseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] cheprice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] chevol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] sessionid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] chedate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] chetime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] spdprc1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] spdprc2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] dosugb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] accno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] sihogagb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] jakino;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] daeyong;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] mem_filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] mem_accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 42)]
            public char[] mem_filler1;
        }


        //2초 예상지수

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct YJ__InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] upcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct YJ__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] time;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]              public char[] _time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] jisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _jisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] drate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _drate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] cvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _cvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] value;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _value;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] upcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _upcode;
        }


        // 2초 장중 체결가

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct IJ__InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] upcode;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct IJ__OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] time;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _time;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] jisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _jisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] drate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _drate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] cvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _cvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] volume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _volume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] value;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _value;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] upjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _upjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] highjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _highjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] unchgjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _unchgjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] lowjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _lowjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] downjo;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _downjo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] upjrate;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _upjrate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] openjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _openjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] opentime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _opentime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] highjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _highjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] hightime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _hightime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] lowjisu;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _lowjisu;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] lowtime;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _lowtime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] frgsvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _frgsvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] orgsvolume;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _orgsvolume;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] frgsvalue;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _frgsvalue;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgsvalue;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _orgsvalue;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] upcode;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]            public char[] _upcode;
        }


        //


        //
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct H01_InBlock
        {
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct H01_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] lineseq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] user;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] seq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] trcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] megrpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] boardid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] memberno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] bpno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] orgordno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] expcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] dosugb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] mocagb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] accno1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] qty2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] price;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] ordgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] hogagb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] sihogagb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] tradid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] treacode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] askcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] creditcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] jakigb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public char[] trustnum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] ptgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] substonum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] accgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] accmarggb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public char[] nationcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] investgb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] forecode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] medcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] ordid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] macid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] orddate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] rcvtime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public char[] mem_filler;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public char[] mem_accno;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 42)]
            public char[] mem_filler1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public char[] ordacpttm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] qty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] autogb;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] rejcode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] prgordde;
        }

        //
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t3521_InBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] kind;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _kind;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] symbol;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _symbol;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct t3521_OutBlock
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] symbol;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _symbol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] hname;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _hname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] close;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _close;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] sign;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _sign;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] change;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _change;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] diff;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _diff;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] date;[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public char[] _date;
        }


        //
        //


        //

    }

}
