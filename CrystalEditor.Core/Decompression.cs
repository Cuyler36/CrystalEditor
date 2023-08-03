using System;
using System.Collections.Generic;

namespace CrystalEditor.Core
{
    public static class Decompression
    {
        private static byte[] Decompress_Method_10(in byte[] data, int len, int start_pos, out int end_pos)
        {
            byte[] decompressed = new byte[len];
            int dst_p = 0;
            int src_p = start_pos;

            while (len != 0)
            {
                int bitfield = data[src_p++];

                for (int i = 0; i < 8; i++)
                {
                    if ((bitfield & 0x80) == 0)
                    {
                        /* Direct copy */

                        len--;
                        decompressed[dst_p++] = data[src_p++];
                    }
                    else
                    {
                        int info = data[src_p];
                        int i0 = info >> 4;
                        int i1 = info & 0xF;
                        int copy_len = i0 + 3;

                        /* Maximum backbuffer position is 0xFFF+1 = 0x1000 */
                        int back_pos = (((data[src_p++] & 0xF) << 8) | data[src_p++]) + 1;

                        len -= copy_len;

                        for (int j = 0; j < copy_len; j++)
                        {
                            try
                            {
                                decompressed[dst_p] = decompressed[dst_p - back_pos];
                                dst_p++;
                            }
                            catch
                            {
                                end_pos = src_p;
                                return decompressed;
                            }
                        }
                    }

                    bitfield <<= 1;
                }
            }

            end_pos = src_p;
            return decompressed;
        }

        private static byte[] Decompress_Method_11(in byte[] data, int len, int start_pos, out int end_pos)
        {
            byte[] decompressed = new byte[len];
            int dst_p = 0;
            int src_p = start_pos;

            while (len != 0)
            {
                int bitfield = data[src_p++];

                for (int i = 0; i < 8; i++)
                {
                    if (len == 0)
                    {
                        break;
                    }

                    if ((bitfield & 0x80) == 0)
                    {
                        /* Direct copy */

                        len--;
                        decompressed[dst_p++] = data[src_p++];
                    }
                    else
                    {
                        int info = data[src_p];
                        int i0 = info >> 4;
                        int i1 = info & 0xF;
                        int copy_len;

                        if (i0 == 1)
                        {
                            src_p++;
                            copy_len = ((i1 << 12) | (data[src_p] << 4) | (data[src_p + 1] >> 4)) + 0x111;
                            src_p++;
                        }
                        else if (i0 == 0)
                        {
                            src_p++;
                            copy_len = ((i1 << 4) | (data[src_p] >> 4)) + 0x11;
                        }
                        else
                        {
                            copy_len = i0 + 1;
                        }

                        /* Maximum backbuffer position is 0xFFF+1 = 0x1000 */
                        int back_pos = (((data[src_p++] & 0xF) << 8) | data[src_p++]) + 1;

                        len -= copy_len;

                        for (int j = 0; j < copy_len; j++)
                        {
                            try
                            {
                                decompressed[dst_p] = decompressed[dst_p - back_pos];
                                dst_p++;
                            }
                            catch
                            {
                                end_pos = src_p;
                                return decompressed;
                            }
                        }
                    }

                    bitfield <<= 1;
                }
            }

            end_pos = src_p;
            return decompressed;
        }

        public static List<byte[]> Decompress(in byte[] data)
        {
            int pos = 0;
            List<byte[]> files = new List<byte[]>();

            while (pos < data.Length)
            {
                int len = (data[pos + 3] << 16) | (data[pos + 2] << 8) | data[pos + 1];
                int start_pos = pos + 4;

                if (len == 0)
                {
                    len = (data[pos + 7] << 16) | (data[pos + 6] << 8) | data[pos + 5];
                    start_pos = pos + 8;
                }

                if (len == 0)
                {
                    throw new Exception("Zero'd decompressed size!");
                }

                int type = data[pos + 0] & 0xF0;

                if (type == 0x10)
                {
                    int subtype = data[pos + 0] & 0x0F;

                    if (subtype == 0)
                    {
                        files.Add(Decompress_Method_10(data, len, start_pos, out pos));
                    }
                    else if (subtype == 1)
                    {
                        files.Add(Decompress_Method_11(data, len, start_pos, out pos));
                    }
                    else
                    {
                        throw new Exception($"Unknown compression 0x1X subtype {subtype:X}");
                    }
                }
                else
                {
                    throw new Exception($"Unhandled compression type 0x{type:X}X");
                }

                break;
            }

            return files;
        }
    }
}
