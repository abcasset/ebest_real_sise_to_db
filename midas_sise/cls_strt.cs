using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midas_sise
{
    public class cls_packet
    {
        public cls_ebest.real_msg packet { get; set; }
        public cls_packet(cls_ebest.real_msg a_packet)
        {
            this.packet = a_packet;
        }
    }

    public class cls_string
    {
        public string ls_temp { get; set; }
        public cls_string(string a_packet)
        {
            this.ls_temp = a_packet;
        }
    }





    //8c, 3c 같이 중복해서 사용 주의
    public class sfcd_shcd_nm
    {
        public string sf_code { get; set; }
        public string sh_code { get; set; }
        public string name { get; set; }
        public int rnk { get; set; }


        public sfcd_shcd_nm(string _sf_code, string _sh_code, string _hname, int _rnk)
        {
            this.sf_code = _sf_code;
            this.sh_code = _sh_code;
            this.name = _hname;
            this.rnk = _rnk;
        }

    }

    //8c, 3c 같이 중복해서 사용 주의
    public class shcd_sf3c_nm
    {
        public string sh_code { get; set; }
        public string sf3c_code { get; set; }
        public string hname { get; set; }

        public shcd_sf3c_nm(string _sh_code, string _sf3c_code, string _hname)
        {
            this.sh_code = _sh_code;
            this.sf3c_code = _sf3c_code;
            this.hname = _hname;
        }
    }

    public class cl_jtong
    {
        public string cd { get; set; }
        public string nm { get; set; }
        public int ask { get; set; }
        public int bid { get; set; }
        public int prc { get; set; }

        public int multi { get; set; }


        public cl_jtong(string _sh_code, string _sh_name,  
                        int _ask,    int _bid,    int _prc,  
                        int _multi)
        {
            this.cd = _sh_code;
            this.nm = _sh_name;

            this.ask = _ask;
            this.bid = _bid;
            this.prc = _prc;


            this.multi = _multi;
        }

    }


    public class cl_ytong
    {
        public string cd { get; set; }
        public string nm { get; set; }
        public int prc { get; set; }

        public cl_ytong(string _sh_code,string  _nm, int _prc)
        {
            this.cd = _sh_code;
            this.nm = _nm;
            this.prc = _prc;


        }

    }




}
