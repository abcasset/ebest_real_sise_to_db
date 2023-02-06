using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class ObjectShredder<T>
{
    private System.Reflection.FieldInfo[] _fi;
    private System.Reflection.PropertyInfo[] _pi;
    private System.Collections.Generic.Dictionary<string, int> _ordinalMap;
    private System.Type _type;

    // ObjectShredder constructor.
    public ObjectShredder()
    {
        _type = typeof(T);
        _fi = _type.GetFields();
        _pi = _type.GetProperties();
        _ordinalMap = new Dictionary<string, int>();
    }

    /// <summary>
    /// Loads a DataTable from a sequence of objects.
    /// </summary>
    /// <param name="source">The sequence of objects to load into the DataTable.</param>
    /// <param name="table">The input table. The schema of the table must match that
    /// the type T.  If the table is null, a new table is created with a schema
    /// created from the public properties and fields of the type T.</param>
    /// <param name="options">Specifies how values from the source sequence will be applied to
    /// existing rows in the table.</param>
    /// <returns>A DataTable created from the source sequence.</returns>
    public DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
    {
        // Load the table from the scalar sequence if T is a primitive type.
        if (typeof(T).IsPrimitive)
        {
            return ShredPrimitive(source, table, options);
        }

        // Create a new table if the input table is null.
        table = new DataTable(typeof(T).Name);

        // Initialize the ordinal map and extend the table schema based on type T.
        table = ExtendTable(table, typeof(T));

        // Enumerate the source sequence and load the object values into rows.
        table.BeginLoadData();
        using (IEnumerator<T> e = source.GetEnumerator())
        {
            while (e.MoveNext())
            {
                if (options != null)
                {
                    table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                }
                else
                {
                    table.LoadDataRow(ShredObject(table, e.Current), true);
                }
            }
        }
        table.EndLoadData();

        // Return the table.
        return table;
    }

    public DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
    {
        // Create a new table if the input table is null.
        table = new DataTable(typeof(T).Name);

        if (!table.Columns.Contains("Value"))
        {
            table.Columns.Add("Value", typeof(T));
        }

        // Enumerate the source sequence and load the scalar values into rows.
        table.BeginLoadData();
        using (IEnumerator<T> e = source.GetEnumerator())
        {
            Object[] values = new object[table.Columns.Count];
            while (e.MoveNext())
            {
                values[table.Columns["Value"].Ordinal] = e.Current;

                if (options != null)
                {
                    table.LoadDataRow(values, (LoadOption)options);
                }
                else
                {
                    table.LoadDataRow(values, true);
                }
            }
        }
        table.EndLoadData();

        // Return the table.
        return table;
    }

    public object[] ShredObject(DataTable table, T instance)
    {

        FieldInfo[] fi = _fi;
        PropertyInfo[] pi = _pi;

        if (instance.GetType() != typeof(T))
        {
            // If the instance is derived from T, extend the table schema
            // and get the properties and fields.
            ExtendTable(table, instance.GetType());
            fi = instance.GetType().GetFields();
            pi = instance.GetType().GetProperties();
        }

        // Add the property and field values of the instance to an array.
        Object[] values = new object[table.Columns.Count];
        foreach (FieldInfo f in fi)
        {
            values[_ordinalMap[f.Name]] = f.GetValue(instance);
        }

        foreach (PropertyInfo p in pi)
        {
            values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
        }

        // Return the property and field values of the instance.
        return values;
    }

    public DataTable ExtendTable(DataTable table, Type type)
    {
        // Extend the table schema if the input table was null or if the value
        // in the sequence is derived from type T.
        foreach (FieldInfo f in type.GetFields())
        {
            if (!_ordinalMap.ContainsKey(f.Name))
            {
                // Add the field as a column in the table if it doesn't exist
                // already.
                DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                    : table.Columns.Add(f.Name, f.FieldType);

                // Add the field to the ordinal map.
                _ordinalMap.Add(f.Name, dc.Ordinal);
            }
        }
        foreach (PropertyInfo p in type.GetProperties())
        {
            if (!_ordinalMap.ContainsKey(p.Name))
            {
                // Add the property as a column in the table if it doesn't exist
                // already.
                DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                    : table.Columns.Add(p.Name, p.PropertyType);

                // Add the property to the ordinal map.
                _ordinalMap.Add(p.Name, dc.Ordinal);
            }
        }

        // Return the table.
        return table;
    }
}
public static class CustomLINQtoDataSetMethods
{
    public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
    {
        return new ObjectShredder<T>().Shred(source, null, null);
    }

    public static DataTable CopyToDataTable<T>(this IEnumerable<T> source,
                                                DataTable table, LoadOption? options)
    {
        return new ObjectShredder<T>().Shred(source, table, options);
    }
}





namespace midas_sise
{
    class cls_util
    {
        static byte[] Skey = ASCIIEncoding.ASCII.GetBytes("11111111");

