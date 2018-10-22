using System;

namespace easy_socket
{
    public class network_convert
    {
        public static ushort switch_ushort(ushort us)
        {
            return (ushort)((us>>8)+((us&0xff)<<8));
        }
        public static UInt32 switch_UInt32(UInt32 ui)
        {
            return (UInt32)((ui&0xff)<<24)
                +((ui&0xff00)<<8)
                +((ui&0xff0000)>>8)
                +((ui&0xff000000)>>24);
        }
    }
}
