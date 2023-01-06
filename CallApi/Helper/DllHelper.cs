using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CallApi.Helper
{
    static class GoDll
    {
        [DllImport("coverapp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddHello();
        
    }

    static class GoMath
    {
        [DllImport("coverapp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int Add(int a, int b);
        [DllImport("coverapp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int Sub(int a, int b);
        [DllImport("coverapp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern double Cosine(double x);


        [DllImport("coverapp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void Test();

        [DllImport("coverapp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int AddNum(int b);

        [DllImport("coverapp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void TestParam(GoString pageURL, GoString userAgent, GoString proxy, GoString redirectVal, GoString timeoutraw, GoString addedQuery, GoString body);

        [DllImport("coverapp.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void TestParam2(GoString pageURL, GoString userAgent, GoString proxy, GoString redirectVal, GoString timeoutraw, GoInterface addedQuery, GoInterface body);

    }

    static class GoHello
    {
        [DllImport("go.tls.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe void* CallTls(GoString method, GoString pageURL, GoString userAgent, GoString proxy, GoString queryString, GoString bodyJson);
    }

    unsafe static class GoUtils
    {
        public static GoString ToGoString(this string value)
        {
            return new GoString { p = Marshal.StringToHGlobalAnsi(value), n = value.Length };
        }

        public static string PointerToString(void* value)
        {
            byte* buf = (byte*)value;
            byte[] lenBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                lenBytes[i] = *buf++;
            }
            // Read the result itself
            int n = BitConverter.ToInt32(lenBytes, 0);
            int j = 0;
            byte[] bytes = new byte[n];

            for (int i = 0; i < n; i++)
            {
                if (i < 4)
                {
                    *buf++ = 0;
                }
                else
                {
                    bytes[j] = *buf++;
                    j++;
                }
            }
            var results = Encoding.UTF8.GetString(bytes);
            return results;
        }
    }

    struct GoString
    {
        public IntPtr p;
        public Int64 n;
    }

    struct GoInterface
    {
        public IntPtr t;
        public IntPtr v;
    }

    public struct GoSlice
    {
        public IntPtr Data;

        public long Len;

        public long Cap;
    }
}