        [DllImport("kernel32")]
        public static extern int SetSystemTime(ref SYSTEMTIME lpSystemTime);

        public struct SYSTEMTIME
        {
            public short wYear;			// 년도
            public short wMonth;		// 월
            public short wDayOfWeek;	// 요일
            public short wDay;			// 일
            public short wHour;			// 시
            public short wMinute;		// 분
            public short wSecond;		// 초
            public short wMilliseconds; // 1/100초
        }

        /// <summary>
        /// PC의 시스템 시간을 설정
        /// </summary>
        /// <param name="as_szDate">날짜 :: 20140101</param>
        /// <param name="as_szTime">시간 :: 010101001</param>
        public static void set_system_time(string as_szDate, string as_szTime)
        {
            string szYear = as_szDate.Substring(0, 4);
            string szMonth = as_szDate.Substring(4, 2);
            string szDay = as_szDate.Substring(6, 2);

            string szHour = as_szTime.Substring(0, 2);
            string szMinute = as_szTime.Substring(2, 2);
            string szSecond = as_szTime.Substring(4, 2);
            string szMiliSecond = as_szTime.Substring(6, 3);

            SYSTEMTIME sTime = new SYSTEMTIME();

            sTime.wYear = Convert.ToInt16(szYear);
            sTime.wMonth = Convert.ToInt16(szMonth); ;
            sTime.wDayOfWeek = 1;								// 일요일을 한주의 시작으로 설정
            sTime.wDay = Convert.ToInt16(szDay);
            sTime.wHour = (short)(Convert.ToInt16(szHour) - 9);	// 표준시 계산
            sTime.wMinute = Convert.ToInt16(szMinute);
            sTime.wSecond = Convert.ToInt16(szSecond);
            sTime.wMilliseconds = Convert.ToInt16(szMiliSecond);

            SetSystemTime(ref sTime);
        }	// end function


        public static byte[] StructureToByte(object a_obj)

        {
            int datasize = Marshal.SizeOf(a_obj);//((PACKET_DATA)obj).TotalBytes; // 구조체에 할당된 메모리의 크기를 구한다.
            IntPtr l_ptr_buff = Marshal.AllocHGlobal(datasize); // 비관리 메모리 영역에 구조체 크기만큼의 메모리를 할당한다.

            Marshal.StructureToPtr(a_obj, l_ptr_buff, false); // 할당된 구조체 객체의 주소를 구한다.
            byte[] data = new byte[datasize]; // 구조체가 복사될 배열
            Marshal.Copy(l_ptr_buff, data, 0, datasize); // 구조체 객체를 배열에 복사

            Marshal.FreeHGlobal(l_ptr_buff); // 비관리 메모리 영역에 할당했던 메모리를 해제함
            return data; // 배열을 리턴
        }

        //byte 배열을 구조체로
        public static object ByteToStructure(byte[] abyte_data, Type a_type)
        {
            IntPtr _ptr_buff = Marshal.AllocHGlobal(abyte_data.Length); // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.

            Marshal.Copy(abyte_data, 0, _ptr_buff, abyte_data.Length); // 배열에 저장된 데이터를 위에서 할당한 메모리 영역에 복사한다.
            object _obj = Marshal.PtrToStructure(_ptr_buff, a_type); // 복사된 데이터를 구조체 객체로 변환한다.

            Marshal.FreeHGlobal(_ptr_buff); // 비관리 메모리 영역에 할당했던 메모리를 해제함
            if (Marshal.SizeOf(_obj) != abyte_data.Length)// (((PACKET_DATA)obj).TotalBytes != data.Length) // 구조체와 원래의 데이터의 크기 비교
            {
                return null; // 크기가 다르면 null 리턴
            }
            return _obj; // 구조체 리턴
        }


