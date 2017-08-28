using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffingLibs
{
    public class ReadFunctions
    {
        private static int indexTab = 0;
        private static string[] data_tab;
        private static int INT_SIZE = 32;
        private static int SHORT_SIZE = 16;
        private static int SHORT_MIN_VALUE = -32768;
        private static int SHORT_MAX_VALUE = 32767;
        private static int UNSIGNED_SHORT_MAX_VALUE = 65536;
        private static int CHUNCK_BIT_SIZE = 7;
        private static int MASK_10000000 = 128;
        private static int MASK_01111111 = 127;

        public int readVarShort(BinaryReader b)
        {
            int loc4 = 0;
            int loc1 = 0;
            int loc2 = 0;
            bool loc3 = false;
            int i = 1;

            while (loc2 < SHORT_SIZE)
            {
                loc4 = b.ReadByte();
                loc3 = (loc4 & MASK_10000000) == MASK_10000000;
                if (loc2 > 0)
                    loc1 = loc1 + ((loc4 & MASK_01111111) << loc2);
                else
                    loc1 = loc1 + (loc4 & MASK_01111111);
                loc2 = loc2 + CHUNCK_BIT_SIZE;
                if (!loc3)
                {
                    if (loc1 > SHORT_MAX_VALUE)
                        loc1 = loc1 - UNSIGNED_SHORT_MAX_VALUE;
                    return loc1;
                }
                i++;
            }
            return loc1;
        }
    }
}
