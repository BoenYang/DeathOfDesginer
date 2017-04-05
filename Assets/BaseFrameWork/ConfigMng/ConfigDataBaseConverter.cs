// 系统自动生成，请勿修改

using System;

public abstract partial class ConfigDataBase
{
    private static byte[] source;
    private static int index;

    private static int UseIndex(int add)
    {
        index += add;
        return index - add;
    }

    #region 基本类型

    private static bool ReadBoolean()
    {
        return BitConverter.ToBoolean(source, UseIndex(1));
    }

    private static byte ReadByte()
    {
        return source[UseIndex(1)];
    }

    private static int ReadInt32()
    {
        return BitConverter.ToInt32(source, UseIndex(4));
    }

    private static uint ReadUInt32()
    {
        return BitConverter.ToUInt32(source, UseIndex(4));
    }

    private static long ReadInt64()
    {
        return BitConverter.ToInt64(source, UseIndex(8));
    }

    private static ulong ReadUInt64()
    {
        return BitConverter.ToUInt64(source, UseIndex(8));
    }

    private static float ReadSingle()
    {
        return BitConverter.ToSingle(source, UseIndex(4));
    }

    private static double ReadDouble()
    {
        return BitConverter.ToDouble(source, UseIndex(8));
    }

    private static string ReadString()
    {
        int stringlength = BitConverter.ToInt32(source, UseIndex(4));
        return System.Text.Encoding.UTF8.GetString(source, UseIndex(stringlength), stringlength);
    }

    #endregion

    #region 一维数组

    private static bool[] ReadBooleanArr1()
    {
        byte a1 = source[UseIndex(1)];
        bool[] ret = new bool[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadBoolean();
        }
        return ret;
    }

    private static byte[] ReadByteArr1()
    {
        byte a1 = source[UseIndex(1)];
        byte[] ret = new byte[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadByte();
        }
        return ret;
    }

    private static int[] ReadInt32Arr1()
    {
        byte a1 = source[UseIndex(1)];
        int[] ret = new int[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadInt32();
        }
        return ret;
    }

    private static uint[] ReadUInt32Arr1()
    {
        byte a1 = source[UseIndex(1)];
        uint[] ret = new uint[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadUInt32();
        }
        return ret;
    }

    private static long[] ReadInt64Arr1()
    {
        byte a1 = source[UseIndex(1)];
        long[] ret = new long[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadInt64();
        }
        return ret;
    }

    private static ulong[] ReadUInt64Arr1()
    {
        byte a1 = source[UseIndex(1)];
        ulong[] ret = new ulong[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadUInt64();
        }
        return ret;
    }

    private static float[] ReadSingleArr1()
    {
        byte a1 = source[UseIndex(1)];
        float[] ret = new float[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadSingle();
        }
        return ret;
    }

    private static double[] ReadDoubleArr1()
    {
        byte a1 = source[UseIndex(1)];
        double[] ret = new double[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadDouble();
        }
        return ret;
    }