        public static T ByteToStruct2<T>(byte[] buffer) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            if (size > buffer.Length)
            {
                //throw new Exception();
            }
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, ptr, size);
            T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return obj;
        }






        public static byte[] byte_combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }


        public static string 전일대비구분포함_old(string 전일대비구분값)
        {
            switch (전일대비구분값)
            {
                case "2":
                    return "▲";
                case "5":
                    return "▼";
                case "3":
                    return " ";
                case "1":
                    return "↑";
                case "4":
                    return "↓";
                default:
                    return "";
            }
        }

        public static string GetCurrentDirectoryWithPath()
        {
            return System.Environment.CurrentDirectory;
        }

        public static string f_전일대비구분포함(string 전일대비구분값)
        {
            switch (전일대비구분값)
            {
                case "2":
                    return ""; //+
                case "5":
                    return "-";
                case "3":
                    return "";
                case "1":
                    return "";
                case "4":
                    return "-";
                default:
                    return "";
            }
        }

        public static Int32 f_전일대비(string 전일대비구분값)
        {
            switch (전일대비구분값)
            {
                case "2":
                    return 1; //+
                case "5":
                    return -1;
                case "3":
                    return 0;
                case "1":
                    return 1;
                case "4":
                    return -1;
                default:
                    return 0;
            }
        }


        public static string f_conversion_time_even(string as_1)
        {
            string ls_out;
            int i;

            i = int.Parse(as_1.Substring(5, 1));
            if (i % 2 == 1)
            {
                i = i - 1;
                ls_out = as_1.Substring(0, 5) + i.ToString();
            }
            else
            {
                ls_out = as_1;
            }
            return ls_out;
        }




        /// <summary>
        /// 천단위 변환된 숫자 리턴 
        /// </summary>
        /// <param name="szNumber"></param>
        /// <returns></returns>
        public static string GetNumberFormat(string szNumber)
        {
            if (isNaN(szNumber))
            {
                return String.Format("{0:#,##0}", Double.Parse(szNumber.Replace(",", "")));
            }
            else
            {
                return "0";
            }
        }


        /// <summary>
        /// 천단위 변환된 숫자 리턴
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        public static string GetNumberFormat(double iNumber)
        {
            return String.Format("{0:#,##0}", iNumber);
        }

        public static double f_todouble2(string as_1)
        {
            return Convert.ToDouble(as_1.Insert(as_1.Length - 2, "."));
        }



        /// <summary>
        /// 틱 단위 리턴 
        /// </summary>
        /// <param name="iNumber"></param>
        /// <returns></returns>
        public static double f_tick_size(double iNumber)
        {
            if (iNumber >= 500000)
            {
                return 1000;
            }
            else if (iNumber >= 100000 && iNumber < 500000)
            {
                return 500;
            }
            else if (iNumber >= 50000 && iNumber < 100000)
            {
                return 100;
            }
            else if (iNumber >= 10000 && iNumber < 50000)
            {
                return 50;
            }
            else if (iNumber >= 5000 && iNumber < 10000)
            {
                return 10;
            }
            else if (iNumber >= 1000 && iNumber < 5000)
            {
                return 5;
            }
            else
            {
                return 1;
            }
        }   // end fucntion


        public static double chage_to_double(int as_가격)
        {
            string ls_temp = $"{  (as_가격 / 100):00000000.00}";  //지수경우     //as_선물가격 = "00000279.00";  //지수경우


            return double.Parse(ls_temp);
        }


        /// <summary>
        /// +1틱 적용된 가격 리턴
        /// </summary>
        /// <param name="iPrice"></param>
        /// <returns></returns>
        public static double GetPricePlus01(double iPrice)
        {
            return iPrice + f_tick_size(iPrice);
        }


        /// <summary>
        /// +1틱 적용된 가격 리턴 
        /// </summary>
        /// <param name="szPrice"></param>
        /// <returns></returns>
        public static double GetPricePlus01(string szPrice)
        {
            double iPrice = Double.Parse(szPrice);
            return iPrice + f_tick_size(iPrice);
        }


        /// <summary>
        /// -1틱 적용된 가격 리턴
        /// </summary>
        /// <param name="iPrice"></param>
        /// <returns></returns>
        public static double GetPriceMinus01(double iPrice)
        {
            return iPrice - f_tick_size(iPrice);
        }


        /// <summary>
        /// -1틱 적용된 가격 
        /// </summary>
        /// <param name="szPrice"></param>
        /// <returns></returns>
        public static double GetPriceMinus01(string szPrice)
        {
            double iPrice = Double.Parse(szPrice);
            return iPrice - f_tick_size(iPrice);
        }


        /// <summary>
        /// % 적용된 금액 리턴 
        /// </summary>
        /// <param name="iPrice"></param>
        /// <param name="iPercent"></param>
        /// <returns></returns>
        public static double GetPricePercent(double iPrice, double iPercent)
        {
            return iPrice + (iPrice * iPercent / 100);
        }


        /// <summary>
        /// 현재 디렉토리명 리턴
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectoryName()
        {
            string[] aTmp = System.Environment.CurrentDirectory.Split('\\');

            return aTmp[aTmp.Length - 1];
        }


        ///// <summary>
        ///// 현재 디렉토리명 리턴(전체경로 포함) 
        ///// </summary>
        ///// <returns></returns>
        //public static string GetCurrentDirectoryWithPath()
        //{
        //    return System.Environment.CurrentDirectory;
        //}


        /// <summary>
        /// 해당 값이 숫자인지 아닌지 체크
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isNaN(string str)
        {
            double iNum;
            bool bNum = double.TryParse(str, out iNum);
            return bNum;
        }

        //// idx 시작값 생성
        //public static string GenerateIdx()
        //{
        //    double iNum;
        //    bool bNum = double.TryParse(GetCurrentDirectoryName(), out iNum);

        //    if (bNum)
        //    {
        //        return iNum.ToString();
        //    }
        //    else 
        //    {
        //        return "0";
        //    }
        //}


        public static string GetFormatNow(string format)
        {
            return DateTime.Now.ToString(format);
        }	// end function


    }

}
