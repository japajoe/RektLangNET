using System;

namespace RektLangNET
{
    public unsafe struct NullTerminatedString
    {
        public readonly byte* Data;

        public NullTerminatedString(byte* data)
        {
            Data = data;
        }

        public override string ToString()
        {
            int length = 0;
            byte* ptr = Data;
            while (*ptr != 0)
            {
                length += 1;
                ptr += 1;
            }

            return System.Text.Encoding.UTF8.GetString(Data, length);
        }

        public static string GetString(byte[] data)
        {
            UIntPtr ptr = new UIntPtr(BitConverter.ToUInt64(data, 0));
            NullTerminatedString s = new NullTerminatedString((byte*)ptr.ToPointer());
            return s.ToString();
        }        

        public static implicit operator string(NullTerminatedString nts) => nts.ToString();
    }    
}