    private static string[] ReadStringArr1()
    {
        byte a1 = source[UseIndex(1)];
        string[] ret = new string[a1];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            ret[aindex1] = ReadString();
        }
        return ret;
    }

    #endregion

    #region 二维数组

    private static bool[,] ReadBooleanArr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        bool[,] ret = new bool[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadBoolean();
            }
        }
        return ret;
    }

    private static byte[,] ReadByteArr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte[,] ret = new byte[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadByte();
            }
        }
        return ret;
    }

    private static int[,] ReadInt32Arr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        int[,] ret = new int[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadInt32();
            }
        }
        return ret;
    }

    private static uint[,] ReadUInt32Arr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        uint[,] ret = new uint[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadUInt32();
            }
        }
        return ret;
    }

    private static long[,] ReadInt64Arr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        long[,] ret = new long[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadInt64();
            }
        }
        return ret;
    }

    private static ulong[,] ReadUInt64Arr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        ulong[,] ret = new ulong[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadUInt64();
            }
        }
        return ret;
    }

    private static float[,] ReadSingleArr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        float[,] ret = new float[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadSingle();
            }
        }
        return ret;
    }

    private static double[,] ReadDoubleArr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        double[,] ret = new double[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadDouble();
            }
        }
        return ret;
    }

    private static string[,] ReadStringArr2()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        string[,] ret = new string[a1, a2];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                ret[aindex1, aindex2] = ReadString();
            }
        }
        return ret;
    }

    #endregion

    #region 三维数组

    private static bool[,,] ReadBooleanArr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        bool[,,] ret = new bool[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadBoolean();
                }
            }
        }
        return ret;
    }

    private static byte[,,] ReadByteArr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte[,,] ret = new byte[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadByte();
                }
            }
        }
        return ret;
    }

    private static int[,,] ReadInt32Arr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        int[,,] ret = new int[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadInt32();
                }
            }
        }
        return ret;
    }

    private static uint[,,] ReadUInt32Arr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        uint[,,] ret = new uint[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadUInt32();
                }
            }
        }
        return ret;
    }

    private static long[,,] ReadInt64Arr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        long[,,] ret = new long[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadInt64();
                }
            }
        }
        return ret;
    }

    private static ulong[,,] ReadUInt64Arr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        ulong[,,] ret = new ulong[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadUInt64();
                }
            }
        }
        return ret;
    }

    private static float[,,] ReadSingleArr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        float[,,] ret = new float[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadSingle();
                }
            }
        }
        return ret;
    }

    private static double[,,] ReadDoubleArr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        double[,,] ret = new double[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadDouble();
                }
            }
        }
        return ret;
    }

    private static string[,,] ReadStringArr3()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        string[,,] ret = new string[a1, a2, a3];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    ret[aindex1, aindex2, aindex3] = ReadString();
                }
            }
        }
        return ret;
    }

    #endregion

    #region 四维数组

    private static bool[,,,] ReadBooleanArr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        bool[,,,] ret = new bool[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadBoolean();
                    }
                }
            }
        }
        return ret;
    }

    private static byte[,,,] ReadByteArr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        byte[,,,] ret = new byte[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadByte();
                    }
                }
            }
        }
        return ret;
    }

    private static int[,,,] ReadInt32Arr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        int[,,,] ret = new int[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadInt32();
                    }
                }
            }
        }
        return ret;
    }

    private static uint[,,,] ReadUInt32Arr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        uint[,,,] ret = new uint[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadUInt32();
                    }
                }
            }
        }
        return ret;
    }

    private static long[,,,] ReadInt64Arr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        long[,,,] ret = new long[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadInt64();
                    }
                }
            }
        }
        return ret;
    }

    private static ulong[,,,] ReadUInt64Arr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        ulong[,,,] ret = new ulong[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadUInt64();
                    }
                }
            }
        }
        return ret;
    }

    private static float[,,,] ReadSingleArr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        float[,,,] ret = new float[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadSingle();
                    }
                }
            }
        }
        return ret;
    }

    private static double[,,,] ReadDoubleArr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        double[,,,] ret = new double[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadDouble();
                    }
                }
            }
        }
        return ret;
    }

    private static string[,,,] ReadStringArr4()
    {
        byte a1 = source[UseIndex(1)];
        byte a2 = source[UseIndex(1)];
        byte a3 = source[UseIndex(1)];
        byte a4 = source[UseIndex(1)];
        string[,,,] ret = new string[a1, a2, a3, a4];
        for (byte aindex1 = 0; aindex1 < a1; aindex1++)
        {
            for (byte aindex2 = 0; aindex2 < a2; aindex2++)
            {
                for (byte aindex3 = 0; aindex3 < a3; aindex3++)
                {
                    for (byte aindex4 = 0; aindex4 < a4; aindex4++)
                    {
                        ret[aindex1, aindex2, aindex3, aindex4] = ReadString();
                    }
                }
            }
        }
        return ret;
    }

    #endregion
}