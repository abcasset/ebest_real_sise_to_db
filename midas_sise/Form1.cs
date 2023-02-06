using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midas_sise
{
    public partial class Form1 : Form
    {
        int gi_DIFF_SECONd_GIJUN_15CHO = 15 ;
        //public const string gs_m1_2c = "R4";
        string gs_m1_2c;
        const int SW_HIDE = 0; // 숨기기
        const int SW_SHOW = 1; // 보이기
        int gb_win_show = 0; //1; // 보이기

        public const int i100 = 100;

        static Dictionary<string, string> sd_shcd_nm = new Dictionary<string, string>();  //종목은 3000개, sf 140개 라서 cls로 하면 안됨

        //string gs_connectionString = "server = 127.0.0.1, 1433; uid = fuser; pwd = mada3787; database = fnguide;";
        //string gs_connectionString = "server = 128.50.245.140, 1433; uid = fuser; pwd = mada3787; database = fnguide;";
        //string gs_connectionString = "server = 128.50.245.114, 1433; uid = fuser; pwd = mada3787; database = fnguide;";
        //bool gb_최초로그인 = true;

        //bool gb_하트비트 = false;
        bool gb_하트비트 = true;  //True 가 초기화 값이 조심.

        string gs_today = DateTime.Now.ToString("yyyyMMdd");


        DateTime gdatetime_recv_time_interval_IMPO_2cho;
        DateTime gt_지수_2초_recv_time;

        System.Windows.Forms.Timer gt_timer_4cho = new System.Windows.Forms.Timer(); //주식 잔고

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //static Dictionary<string, sfcd_shcd_nm> sd_sf3c_shcd_nm = new Dictionary<string, sfcd_shcd_nm>();
        static Dictionary<string, cl_jtong> sd_jtong = new Dictionary<string, cl_jtong>();
        //static Dictionary<string, cl_ytong> sd_ytong = new Dictionary<string, cl_ytong>();
        IntPtr g_MainWindowHandle;
        List<string> g_list_종목_makefile = new List<string>();

        public const int REQUEST_DATA = 1;
        public const int MESSAGE_DATA = 2;
        public const int SYSTEM_ERROR_DATA = 3;
        public const int RELEASE_DATA = 4;
        public const int LINK_DATA = 10;			// XM_RECEIVE_LINK_DATA 메시지의 구분 플래그

        string gs_sql_server_info;
        string gs_ebest_서버_ip;
        string gs_ebest_주식_no; //-> no
        string gs_ebest_선물_no; //-> no
        string gs_ebest_공통_pw;

        public static Func<string, int> ff_c1or2_to_buho = (x) => x == "2" ? 1 : -1;

        public Form1()
        {
            InitializeComponent();

            gt_timer_4cho.Tick += new EventHandler(f_timer_tick_4cho);
            gt_timer_4cho.Interval = 4000;
        }

        void f_button_login_Click()
        {
            g_MainWindowHandle = this.Handle;

            if (EB_rb_demo.Checked)
            {
                gs_ebest_서버_ip = "demo.ebestsec.co.kr";
                gs_ebest_주식_no = cls_config.GetAppConfig("config_ebest_demo_주식_id");
                gs_ebest_선물_no = cls_config.GetAppConfig("config_ebest_demo_선물_id");
                gs_ebest_공통_pw = cls_config.GetAppConfig("config_ebest_demo_공통_pw");
            }
            else
            {
                gs_ebest_주식_no = cls_config.GetAppConfig("config_ebest_hts_주식_id");
                gs_ebest_선물_no = cls_config.GetAppConfig("config_ebest_hts_선물_id");
                gs_ebest_공통_pw = cls_config.GetAppConfig("config_ebest_hts_공통_pw");
                if (EB_rb_hts.Checked)
                {
                    gs_ebest_서버_ip = "hts.ebestsec.co.kr1";
                }
                else if (EB_rb_127.Checked)
                {
                    gs_ebest_서버_ip = "127.0.0.1";
                }
            }

            if (cls_ebest.ETK_Connect(g_MainWindowHandle, gs_ebest_서버_ip, 20001, 1024, 5000, -1))
            {
                if (!cls_ebest.ETK_Login(g_MainWindowHandle, "skylee99", "aceg1357", "Aceg1357@@", 1, false))
                //if (!cls_ebest.ETK_Login(g_MainWindowHandle, "jjyy0818", "asas1212", "Asas1212@@", 1, false))
                {
                    Console.WriteLine("LOGIN FAILED");
                }
                else
                {
                    Console.WriteLine("(1) ETK_Login 성공");
                    f_log("(1) ETK_Login 성공");
                }
            }
            else
            {
                Console.WriteLine("CONNECTION FAILED");
            }
        }

        protected override void WndProc(ref Message m)
        {
            IntPtr hwnd = m.HWnd;
            int msg = m.Msg;
            IntPtr wParam = m.WParam;
            IntPtr lParam = m.LParam;

            if (msg == 1024 + 3)        //XM_RECEIVEintptr_input	3
            {
                f_rqst(hwnd, msg, wParam, lParam);
            }
            else if (msg == 1024 + 4)   //XM_RECEIVE_REALintptr_input 4
            {
                f_real(lParam);
            }
            else if (msg == 1024 + 5)   // XM_LOGIN 5 *
            {
                f_rqst_login(hwnd, msg, wParam, lParam);
            }
            base.WndProc(ref m);
        }

        void f_rqst_login(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if (Marshal.PtrToStringAnsi(wParam) == "0000")
            {
                //IntPtr intptr_input = Marshal.StringToHGlobalAnsi("0");
                //eBest.ETK_Request(MainWindowHandle, "t8430", intptr_input, 1, false, "", -1);
                //Marshal.FreeHGlobal(intptr_input);
            }
            Console.WriteLine("(2) 로그인 성공 - 메시지" + Marshal.PtrToStringAnsi(lParam).Trim());
            f_log("(2) 로그인 성공 - 메시지");
            f_init_1_after_login();
        }

        void f_init_1_after_login()
        {
            //지수도 종목파일로 advisereal하는 것처럼 별도로 뺄까 고민중. 20200605
            //f_read_file_sf3c_shcode_shname_request_JH0();
            gs_sql_server_info = cls_config.GetAppConfig("sql_server_info");
            //gs_m1_2c = cls_config.GetAppConfig("gs_m1_2c");

            //1414 f_t8430_request();
            f_t8436_request();


        }


        //void f_t8430_request()
        //{
        //    IntPtr _ptr = Marshal.StringToHGlobalAnsi("0");
        //    cls_ebest.ETK_Request(g_MainWindowHandle, "t8430", _ptr, 1, false, "", -1);
        //    Marshal.FreeHGlobal(_ptr);

        //    Thread.Sleep(300);
        //}

        void f_t8436_request()
        {
            IntPtr _ptr = Marshal.StringToHGlobalAnsi("0");  
            cls_ebest.ETK_Request(g_MainWindowHandle, "t8436", _ptr, 1, false, "", -1);
            Marshal.FreeHGlobal(_ptr);

            Thread.Sleep(300);
        }



        void f_rqst(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            int li_wParam;
            li_wParam = wParam.ToInt32();

            switch (li_wParam)
            {
                case REQUEST_DATA:   //1, REQUESTintptr_input	1 **
                    cls_ebest.request_packet msg_rqst = (cls_ebest.request_packet)Marshal.PtrToStructure(lParam, typeof(cls_ebest.request_packet));
                    string ls_msg_head = msg_rqst.TransactionCode;

                    switch (ls_msg_head)
                    {
                        case "t8436": 
                            //f_msg_t8430_주식전종목(msg_rqst);
                            f_msg_t8436_주식전종목(msg_rqst);
                            break;

                        case "t0167": // 서버시간               //f_msg_t0167(msg_rqst);
                            {
                                cls_ebest.t0167_OutBlock _t0167_OutBlock = (cls_ebest.t0167_OutBlock)Marshal.PtrToStructure(msg_rqst.Data, typeof(cls_ebest.t0167_OutBlock));

                                string ls_date = new string(_t0167_OutBlock.dt);
                                string ls_time = new string(_t0167_OutBlock.time);  //예: 123948099122  ;  들어오는 것은 12자리 이지만,  앞 6자리만 사용
                                string ls_time_8ch = ls_time.Substring(0, 8);

                                //gtime_recv_time_interval_NOT_2cho = f_string_to_time(ls_date, ls_time);
                                //gdatetime_recv_time_interval_IMPO_2cho = f_string_to_time(ls_date, ls_time_8ch);
                                //f_log("서버시간 : "+ ls_time);
                                //gb_하트비트 = false;
                            }
                            break;

                        case "t1101": // 현재가     //f_msg_t1101(msg_rqst);
                            {
                                cls_ebest.t1101_OutBlock _t1101_OutBlock = (cls_ebest.t1101_OutBlock)Marshal.PtrToStructure(msg_rqst.Data, typeof(cls_ebest.t1101_OutBlock));
                                Console.WriteLine(_t1101_OutBlock.price);
                            }
                            break;

                        case "t1511": // 업종 조회
                            {
                                f_log("업종 도착 1 ======");
                                f_msg_t1511(msg_rqst);
                            }
                            break;

                        case "t8407": // 50종목 멀티 현재가 
                            {
                                f_msg_t8407(msg_rqst);
                            }
                            break;

                        default:
                            Console.WriteLine("리퀘스트 case 없음 : 오류같음");
                            break;
                    }
                    break;

                case MESSAGE_DATA:  //or, MESSAGEintptr_input	2 *
                case SYSTEM_ERROR_DATA:  // SYSTEM_ERRORintptr_input3 *
                    cls_ebest.ETK_ReleaseMessageData(lParam);
                    break;

                case RELEASE_DATA:  //RELEASEintptr_input	 4 -
                    cls_ebest.ETK_ReleaseRequestData(lParam.ToInt32());  //내용이 아니라 object(pnt)구나
                    break;

                default:
                    Console.WriteLine("switch default error");
                    break;
            }
        }

        void f_real(IntPtr lParam)
        {
            cls_ebest.real_msg _real_msg = (cls_ebest.real_msg)Marshal.PtrToStructure(lParam, typeof(cls_ebest.real_msg));
            string ls_msg_head = _real_msg.TransactionCode;

            IntPtr _real_msg_ptr_Data = _real_msg.ptr_Data;

            switch (ls_msg_head)
            {
                case "S3_":  //체결가 KP
                case "K3_":  //체결가 KQ
                    {
                        cls_ebest.S3__OutBlock _S3__OutBlock = (cls_ebest.S3__OutBlock)Marshal.PtrToStructure(_real_msg_ptr_Data, typeof(cls_ebest.S3__OutBlock));

                        // 가
                        string ls_sh_cd = new string(_S3__OutBlock.shcode);
                        int li_prc = Int32.Parse(new string(_S3__OutBlock.price)); //현재가

                        try
                        {
                            //sd_jtong[ls_sh_cd].cd = ls_sh_cd;
                            sd_jtong[ls_sh_cd].prc = li_prc;
                        }
                        catch (Exception)
                        {
                            //throw;
                        }





                        // 다
                        string ls_chetime = new string(_S3__OutBlock.chetime); //체결시간
                        string ls_chetime2 = DateTime.Now.ToString("HHmmssffffff");
                        string ls_sign = new string(_S3__OutBlock.sign); //전일대비구분

                        int li_change = Int32.Parse(new string(_S3__OutBlock.change)); //전일대비
                        double ld_drate = Double.Parse(new string(_S3__OutBlock.drate)); //등락율
                        int li_price = Int32.Parse(new string(_S3__OutBlock.price)); //현재가

                        string ls_opentime = new string(_S3__OutBlock.opentime); //시가시간
                        int li_open = Int32.Parse(new string(_S3__OutBlock.open)); //시가
                        string ls_hightime = new string(_S3__OutBlock.hightime); //고가시간
                        int li_high = Int32.Parse(new string(_S3__OutBlock.high)); //고가
                        string ls_lowtime = new string(_S3__OutBlock.lowtime); //저가시간
                        int li_low = Int32.Parse(new string(_S3__OutBlock.low)); //저가

                        string ls_cgubun = new string(_S3__OutBlock.cgubun); //체결구분
                        int li_cvolume = Int32.Parse(new string(_S3__OutBlock.cvolume)); //체결량
                        int li_volume = Int32.Parse(new string(_S3__OutBlock.volume)); //누적거래량
                        int li_value = Int32.Parse(new string(_S3__OutBlock.value)); //누적거래대금

                        int li_mdvolume = Int32.Parse(new string(_S3__OutBlock.mdvolume)); //매도누적체결량
                        int li_mdchecnt = Int32.Parse(new string(_S3__OutBlock.mdchecnt)); //매도누적체결건수
                        int li_msvolume = Int32.Parse(new string(_S3__OutBlock.msvolume)); //매수누적체결량
                        int li_mschecnt = Int32.Parse(new string(_S3__OutBlock.mschecnt)); //매수누적체결건수

                        double ld_cpower = Double.Parse(new string(_S3__OutBlock.cpower)); //체결강도
                        int li_w_avrg = Int32.Parse(new string(_S3__OutBlock.w_avrg)); //가중평균가
                        int li_offerho = Int32.Parse(new string(_S3__OutBlock.offerho)); //매도호가
                        int li_bidho = Int32.Parse(new string(_S3__OutBlock.bidho)); //매수호가

                        string ls_status = new string(_S3__OutBlock.status); //장정보
                        int li_jnilvolume = Int32.Parse(new string(_S3__OutBlock.jnilvolume)); //전일동시간대거래량
                                                                                               //string ls_shcode = new string(_S3__OutBlock.shcode); //단축코드


                        //
                        string ls_sql_che = "INSERT INTO A_CHE (trd_dt,stk_cd,stk_nm,      tm, tm2,sign,change,drate,prc,opentime,opn,hightime,high,lowtime,low,cgubun,cvolume,volume,value,mdvolume,mdchecnt,msvolume,mschecnt,cpower,w_avrg,offerho,bidho,status,jnilvolume) VALUES ( '"

                                            + gs_today + "' ,'"
                                            + ls_sh_cd + "' ,'"
                                            + fl_shnm(ls_sh_cd) + "' ,'"

                                            + ls_chetime + "' ,'"
                                            + ls_chetime2 + "' ,'"
                                            + ls_sign + "' ,'"

                                            + li_change + "' ,'"
                                            + ld_drate + "' ,'"
                                            + li_price + "' ,'"

                                            + ls_opentime + "' ,'"
                                            + li_open + "' ,'"
                                            + ls_hightime + "' ,'"
                                            + li_high + "' ,'"
                                            + ls_lowtime + "' ,'"
                                            + li_low + "' ,'"

                                            //+ ff_gubun_to_buho(ls_cgubun) + "' ,'"
                                            + ls_cgubun + "' ,'"
                                            + li_cvolume + "' ,'"
                                            + li_volume + "' ,'"
                                            + li_value + "' ,'"

                                            + li_mdvolume + "' ,'"
                                            + li_mdchecnt + "' ,'"
                                            + li_msvolume + "' ,'"
                                            + li_mschecnt + "' ,'"

                                            + ld_cpower + "' ,'"
                                            + li_w_avrg + "' ,'"
                                            + li_offerho + "' ,'"
                                            + li_bidho + "' ,'"

                                            + ls_status + "' ,'"
                                            + li_jnilvolume + "') ;";













                        using (SqlConnection _conn = new SqlConnection(gs_sql_server_info))  ///1 of 5
                        {
                            _conn.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = _conn;
                            cmd.CommandText = ls_sql_che;
                            cmd.ExecuteNonQuery();
                        }















                    }
                    break;

               
                case "H1_": //주식 전호가 및 전호가잔량, 패킷 사이즈 아주 큼
                case "HA_":
                    { 
                        cls_ebest.H1__OutBlock _H1__OutBlock = (cls_ebest.H1__OutBlock)Marshal.PtrToStructure(_real_msg_ptr_Data, typeof(cls_ebest.H1__OutBlock)); //코스닥도 같겠지???

                        string ls_sh_cd = new string(_H1__OutBlock.shcode);
                        int li_sh_ask = f_char_to_int(_H1__OutBlock.offerho1);
                        int li_sh_bid = f_char_to_int(_H1__OutBlock.bidho1);

                        try
                        {
                            sd_jtong[ls_sh_cd].ask = li_sh_ask;  // 코스닥레버리지 ETF 를 신청했나?  왜 도착하지?
                            sd_jtong[ls_sh_cd].bid = li_sh_bid;
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                    }
                    break;

                case "S2_":  // 패킷 사이즈 아주 작음,  1호가만
                case "KS_":
                    { 
                        //cls_ebest.S2__OutBlock _S2__OutBlock = (cls_ebest.S2__OutBlock)Marshal.PtrToStructure(_real_msg_ptr_Data, typeof(cls_ebest.S2__OutBlock));

                        //string ls_sh_cd = new string(_S2__OutBlock.shcode);
                        //int li_sh_ask = f_char_to_int(_S2__OutBlock.offerho);
                        //int li_sh_bid = f_char_to_int(_S2__OutBlock.bidho);

                        //try
                        //{
                        //    sd_jtong[ls_sh_cd].ask = li_sh_ask;
                        //    sd_jtong[ls_sh_cd].bid = li_sh_bid;
                        //}
                        //catch (Exception)
                        //{
                        //    //throw;
                        //}
                    }
                    break;

                case "YS3":  // 종목 예상체결
                case "YK3":
                    {
                        cls_ebest.YS3_OutBlock _YS3_OutBlock = (cls_ebest.YS3_OutBlock)Marshal.PtrToStructure(_real_msg_ptr_Data, typeof(cls_ebest.YS3_OutBlock));

                        string ls_sh_cd = new string(_YS3_OutBlock.shcode);
                        int li_yprc = f_char_to_int(_YS3_OutBlock.yeprice);

                        try
                        {
                            sd_jtong[ls_sh_cd].prc = li_yprc;
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                    }
                    break;

                case "K1_":  //거래원
                case "OK_":
                    f_K1_(_real_msg_ptr_Data);
                    break;


                case "YJ_": //2초 지수 예상 , IJ_ 포맷이 틀림. 주의 아래가 더 복잡(시초가 등의 정보가 더 있음)
                    { 
                        cls_ebest.YJ__OutBlock _YJ_OutBlock = (cls_ebest.YJ__OutBlock)Marshal.PtrToStructure(_real_msg_ptr_Data, typeof(cls_ebest.YJ__OutBlock));

                        string sTime_packet = new string(_YJ_OutBlock.time);
                        string sJisu = new string(_YJ_OutBlock.jisu);
                        //string sSign = new string(_YJ_OutBlock.sign);
                        //string ls_change = new string(_YJ_OutBlock.change);
                        //string sDRate = new string(_YJ_OutBlock.drate);
                        string ls_upcode = new string(_YJ_OutBlock.upcode);

                        process_jisu_jtong_bulkupdate_2cho(sTime_packet , ls_upcode, sJisu);
                        //gb_하트비트 = true;  //True 가 초기화 값이 조심.

                    }
                    break;

                case "IJ_": //2초 지수 
                    { 
                        cls_ebest.IJ__OutBlock _IJ_OutBlock = (cls_ebest.IJ__OutBlock)Marshal.PtrToStructure(_real_msg_ptr_Data, typeof(cls_ebest.IJ__OutBlock));

                        string sTime_packet = new string(_IJ_OutBlock.time);
                        string sJisu = new string(_IJ_OutBlock.jisu);
                        //string sSign = new string(_IJ_OutBlock.sign);
                        //string ls_change = new string(_IJ_OutBlock.change);
                        //string sDRate = new string(_IJ_OutBlock.drate);
                        string ls_upcode = new string(_IJ_OutBlock.upcode);

                        process_jisu_jtong_bulkupdate_2cho(sTime_packet, ls_upcode, sJisu );
                        //gb_하트비트 = true;  //True 가 초기화 값이 조심.

                    }
                    break;

                default:
                    Console.WriteLine("real error : tr code 가 없는 경우 임: ");
                    break;
            }
        }

        void f_K1_(IntPtr _real_msg_ptr_Data)//거래원
        {

            //cl_ebst.real_msg _ebest_real_packet2 = (cl_ebst.real_msg)Marshal.PtrToStructure(lParam, typeof(cl_ebst.real_msg));
            //cl_ebst.K1__OutBlock _K1__OutBlock = (cl_ebst.K1__OutBlock)Marshal.PtrToStructure(_ebest_real_packet2.ptr_Data, typeof(cl_ebst.K1__OutBlock));

            cls_ebest.K1__OutBlock _K1__OutBlock = (cls_ebest.K1__OutBlock)Marshal.PtrToStructure(_real_msg_ptr_Data, typeof(cls_ebest.K1__OutBlock));

            string ms_sh_cd = new string(_K1__OutBlock.shcode);

            //Console.WriteLine("도착:" + ms_sh_cd);
            //f_log_form("도착:" + ls_sh_code_N);

            //
            string ls_offerno1 = new string(_K1__OutBlock.offerno1); //매도증권사코드1
            string ls_bidno1 = new string(_K1__OutBlock.bidno1); //매수증권사코드1
            string ls_offertrad1 = new string(_K1__OutBlock.offertrad1); //매도회원사명1
            string ls_bidtrad1 = new string(_K1__OutBlock.bidtrad1); //매수회원사명1

            int li_tradmdvol1 = Int32.Parse(new string(_K1__OutBlock.tradmdvol1)); //매도거래량1
            int li_tradmsvol1 = Int32.Parse(new string(_K1__OutBlock.tradmsvol1)); //매수거래량1
            double ld_tradmdrate1 = Double.Parse(new string(_K1__OutBlock.tradmdrate1)) / i100; //매도거래량비중1

            //f_log_form("double 확인1 : " + ld_tradmdrate1);
            //f_log_form("double 확인2 : " + f_char_to_double(_K1__OutBlock.tradmdrate1));



            double ld_tradmsrate1 = Double.Parse(new string(_K1__OutBlock.tradmsrate1)) / i100; //매수거래량비중1
            int li_tradmdcha1 = Int32.Parse(new string(_K1__OutBlock.tradmdcha1)); //매도거래량직전대비1
            int li_tradmscha1 = Int32.Parse(new string(_K1__OutBlock.tradmscha1)); //매수거래량직전대비1

            string ls_offerno2 = new string(_K1__OutBlock.offerno2); //매도증권사코드2
            string ls_bidno2 = new string(_K1__OutBlock.bidno2); //매수증권사코드2
            string ls_offertrad2 = new string(_K1__OutBlock.offertrad2); //매도회원사명2
            string ls_bidtrad2 = new string(_K1__OutBlock.bidtrad2); //매수회원사명2
            int li_tradmdvol2 = Int32.Parse(new string(_K1__OutBlock.tradmdvol2)); //매도거래량2
            int li_tradmsvol2 = Int32.Parse(new string(_K1__OutBlock.tradmsvol2)); //매수거래량2
            double ld_tradmdrate2 = Double.Parse(new string(_K1__OutBlock.tradmdrate2)) / i100; //매도거래량비중2
            double ld_tradmsrate2 = Double.Parse(new string(_K1__OutBlock.tradmsrate2)) / i100; //매수거래량비중2
            int li_tradmdcha2 = Int32.Parse(new string(_K1__OutBlock.tradmdcha2)); //매도거래량직전대비2
            int li_tradmscha2 = Int32.Parse(new string(_K1__OutBlock.tradmscha2)); //매수거래량직전대비2

            string ls_offerno3 = new string(_K1__OutBlock.offerno3); //매도증권사코드3
            string ls_bidno3 = new string(_K1__OutBlock.bidno3); //매수증권사코드3
            string ls_offertrad3 = new string(_K1__OutBlock.offertrad3); //매도회원사명3
            string ls_bidtrad3 = new string(_K1__OutBlock.bidtrad3); //매수회원사명3
            int li_tradmdvol3 = Int32.Parse(new string(_K1__OutBlock.tradmdvol3)); //매도거래량3
            int li_tradmsvol3 = Int32.Parse(new string(_K1__OutBlock.tradmsvol3)); //매수거래량3
            double ld_tradmdrate3 = Double.Parse(new string(_K1__OutBlock.tradmdrate3)) / i100; //매도거래량비중3
            double ld_tradmsrate3 = Double.Parse(new string(_K1__OutBlock.tradmsrate3)) / i100; //매수거래량비중3
            int li_tradmdcha3 = Int32.Parse(new string(_K1__OutBlock.tradmdcha3)); //매도거래량직전대비3
            int li_tradmscha3 = Int32.Parse(new string(_K1__OutBlock.tradmscha3)); //매수거래량직전대비3

            string ls_offerno4 = new string(_K1__OutBlock.offerno4); //매도증권사코드4
            string ls_bidno4 = new string(_K1__OutBlock.bidno4); //매수증권사코드4
            string ls_offertrad4 = new string(_K1__OutBlock.offertrad4); //매도회원사명4
            string ls_bidtrad4 = new string(_K1__OutBlock.bidtrad4); //매수회원사명4
            int li_tradmdvol4 = Int32.Parse(new string(_K1__OutBlock.tradmdvol4)); //매도거래량4
            int li_tradmsvol4 = Int32.Parse(new string(_K1__OutBlock.tradmsvol4)); //매수거래량4
            double ld_tradmdrate4 = Double.Parse(new string(_K1__OutBlock.tradmdrate4)) / i100; //매도거래량비중4
            double ld_tradmsrate4 = Double.Parse(new string(_K1__OutBlock.tradmsrate4)) / i100; //매수거래량비중4
            int li_tradmdcha4 = Int32.Parse(new string(_K1__OutBlock.tradmdcha4)); //매도거래량직전대비4
            int li_tradmscha4 = Int32.Parse(new string(_K1__OutBlock.tradmscha4)); //매수거래량직전대비4

            string ls_offerno5 = new string(_K1__OutBlock.offerno5); //매도증권사코드5
            string ls_bidno5 = new string(_K1__OutBlock.bidno5); //매수증권사코드5
            string ls_offertrad5 = new string(_K1__OutBlock.offertrad5); //매도회원사명5
            string ls_bidtrad5 = new string(_K1__OutBlock.bidtrad5); //매수회원사명5
            int li_tradmdvol5 = Int32.Parse(new string(_K1__OutBlock.tradmdvol5)); //매도거래량5
            int li_tradmsvol5 = Int32.Parse(new string(_K1__OutBlock.tradmsvol5)); //매수거래량5
            double ld_tradmdrate5 = Double.Parse(new string(_K1__OutBlock.tradmdrate5)) / i100; //매도거래량비중5
            double ld_tradmsrate5 = Double.Parse(new string(_K1__OutBlock.tradmsrate5)) / i100; //매수거래량비중5
            int li_tradmdcha5 = Int32.Parse(new string(_K1__OutBlock.tradmdcha5)); //매도거래량직전대비5
            int li_tradmscha5 = Int32.Parse(new string(_K1__OutBlock.tradmscha5)); //매수거래량직전대비5

            int li_ftradmdvol = Int32.Parse(new string(_K1__OutBlock.ftradmdvol)); //외국계증권사매도합계
            int li_ftradmsvol = Int32.Parse(new string(_K1__OutBlock.ftradmsvol)); //외국계증권사매수합계
            double ld_ftradmdrate = Double.Parse(new string(_K1__OutBlock.ftradmdrate)) / i100; //외국계증권사매도거래량비중
            double ld_ftradmsrate = Double.Parse(new string(_K1__OutBlock.ftradmsrate)) / i100; //외국계증권사매수거래량비중
            int li_ftradmdcha = Int32.Parse(new string(_K1__OutBlock.ftradmdcha)); //외국계증권사매도거래량직전대비
            int li_ftradmscha = Int32.Parse(new string(_K1__OutBlock.ftradmscha)); //외국계증권사매수거래량직전대비

         

            //1515 넣을 이유가 없지.. 
            //sd_jtong[ms_sh_cd].cd = ms_sh_cd;

            //sd_jtong[ms_sh_cd].offerno1 = ls_offerno1;
            //sd_jtong[ms_sh_cd].bidno1 = ls_bidno1;
            //sd_jtong[ms_sh_cd].offertrad1 = ls_offertrad1;
            //sd_jtong[ms_sh_cd].bidtrad1 = ls_bidtrad1;
            //sd_jtong[ms_sh_cd].tradmdvol1 = li_tradmdvol1;
            //sd_jtong[ms_sh_cd].tradmsvol1 = li_tradmsvol1;
            //sd_jtong[ms_sh_cd].tradmdrate1 = ld_tradmdrate1;
            //sd_jtong[ms_sh_cd].tradmsrate1 = ld_tradmsrate1;
            //sd_jtong[ms_sh_cd].tradmdcha1 = li_tradmdcha1;
            //sd_jtong[ms_sh_cd].tradmscha1 = li_tradmscha1;


            //sd_jtong[ms_sh_cd].offerno2 = ls_offerno2;
            //sd_jtong[ms_sh_cd].bidno2 = ls_bidno2;
            //sd_jtong[ms_sh_cd].offertrad2 = ls_offertrad2;
            //sd_jtong[ms_sh_cd].bidtrad2 = ls_bidtrad2;
            //sd_jtong[ms_sh_cd].tradmdvol2 = li_tradmdvol2;
            //sd_jtong[ms_sh_cd].tradmsvol2 = li_tradmsvol2;
            //sd_jtong[ms_sh_cd].tradmdrate2 = ld_tradmdrate2;
            //sd_jtong[ms_sh_cd].tradmsrate2 = ld_tradmsrate2;
            //sd_jtong[ms_sh_cd].tradmdcha2 = li_tradmdcha2;
            //sd_jtong[ms_sh_cd].tradmscha2 = li_tradmscha2;
            //sd_jtong[ms_sh_cd].offerno3 = ls_offerno3;
            //sd_jtong[ms_sh_cd].bidno3 = ls_bidno3;
            //sd_jtong[ms_sh_cd].offertrad3 = ls_offertrad3;
            //sd_jtong[ms_sh_cd].bidtrad3 = ls_bidtrad3;
            //sd_jtong[ms_sh_cd].tradmdvol3 = li_tradmdvol3;
            //sd_jtong[ms_sh_cd].tradmsvol3 = li_tradmsvol3;
            //sd_jtong[ms_sh_cd].tradmdrate3 = ld_tradmdrate3;
            //sd_jtong[ms_sh_cd].tradmsrate3 = ld_tradmsrate3;
            //sd_jtong[ms_sh_cd].tradmdcha3 = li_tradmdcha3;
            //sd_jtong[ms_sh_cd].tradmscha3 = li_tradmscha3;
            //sd_jtong[ms_sh_cd].offerno4 = ls_offerno4;
            //sd_jtong[ms_sh_cd].bidno4 = ls_bidno4;
            //sd_jtong[ms_sh_cd].offertrad4 = ls_offertrad4;
            //sd_jtong[ms_sh_cd].bidtrad4 = ls_bidtrad4;
            //sd_jtong[ms_sh_cd].tradmdvol4 = li_tradmdvol4;
            //sd_jtong[ms_sh_cd].tradmsvol4 = li_tradmsvol4;
            //sd_jtong[ms_sh_cd].tradmdrate4 = ld_tradmdrate4;
            //sd_jtong[ms_sh_cd].tradmsrate4 = ld_tradmsrate4;
            //sd_jtong[ms_sh_cd].tradmdcha4 = li_tradmdcha4;
            //sd_jtong[ms_sh_cd].tradmscha4 = li_tradmscha4;
            //sd_jtong[ms_sh_cd].offerno5 = ls_offerno5;
            //sd_jtong[ms_sh_cd].bidno5 = ls_bidno5;
            //sd_jtong[ms_sh_cd].offertrad5 = ls_offertrad5;
            //sd_jtong[ms_sh_cd].bidtrad5 = ls_bidtrad5;
            //sd_jtong[ms_sh_cd].tradmdvol5 = li_tradmdvol5;
            //sd_jtong[ms_sh_cd].tradmsvol5 = li_tradmsvol5;
            //sd_jtong[ms_sh_cd].tradmdrate5 = ld_tradmdrate5;
            //sd_jtong[ms_sh_cd].tradmsrate5 = ld_tradmsrate5;
            //sd_jtong[ms_sh_cd].tradmdcha5 = li_tradmdcha5;
            //sd_jtong[ms_sh_cd].tradmscha5 = li_tradmscha5;
            //sd_jtong[ms_sh_cd].ftradmdvol = li_ftradmdvol;
            //sd_jtong[ms_sh_cd].ftradmsvol = li_ftradmsvol;
            //sd_jtong[ms_sh_cd].ftradmdrate = ld_ftradmdrate;
            //sd_jtong[ms_sh_cd].ftradmsrate = ld_ftradmsrate;
            //sd_jtong[ms_sh_cd].ftradmdcha = li_ftradmdcha;
            //sd_jtong[ms_sh_cd].ftradmscha = li_ftradmscha;
    



            string ls_sql0;

            //string ls_hms = gs_hms;  //DateTime.Now.ToString("yyyyMMdd_HHmmss") 
            string ls_hms = DateTime.Now.ToString("HHmmss");  //DateTime.Now.ToString("yyyyMMdd_HHmmss") 

            int li_ask = sd_jtong[ms_sh_cd].ask;
            int li_bid = sd_jtong[ms_sh_cd].bid;


            string ls_tempname = fl_shnm(ms_sh_cd); //f_lookup_3c_to_name_gst
            ls_sql0 = "INSERT INTO A_TRD_SEC (trd_dt, stk_cd, stk_nm, hms,  ask, bid,    offerno1 ,bidno1, offertrad1, bidtrad1, tradmdvol1, tradmsvol1, tradmdrate1, tradmsrate1, tradmdcha1, tradmscha1,    offerno2 ,bidno2, offertrad2, bidtrad2, tradmdvol2, tradmsvol2, tradmdrate2, tradmsrate2, tradmdcha2, tradmscha2,    offerno3 ,bidno3, offertrad3, bidtrad3, tradmdvol3, tradmsvol3, tradmdrate3, tradmsrate3, tradmdcha3, tradmscha3,    offerno4 ,bidno4, offertrad4, bidtrad4, tradmdvol4, tradmsvol4, tradmdrate4, tradmsrate4, tradmdcha4, tradmscha4,    offerno5 ,bidno5, offertrad5, bidtrad5, tradmdvol5, tradmsvol5, tradmdrate5, tradmsrate5, tradmdcha5, tradmscha5,     ftradmdvol,ftradmsvol,ftradmdrate,ftradmsrate,ftradmdcha,ftradmscha ) VALUES ( '"

                + gs_today + "' ,'"

                + ms_sh_cd + "' ,'"
                + ls_tempname + "' ,'"
                + ls_hms + "' ,'"

                + li_ask + "' ,'"
                + li_bid + "' ,'"


                + ls_offerno1 + "' ,'"
                + ls_bidno1 + "' ,'"
                + ls_offertrad1 + "' ,'"
                + ls_bidtrad1 + "' ,'"
                + li_tradmdvol1 + "' ,'"
                + li_tradmsvol1 + "' ,'"
                + ld_tradmdrate1 + "' ,'"
                + ld_tradmsrate1 + "' ,'"
                + li_tradmdcha1 + "' ,'"
                + li_tradmscha1 + "' ,'"

                + ls_offerno2 + "' ,'"
                + ls_bidno2 + "' ,'"
                + ls_offertrad2 + "' ,'"
                + ls_bidtrad2 + "' ,'"
                + li_tradmdvol2 + "' ,'"
                + li_tradmsvol2 + "' ,'"
                + ld_tradmdrate2 + "' ,'"
                + ld_tradmsrate2 + "' ,'"
                + li_tradmdcha2 + "' ,'"
                + li_tradmscha2 + "' ,'"

                + ls_offerno3 + "' ,'"
                + ls_bidno3 + "' ,'"
                + ls_offertrad3 + "' ,'"
                + ls_bidtrad3 + "' ,'"
                + li_tradmdvol3 + "' ,'"
                + li_tradmsvol3 + "' ,'"
                + ld_tradmdrate3 + "' ,'"
                + ld_tradmsrate3 + "' ,'"
                + li_tradmdcha3 + "' ,'"
                + li_tradmscha3 + "' ,'"

                + ls_offerno4 + "' ,'"
                + ls_bidno4 + "' ,'"
                + ls_offertrad4 + "' ,'"
                + ls_bidtrad4 + "' ,'"
                + li_tradmdvol4 + "' ,'"
                + li_tradmsvol4 + "' ,'"
                + ld_tradmdrate4 + "' ,'"
                + ld_tradmsrate4 + "' ,'"
                + li_tradmdcha4 + "' ,'"
                + li_tradmscha4 + "' ,'"

                + ls_offerno5 + "' ,'"
                + ls_bidno5 + "' ,'"
                + ls_offertrad5 + "' ,'"
                + ls_bidtrad5 + "' ,'"
                + li_tradmdvol5 + "' ,'"
                + li_tradmsvol5 + "' ,'"
                + ld_tradmdrate5 + "' ,'"
                + ld_tradmsrate5 + "' ,'"
                + li_tradmdcha5 + "' ,'"
                + li_tradmscha5 + "' ,'"

                + li_ftradmdvol + "' ,'"
                + li_ftradmsvol + "' ,'"
                + ld_ftradmdrate + "' ,'"
                + ld_ftradmsrate + "' ,'"
                + li_ftradmdcha + "' ,'"
                + li_ftradmscha + "') ;";
            //MessageBox.Show(ls_s);


            string ls_sql1_sell = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 1 + "' ,"

                + "'" + ls_offerno1 + "' ,"
                + "'" + ls_offertrad1 + "' ,"
                + "'" + "S" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmdvol1 + "' ,"
                + "'" + ld_tradmdrate1 + "' ,"
                + "'" + li_tradmdcha1 + "'  "
                + " ) ;";

            string ls_sql1_buy = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 1 + "' ,"

                + "'" + ls_bidno1 + "' ,"
                + "'" + ls_bidtrad1 + "' ,"
                + "'" + "B" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmsvol1 + "' ,"
                + "'" + ld_tradmsrate1 + "' ,"
                + "'" + li_tradmscha1 + "'  "
                + " ) ;";


            string ls_sql2_sell = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 2 + "' ,"

                + "'" + ls_offerno2 + "' ,"
                + "'" + ls_offertrad2 + "' ,"
                + "'" + "S" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmdvol2 + "' ,"
                + "'" + ld_tradmdrate2 + "' ,"
                + "'" + li_tradmdcha2 + "'  "
                + " ) ;";

            string ls_sql2_buy = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 2 + "' ,"

                + "'" + ls_bidno2 + "' ,"
                + "'" + ls_bidtrad2 + "' ,"
                + "'" + "B" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmsvol2 + "' ,"
                + "'" + ld_tradmsrate2 + "' ,"
                + "'" + li_tradmscha2 + "'  "
                + " ) ;";






            string ls_sql3_sell = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 3 + "' ,"

                + "'" + ls_offerno3 + "' ,"
                + "'" + ls_offertrad3 + "' ,"
                + "'" + "S" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmdvol3 + "' ,"
                + "'" + ld_tradmdrate3 + "' ,"
                + "'" + li_tradmdcha3 + "'  "
                + " ) ;";

            string ls_sql3_buy = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 3 + "' ,"

                + "'" + ls_bidno3 + "' ,"
                + "'" + ls_bidtrad3 + "' ,"
                + "'" + "B" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmsvol3 + "' ,"
                + "'" + ld_tradmsrate3 + "' ,"
                + "'" + li_tradmscha3 + "'  "
                + " ) ;";


            string ls_sql4_sell = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 4 + "' ,"

                + "'" + ls_offerno4 + "' ,"
                + "'" + ls_offertrad4 + "' ,"
                + "'" + "S" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmdvol4 + "' ,"
                + "'" + ld_tradmdrate4 + "' ,"
                + "'" + li_tradmdcha4 + "'  "
                + " ) ;";

            string ls_sql4_buy = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 4 + "' ,"

                + "'" + ls_bidno4 + "' ,"
                + "'" + ls_bidtrad4 + "' ,"
                + "'" + "B" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmsvol4 + "' ,"
                + "'" + ld_tradmsrate4 + "' ,"
                + "'" + li_tradmscha4 + "'  "
                + " ) ;";





            string ls_sql5_sell = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 5 + "' ,"

                + "'" + ls_offerno5 + "' ,"
                + "'" + ls_offertrad5 + "' ,"
                + "'" + "S" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmdvol5 + "' ,"
                + "'" + ld_tradmdrate5 + "' ,"
                + "'" + li_tradmdcha5 + "'  "
                + " ) ;";

            string ls_sql5_buy = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 5 + "' ,"

                + "'" + ls_bidno5 + "' ,"
                + "'" + ls_bidtrad5 + "' ,"
                + "'" + "B" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_tradmsvol5 + "' ,"
                + "'" + ld_tradmsrate5 + "' ,"
                + "'" + li_tradmscha5 + "'  "
                + " ) ;";




            string ls_sql9_sell = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 9 + "' ,"

                + "'" + "frn" + "' ,"
                + "'" + "외국합" + "' ,"
                + "'" + "S" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_ftradmdvol + "' ,"
                + "'" + ld_ftradmdrate + "' ,"
                + "'" + li_ftradmdcha + "'  "
                + " ) ;";

            string ls_sql9_buy = "INSERT INTO A_TRD_SEC2 (trd_dt, stk_cd, stk_nm, hms, rnk, sec_cd , sec_nm, sb, ask, bid, vol, rate, vol_cha) VALUES ( "

                + "'" + gs_today + "' ,"
                + "'" + ms_sh_cd + "' ,"
                + "'" + fl_shnm(ms_sh_cd) + "' ,"
                + "'" + ls_hms + "' ,"
                + "'" + 9 + "' ,"

                + "'" + "frn" + "' ,"
                + "'" + "외국합" + "' ,"
                + "'" + "B" + "' ,"

                + "'" + li_ask + "' ,"
                + "'" + li_bid + "' ,"
                + "'" + li_ftradmsvol + "' ,"
                + "'" + ld_ftradmsrate + "' ,"
                + "'" + li_ftradmscha + "'  "
                + " ) ;";

            using (SqlConnection _conn = new SqlConnection(gs_sql_server_info))  //3 of 5
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = ls_sql0;
                cmd.ExecuteNonQuery();

                cmd.CommandText = ls_sql1_sell;
                cmd.ExecuteNonQuery();
                cmd.CommandText = ls_sql1_buy;
                cmd.ExecuteNonQuery();

                cmd.CommandText = ls_sql2_sell;
                cmd.ExecuteNonQuery();
                cmd.CommandText = ls_sql2_buy;
                cmd.ExecuteNonQuery();

                cmd.CommandText = ls_sql3_sell;
                cmd.ExecuteNonQuery();
                cmd.CommandText = ls_sql3_buy;
                cmd.ExecuteNonQuery();

                cmd.CommandText = ls_sql4_sell;
                cmd.ExecuteNonQuery();
                cmd.CommandText = ls_sql4_buy;
                cmd.ExecuteNonQuery();

                cmd.CommandText = ls_sql5_sell;
                cmd.ExecuteNonQuery();
                cmd.CommandText = ls_sql5_buy;
                cmd.ExecuteNonQuery();

                cmd.CommandText = ls_sql9_sell;
                cmd.ExecuteNonQuery();
                cmd.CommandText = ls_sql9_buy;
                cmd.ExecuteNonQuery();
            }
        }



        ////1)cls_monitor_구축 2) advisereal 3) insert_from_clsmonitor_to_db
        //void f_msg_t8430_주식전종목(cls_ebest.request_packet msg_rqst)
        //{
        //    cls_ebest.t8430_OutBlock _t8430OutBlock = new cls_ebest.t8430_OutBlock();

        //    int _size = Marshal.SizeOf(_t8430OutBlock); // 패킷의 크기
        //    int li_cnt = msg_rqst.DataLength / _size; // 전체 패킷의 길이를 패킷 하나의 크기로 나누어 수신한 종목들의 수를 계산
        //    int _position;

        //    string ls_hname;
        //    string ls_shcode;
        //    int li_jnilclose;
        //    int li_recprice;
        //    int li_etfgubun;
        //    int li_ks_kq_gubun;

        //    for (int i = 0; i < li_cnt; i++)
        //    {
        //        _position = (int)msg_rqst.Data + _size * i; // 전체 패킷에서 각 패킷의 데이터가 시작하는 위치 계산
        //        _t8430OutBlock = (cls_ebest.t8430_OutBlock)Marshal.PtrToStructure((IntPtr)_position, typeof(cls_ebest.t8430_OutBlock));

        //        ls_shcode = new string(_t8430OutBlock.shcode);  //6
        //        ls_hname = f_string_cleansing (new string(_t8430OutBlock.hname)); //20

        //        li_etfgubun = Int32.Parse(new string(_t8430OutBlock.etfgubun)); //1:etf
        //        li_ks_kq_gubun = Int32.Parse(new string(_t8430OutBlock.gubun));  //1:코스피, 2:코스닥

        //        if (li_etfgubun != 1)
        //        {
        //            {
        //                sd_jtong[ls_shcode] = new cl_jtong(ls_shcode, ls_hname, 0, 0, 0, 1);
        //            }

        //            if (li_ks_kq_gubun == 1)
        //            {
        //                //cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "S2_", ls_shcode, 6); //KOSPI 우선호가, 작은 패킷 사이즈 1호가만
        //                cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "S3_", ls_shcode, 6); //KOSPI체결
        //                cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "H1_", ls_shcode, 6); //KOSPI호가잔량, 358, 전체호가및잔량
        //                cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YS3", ls_shcode, 6); //KOSPI 예상체결
        //            }
        //            else if (li_ks_kq_gubun == 2)
        //            {
        //                //cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "KS_", ls_shcode, 6); //코스닥 우선호가, 작은 패킷 사이즈 1호가만
        //                cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "K3_", ls_shcode, 6); //코스닥체결 
        //                cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "HA_", ls_shcode, 6); //코스닥 호가잔량, 358, 전체호가및잔량
        //                cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YK3", ls_shcode, 6); //코스닥 예상체결
        //            }
        //            else
        //            {
        //                f_log("error");
        //            }
        //        }
        //    }
        //    f_init_2_after_t8430();
        //}


        //1)cls_monitor_구축 2) advisereal 3) insert_from_clsmonitor_to_db
        void f_msg_t8436_주식전종목(cls_ebest.request_packet msg_rqst) //아주아주주의 : 8436은 더미칼럼 필요 없음
        {
            cls_ebest.t8436_OutBlock _t8436OutBlock = new cls_ebest.t8436_OutBlock();

            int _size = Marshal.SizeOf(_t8436OutBlock); // 패킷의 크기
            int li_cnt = msg_rqst.DataLength / _size; // 전체 패킷의 길이를 패킷 하나의 크기로 나누어 수신한 종목들의 수를 계산
            int _position;

            string ls_hname;
            string ls_shcode;
            int li_etfgubun;
            int li_ks_kq_gubun;
            string ls_spec_gubun;


            for (int i = 0; i < li_cnt; i++)
            {
                _position = (int)msg_rqst.Data + _size * i; // 전체 패킷에서 각 패킷의 데이터가 시작하는 위치 계산
                _t8436OutBlock = (cls_ebest.t8436_OutBlock)Marshal.PtrToStructure((IntPtr)_position, typeof(cls_ebest.t8436_OutBlock));

                ls_shcode = new string(_t8436OutBlock.shcode);  //6
                ls_hname = f_string_cleansing(new string(_t8436OutBlock.hname)); //20, 여기서 명이 짤리는 구나 

                li_etfgubun = Int32.Parse(new string(_t8436OutBlock.etfgubun)); //1:etf  2:ETN
                li_ks_kq_gubun = Int32.Parse(new string(_t8436OutBlock.gubun));  //1:코스피, 2:코스닥

                ls_spec_gubun = f_string_cleansing( new string(_t8436OutBlock.spac_gubun));  //

                if (li_etfgubun == 0 && ls_spec_gubun == "N")  //Y:스펙
                {
                    //f_log("li_etfgubun 드디어 : " );
                    try
                    {
                        sd_jtong[ls_shcode] = new cl_jtong(ls_shcode, ls_hname, 0, 0, 0, 1);
                    }
                    catch (Exception)
                    {
                        f_log("이미 종목코드가 있음"); //throw;
                    }

                       

                    if (li_ks_kq_gubun == 1)
                    {
                        //cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "S2_", ls_shcode, 6); //KOSPI 우선호가, 작은 패킷 사이즈 1호가만
                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "S3_", ls_shcode, 6); //KOSPI체결
                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "H1_", ls_shcode, 6); //KOSPI호가잔량, 358, 전체호가및잔량
                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YS3", ls_shcode, 6); //KOSPI 예상체결

                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "K1_", ls_shcode, 6);  //거래원

                    }
                    else if (li_ks_kq_gubun == 2)
                    {
                        //cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "KS_", ls_shcode, 6); //코스닥 우선호가, 작은 패킷 사이즈 1호가만
                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "K3_", ls_shcode, 6); //코스닥체결 
                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "HA_", ls_shcode, 6); //코스닥 호가잔량, 358, 전체호가및잔량
                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YK3", ls_shcode, 6); //코스닥 예상체결

                        cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "OK_", ls_shcode, 6);  //거래원

                    }
                    else
                    {
                        f_log("error");
                    }
                }

                sd_shcd_nm[ls_shcode] = ls_hname;
            }
            f_init_2_after_t8436();
        }



        void f_msg_t8407(cls_ebest.request_packet msg_rqst)
        {
            cls_ebest.t8407_OutBlock1 _t8407OutBlock = new cls_ebest.t8407_OutBlock1();

            int li_size244 = Marshal.SizeOf(_t8407OutBlock); // 패킷의 크기

            //f_log("li_size " + li_size244);
            int li_js_JONGMOK_CNT = msg_rqst.DataLength / li_size244; // 전체 패킷의 길이를 패킷 하나의 크기로 나누어 수신한 종목들의 수를 계산

            int _Position;

            string ls_hname;
            string ls_shcode;
            int li_prc;

            string ls_make_file_one_line;

            //f_log("li_js_JONGMOK_CNT" + li_js_JONGMOK_CNT);

            for (int i = 0; i < li_js_JONGMOK_CNT; i++)
            {
                _Position = (int)msg_rqst.Data + li_size244 * i; // 전체 패킷에서 각 패킷의 데이터가 시작하는 위치 계산
                _t8407OutBlock = (cls_ebest.t8407_OutBlock1)Marshal.PtrToStructure((IntPtr)_Position, typeof(cls_ebest.t8407_OutBlock1));

                ls_shcode = new string(_t8407OutBlock.shcode);  //6
                ls_hname = f_string_cleansing(new string(_t8407OutBlock.hname)); //20

                try
                {
                    li_prc = Int32.Parse(new string(_t8407OutBlock.price));
                }
                catch (Exception)
                {
                    li_prc = 0;
                }


                //ls_hname = f_stk_nm (ls_shcode );


                f_log(i.ToString() +  ls_hname);

                sd_jtong[ls_shcode] = new cl_jtong(ls_shcode, ls_shcode, li_prc, li_prc, li_prc, 1);
            }
            //1313
            f_log("50종목 멀티 현재가 t8407 조회 중");
            f_insert_temp_hoga_update_hoga();
        }


        void f_msg_t1511(cls_ebest.request_packet msg_rqst)
        {
            f_log("업종도착 2 ");

            cls_ebest.t1511_OutBlock _t1511_OutBlock = (cls_ebest.t1511_OutBlock)Marshal.PtrToStructure(msg_rqst.Data, typeof(cls_ebest.t1511_OutBlock));

            string  ls_hname =  new string(_t1511_OutBlock.hname);
            string ls_hname_trim = ls_hname.Trim();

            //f_log("ls_hname:" + ls_hname_trim);
            if (ls_hname_trim == "KOSPI200")
            {
                //f_log("if 문 안 쪽 ls_hname:" + ls_hname_trim);

                {
                    double ldbl_pricejisu = double.Parse(new string(_t1511_OutBlock.pricejisu));
                    //int li_jisu_200 = (int)(ldbl_pricejisu * 100);
                    int li_jisu_200 = (int)(ldbl_pricejisu * 1);
                    string ls_jisu_200 = li_jisu_200.ToString();

                    process_jisu_jtong_bulkupdate_2cho("235959", "101", ls_jisu_200);
                }

                {
                    double ldbl_firstjisu = double.Parse(new string(_t1511_OutBlock.firstjisu));
                    //int li_jisu_kospi = (int)(ldbl_firstjisu * 100);
                    int li_jisu_kospi = (int)(ldbl_firstjisu * 1);
                    string ls_bh_jisu_kospi = li_jisu_kospi.ToString();

                    process_jisu_jtong_bulkupdate_2cho("235959", "001", ls_bh_jisu_kospi);
                }
            }
        }


        //void f_init_2_after_t8430()
        //{
        //    {
        //        sd_jtong["IKS200"] = new cl_jtong("IKS200", "IKS200", 0, 0, 0, 100);
        //        sd_jtong["IKS001"] = new cl_jtong("IKS001", "IKS001", 0, 0, 0, 100);
        //    }
        //    f_once_insert_hoga();

        //    cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YJ_", "001", 3);             // jtong에 IKS200를 먼저 넣고 real요쳥해야 햠
        //    cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "IJ_", "001", 3);
        //    cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YJ_", "101", 3);
        //    cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "IJ_", "101", 3);

        //    f_log("(3) t8430메시지 처리됨");
        //    Thread.Sleep(999);

        //    f_timer_event_handler_4cho();
        //    f_log("(4) 타이머도 실행(4초)");

        //    var go_handle = GetConsoleWindow();
        //    ShowWindow(go_handle, gb_win_show);  // 0 hide

        //    f_log("(5) 콘솔창도 숨김. 끝.");
        //    f_t0167_request();  //11초와 차이나지 않게끔 하기 위해서, init 제일 마지막에 그냥 한 번 실행해 봄
        //}


        void f_init_2_after_t8436()
        {
            {
                sd_jtong["IKS200"] = new cl_jtong("IKS200", "IKS200", 0, 0, 0, 100);
                sd_jtong["IKS001"] = new cl_jtong("IKS001", "IKS001", 0, 0, 0, 100);
            }
            f_once_insert_hoga();

            cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YJ_", "001", 3);             // jtong에 IKS200를 먼저 넣고 real요쳥해야 햠
            cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "IJ_", "001", 3);
            cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "YJ_", "101", 3);
            cls_ebest.ETK_AdviseRealData(g_MainWindowHandle, "IJ_", "101", 3);

            f_log("(3) t8430메시지 처리됨");
            Thread.Sleep(999);

            f_timer_event_handler_4cho();
            f_log("(4) 타이머도 실행(4초)");

            var go_handle = GetConsoleWindow();
            ShowWindow(go_handle, gb_win_show);  // 0 hide

            f_log("(5) 콘솔창도 숨김. 끝.");
            f_t0167_request();  //11초와 차이나지 않게끔 하기 위해서, init 제일 마지막에 그냥 한 번 실행해 봄
        }



        void process_jisu_jtong_bulkupdate_2cho(string as_time_packet, string as_upcode,  string sJisu_prc ) //라. process 지수. 다는 없음
        {
            //1414

            if (as_time_packet == "EXTJJJ")
            {
                cb_relogin.Checked = false;

            }else if (Int32.Parse(as_time_packet) > 151950)
            {
                cb_relogin.Checked = false;
            }

            if (as_upcode == "101")
            {
                sd_jtong["IKS200"] = new cl_jtong("IKS200", "IKS200", Int32.Parse(sJisu_prc), Int32.Parse(sJisu_prc), Int32.Parse(sJisu_prc), 100);
            }
            else
            {
                sd_jtong["IKS001"] = new cl_jtong("IKS001", "IKS001", Int32.Parse(sJisu_prc), Int32.Parse(sJisu_prc), Int32.Parse(sJisu_prc), 100);
            }
            f_insert_temp_hoga_update_hoga();

            string ls_date = DateTime.Now.ToString("yyyyMMdd");
            //as_time = DateTime.Now.ToString("HHmmss");

            gdatetime_recv_time_interval_IMPO_2cho = f_string_to_time (ls_date, as_time_packet);
            gb_하트비트 = true;  //True 가 초기화 값이 조심.
            f_log("업시간: " +  as_time_packet + " " + as_upcode + " " + double.Parse(sJisu_prc) / 100);
        }

        void f_t0167_request()  // 시간
        {
            IntPtr _ptr = Marshal.StringToHGlobalAnsi("00000000");
            cls_ebest.ETK_Request(g_MainWindowHandle, "t0167", _ptr, 8, false, "", -1);
            Marshal.FreeHGlobal(_ptr);
        }

        void f_t1101_request()  // 현재가 이미 검즘
        {
            IntPtr intptr_input = Marshal.StringToHGlobalAnsi("005930");
            cls_ebest.ETK_Request(g_MainWindowHandle, "t1101", intptr_input, 6, false, "", -1);
            Marshal.FreeHGlobal(intptr_input);
        }

        private void f_once_insert_hoga()
        {
            DataTable dt_jtong = sd_jtong.Values.ToArray().CopyToDataTable() ;

            using (SqlConnection _conn = new SqlConnection(gs_sql_server_info))
            {
                using (SqlCommand _command = new SqlCommand("DELETE FROM hoga", _conn))
                {
                    _conn.Open();
                    _command.ExecuteNonQuery();

                    //SqlBulkCopy bulkCopy = new SqlBulkCopy(_conn, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);

                    using (SqlBulkCopy _bulkcopy = new SqlBulkCopy(_conn))
                    {
                        _bulkcopy.DestinationTableName = "hoga";
                        _bulkcopy.WriteToServer(dt_jtong);
                        _bulkcopy.Close();
                    }
                    _conn.Close();
                }
            }
        }

        
        private void f_insert_temp_hoga_update_hoga()
        {
            //dictionary의 value를 쿼리를 이용해서 table로 바꾸고, table을 bulkcopy하는 구나
            //re-login 해서 호가가 0인 것은 빼고 처리함
            var larr_jtong_values = sd_jtong.Values.ToArray();

            var lt_query_jtong = from _jtong in larr_jtong_values
                         where _jtong.ask > 9 && _jtong.bid > 9
                         orderby _jtong.cd
                         select new {
                             code = _jtong.cd,
                             name = _jtong.nm,
                             ask = _jtong.ask,
                             bid = _jtong.bid,
                             prc = _jtong.prc,
                             multi = _jtong.multi };
            DataTable dt_jtong = lt_query_jtong.CopyToDataTable();

            //dataGridView1.DataSource = dt_jtong;

            using (SqlConnection _conn = new SqlConnection(gs_sql_server_info))
            {
                using (SqlCommand _command = new SqlCommand("DELETE FROM temp_hoga", _conn))
                {
                    _conn.Open();
                    _command.ExecuteNonQuery();
                    using (SqlBulkCopy _bulkcopy = new SqlBulkCopy(_conn))
                    {
                        _bulkcopy.DestinationTableName = "temp_hoga";
                        _bulkcopy.WriteToServer(dt_jtong); 
                        _bulkcopy.Close();
                    }
                    _command.CommandText = "UPDATE H SET H.ask=T.ask,  H.bid=T.bid,  H.prc=T.prc   FROM  hoga AS H   INNER JOIN  temp_hoga AS T ON H.code = T.code;";
                    _command.ExecuteNonQuery();
                    _conn.Close();
                }
            }
        }

        void f_timer_event_handler_4cho()
        {
            {
                //go_timer_t0167.Tick += new EventHandler(f_timer_tick_4cho);
                //go_timer_t0167.Interval = 4000;
                gt_timer_4cho.Start();  //4초는 헤더에서 선언했음. 여기서 선언해도 될 듯 싶으나, 이유가 있었겠지?
                f_log("타이머 시작");
            }
        }

        void f_timer_tick_4cho(object sender, EventArgs e)
        {
            DateTime lt_now = Convert.ToDateTime(DateTime.Now);
            TimeSpan lt_diff = lt_now - gdatetime_recv_time_interval_IMPO_2cho;
            int li_diff_seconds = lt_diff.Seconds;

            if (cb_relogin.Checked)
            {
                //f_log("서버:" + gdatetime_recv_time_interval_IMPO_2cho.ToString("HHmmss") + " 차이: " + li_diff_seconds.ToString() + "초 ");
                f_log("차이 : " + li_diff_seconds.ToString() + "초 ");

                if (li_diff_seconds > gi_DIFF_SECONd_GIJUN_15CHO)
                {
                    //if (!gb_하트비트)
                    //f_log("재시작전 " + gb_하트비트.ToString());
                    if (gb_하트비트)
                    {
                        gt_timer_4cho.Stop();
                        //sd_jtong.Clear();
                        f_button_login_Click();

                        gb_하트비트 = false; // 로그인 실행 후 하트비트 바꾸어야 함. 위쪽에 넣었다고 오류 발생했음

                        //f_log("재시작 후 " + gb_하트비트.ToString());
                        Thread.Sleep(2000);  // 오류를 못 잡아 그냥 넣어 봄  , 2초마다 확인하니, 쉬어야 할 듯
                    }
                    //gb_하트비트 = true;
                }
            }
            f_t0167_request(); //
        }

        // 간단한 버튼틀릭 함수  ///////////////////////////////////
        private void button_login_Click(object sender, EventArgs e)
        {
            f_button_login_Click();
        }

        private void button_hide_console_Click(object sender, EventArgs e)
        {
            f_win_show_hide();
        }

        private void button_t1101_Click(object sender, EventArgs e)
        {
            f_t1101_request();
        }

        private void button_t0167_Click(object sender, EventArgs e)
        {
            f_t0167_request();
        }

        private void button_timer_Click(object sender, EventArgs e)
        {
            f_timer_event_handler_4cho();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.Sleep(999);
            f_button_login_Click();
        }

       private void btn_t8407_Click(object sender, EventArgs e)
        {
            Task.Run( () => fth_t8407_request() );
        }

        //위에 위치시켜야 하나, 익숙하지 않으니, 잠시 여기에 위치시킴
        void fth_t8407_request()
        {
            string ls_cum_only_stk = "";
            for (int index = 0; index < sd_jtong.Count; index++)
            {
                var item = sd_jtong.ElementAt(index);
                string fs_item_key = item.Key;

                //if (fs_item_key.Substring(0,1) == "I")  //IKS200과 KOSPI가 있어서 제외해 줘야 함. 그렇지 않으면 2개 더 블랭크가 조회되서 에러유발
                //    ls_cum_only_stk = ls_cum_only_stk ;
                //else
                //    ls_cum_only_stk = ls_cum_only_stk + fs_item_key;


                if (Regex.IsMatch(fs_item_key.Substring(0,1), "[a-zA-Z]"))  // 00088K 종목 들도 참 많네.. IKS001 IKS200
                {
                    ls_cum_only_stk = ls_cum_only_stk + "";
                    f_log("fs_item_key 어떤 문자인지 확인 : " + fs_item_key);
                }
                else
                {
                    ls_cum_only_stk = ls_cum_only_stk + fs_item_key;
                }



            }



           





            decimal ldbl_total_len = ls_cum_only_stk.Length;  //int로 하면 에러 발생함. dbl로 해야 함
            int _size300 = 6 * 50;
            int li_cnt = (int)Math.Ceiling((decimal)(ldbl_total_len / _size300));

            int remainder = ((int)ldbl_total_len) % _size300;  // 나머지
            int remainder_cnt = remainder / 6;  // 나머지
            string ls_remainder_cnt = remainder_cnt.ToString().PadLeft(3, '0');




            int _Position;

            string ls_cum_fill = ls_cum_only_stk.PadRight(li_cnt * _size300, '0');

            string ls_substring;

            //141414
            //li_cnt = 1;
            for (int i = 0; i < li_cnt; i++)
            {
                _Position = _size300 * i; // 전체 패킷에서 각 패킷의 데이터가 시작하는 위치 계산
                ls_substring = ls_cum_fill.Substring(_Position, _size300);

                //f_log("input" + i.ToString() + " : " + ls_substring);

                IntPtr intptr_input;
                if (i == (li_cnt - 1))
                {
                    intptr_input = Marshal.StringToHGlobalAnsi(ls_remainder_cnt + " " + ls_substring); // 1종목만 call 향후 바꾸기
                    f_log("마지막조회" + ls_remainder_cnt + ":" + ls_substring);

                }
                else
                {
                    intptr_input = Marshal.StringToHGlobalAnsi("050 " + ls_substring);
                }

                cls_ebest.ETK_Request(g_MainWindowHandle, "t8407", intptr_input, 303+2, false, "", -1);

                Marshal.FreeHGlobal(intptr_input);
                Thread.Sleep(1100);
            }

            IntPtr _ptr2 = Marshal.StringToHGlobalAnsi("101"); //K200 , 코스피 조회해서 처리할려고 했더니, 명이 지저분해서 포기함
            cls_ebest.ETK_Request(g_MainWindowHandle, "t1511", _ptr2, 3, false, "", -1);
            Marshal.FreeHGlobal(_ptr2);
            Thread.Sleep(1100);
        }


        // 안 중요 함수들 /////////////////////////////////////////////////////////////
        public void f_log(string as_log)
        {
            Invoke((MethodInvoker)delegate
            {
                tb_log.AppendText(as_log + Environment.NewLine);
            });
        }

        //public void f_log_tcp_error(string as_log)
        //{
        //    Invoke((MethodInvoker)delegate
        //    {
        //        tb_shutdown.AppendText(as_log + Environment.NewLine);
        //    });
        //}


        void f_win_show_hide()
        {
            var go_handle = GetConsoleWindow();
            if (gb_win_show == 1)
                gb_win_show = 0;
            else
                gb_win_show = 1;

            ShowWindow(go_handle, gb_win_show);
        }

        static int f_char_to_int(char[] a_chars)
        {
            int li_ret = 0;
            //s_x[0] = '7';
            //for (int i = 0; i < 6; i++)
            for (int i = 0; i < a_chars.Length; i++)
            {
                //y = y * 10 + (s_x[i] - '0');  //& 0x0f
                li_ret = li_ret * 10 + (a_chars[i] - 48);  //& 0x0f
                //y = y * 10 + (s_x[i] & 0x0f);  //& 0x0f
                //y = y * 10 + (int)a_chars[i];  //& 0x0f <== 에러 발생??
            }
            return li_ret;
        }

        static string f_string_cleansing(string ls_hname)
        {
            string ls_ret = (ls_hname.Replace("\0", string.Empty)).Trim();
            return ls_ret;
        }


        static string  fl_shnm(string ls_shcode)  //향후 이것을 삭제하기 , 아래 사용
        {
            string ls_hname;
            try
            {
                ls_hname = sd_shcd_nm[ls_shcode];
            }
            catch (Exception)
            {
                ls_hname = ls_shcode; //throw;
            }

            return ls_hname;
        }

        static string f_stk_nm(string ls_shcode)
        {
            string ls_hname;
            try
            {
                ls_hname = sd_shcd_nm[ls_shcode];
            }
            catch (Exception)
            {
                ls_hname = ls_shcode; //throw;
            }

            return ls_hname;
        }






static DateTime f_string_to_time(string ls_date, string ls_time)
        {
            string ls_date_time_format;

            ls_date_time_format = ls_date.Substring(0, 4) + "-" + ls_date.Substring(4, 2) + "-" + ls_date.Substring(6, 2) + " " +
                           ls_time.Substring(0, 2) + ":" + ls_time.Substring(2, 2) + ":" + ls_time.Substring(4, 2) + " ";

            DateTime ltime_temp  = Convert.ToDateTime(ls_date_time_format);

            return ltime_temp;
        }
 
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sd_jtong.Values.ToArray();
        }
    }
}
