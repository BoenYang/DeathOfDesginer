using UnityEngine;
using System.Collections.Generic;

public abstract partial class ConfigDataBase
{
    public int Id { get { return (int) ConfigDataDic["Id"]; } }

    public Dictionary<string, object> ConfigDataDic = new Dictionary<string, object>();

    /// <summary>
    ///     C#通过字段名获取字段
    /// </summary>
    /// <typeparam name="T">字段类型</typeparam>
    /// <param name="key">字段名</param>
    /// <returns>字段</returns>
    public T Get<T>(string key)
    {
        object ret;
        if (ConfigDataDic.TryGetValue(key, out ret))
        {
            try
            {
                return (T) ret;
            }
            catch
            {
                Debug.LogErrorFormat("{0} {1} {2}", GetType().FullName, "获取字段类型错误", key);
            }
        }
        else
        {
            Debug.LogErrorFormat("{0} {1} {2}", GetType().FullName, "没有字段", key);
        }
        return default(T);
    }

    /// <summary>
    ///     Lua通过字段名获取(object)字段
    /// </summary>
    /// <param name="key">字段名</param>
    /// <returns>(object)字段</returns>
    public object this[string key]
    {
        get { return Get<object>(key); }
    }

    public int Init(byte[] sources, int offset, List<KeyValuePair<string, DataTypeCode>> columnList)
    {
        source = sources;
        index = offset;
        columnList.ForEach(InitColumn);
        return Id;
    }

    private void InitColumn(KeyValuePair<string, DataTypeCode> pair)
    {
        switch (pair.Value)
        {
            //基本类型
            case DataTypeCode.Boolean:
                ConfigDataDic.Add(pair.Key, ReadBoolean());
                break;
            case DataTypeCode.Byte:
                ConfigDataDic.Add(pair.Key, ReadByte());
                break;
            case DataTypeCode.Int32:
                ConfigDataDic.Add(pair.Key, ReadInt32());
                break;
            case DataTypeCode.UInt32:
                ConfigDataDic.Add(pair.Key, ReadUInt32());
                break;
            case DataTypeCode.Int64:
                ConfigDataDic.Add(pair.Key, ReadInt64());
                break;
            case DataTypeCode.UInt64:
                ConfigDataDic.Add(pair.Key, ReadUInt64());
                break;
            case DataTypeCode.Single:
                ConfigDataDic.Add(pair.Key, ReadSingle());
                break;
            case DataTypeCode.Double:
                ConfigDataDic.Add(pair.Key, ReadDouble());
                break;
            case DataTypeCode.String:
                ConfigDataDic.Add(pair.Key, ReadString());
                break;
            //一维数组
            case DataTypeCode.BooleanArr1:
                ConfigDataDic.Add(pair.Key, ReadBooleanArr1());
                break;
            case DataTypeCode.ByteArr1:
                ConfigDataDic.Add(pair.Key, ReadByteArr1());
                break;
            case DataTypeCode.Int32Arr1:
                ConfigDataDic.Add(pair.Key, ReadInt32Arr1());
                break;
            case DataTypeCode.UInt32Arr1:
                ConfigDataDic.Add(pair.Key, ReadUInt32Arr1());
                break;
            case DataTypeCode.Int64Arr1:
                ConfigDataDic.Add(pair.Key, ReadInt64Arr1());
                break;
            case DataTypeCode.UInt64Arr1:
                ConfigDataDic.Add(pair.Key, ReadUInt64Arr1());
                break;
            case DataTypeCode.SingleArr1:
                ConfigDataDic.Add(pair.Key, ReadSingleArr1());
                break;
            case DataTypeCode.DoubleArr1:
                ConfigDataDic.Add(pair.Key, ReadDoubleArr1());
                break;
            case DataTypeCode.StringArr1:
                ConfigDataDic.Add(pair.Key, ReadStringArr1());
                break;
            //二维数组
            case DataTypeCode.BooleanArr2:
                ConfigDataDic.Add(pair.Key, ReadBooleanArr2());
                break;
            case DataTypeCode.ByteArr2:
                ConfigDataDic.Add(pair.Key, ReadByteArr2());
                break;
            case DataTypeCode.Int32Arr2:
                ConfigDataDic.Add(pair.Key, ReadInt32Arr2());
                break;
            case DataTypeCode.UInt32Arr2:
                ConfigDataDic.Add(pair.Key, ReadUInt32Arr2());
                break;
            case DataTypeCode.Int64Arr2:
                ConfigDataDic.Add(pair.Key, ReadInt64Arr2());
                break;
            case DataTypeCode.UInt64Arr2:
                ConfigDataDic.Add(pair.Key, ReadUInt64Arr2());
                break;
            case DataTypeCode.SingleArr2:
                ConfigDataDic.Add(pair.Key, ReadSingleArr2());
                break;
            case DataTypeCode.DoubleArr2:
                ConfigDataDic.Add(pair.Key, ReadDoubleArr2());
                break;
            case DataTypeCode.StringArr2:
                ConfigDataDic.Add(pair.Key, ReadStringArr2());
                break;
            //三维数组
            case DataTypeCode.BooleanArr3:
                ConfigDataDic.Add(pair.Key, ReadBooleanArr3());
                break;
            case DataTypeCode.ByteArr3:
                ConfigDataDic.Add(pair.Key, ReadByteArr3());
                break;
            case DataTypeCode.Int32Arr3:
                ConfigDataDic.Add(pair.Key, ReadInt32Arr3());
                break;
            case DataTypeCode.UInt32Arr3:
                ConfigDataDic.Add(pair.Key, ReadUInt32Arr3());
                break;
            case DataTypeCode.Int64Arr3:
                ConfigDataDic.Add(pair.Key, ReadInt64Arr3());
                break;
            case DataTypeCode.UInt64Arr3:
                ConfigDataDic.Add(pair.Key, ReadUInt64Arr3());
                break;
            case DataTypeCode.SingleArr3:
                ConfigDataDic.Add(pair.Key, ReadSingleArr3());
                break;
            case DataTypeCode.DoubleArr3:
                ConfigDataDic.Add(pair.Key, ReadDoubleArr3());
                break;
            case DataTypeCode.StringArr3:
                ConfigDataDic.Add(pair.Key, ReadStringArr3());
                break;
            //四维数组
            case DataTypeCode.BooleanArr4:
                ConfigDataDic.Add(pair.Key, ReadBooleanArr4());
                break;
            case DataTypeCode.ByteArr4:
                ConfigDataDic.Add(pair.Key, ReadByteArr4());
                break;
            case DataTypeCode.Int32Arr4:
                ConfigDataDic.Add(pair.Key, ReadInt32Arr4());
                break;
            case DataTypeCode.UInt32Arr4:
                ConfigDataDic.Add(pair.Key, ReadUInt32Arr4());
                break;
            case DataTypeCode.Int64Arr4:
                ConfigDataDic.Add(pair.Key, ReadInt64Arr4());
                break;
            case DataTypeCode.UInt64Arr4:
                ConfigDataDic.Add(pair.Key, ReadUInt64Arr4());
                break;
            case DataTypeCode.SingleArr4:
                ConfigDataDic.Add(pair.Key, ReadSingleArr4());
                break;
            case DataTypeCode.DoubleArr4:
                ConfigDataDic.Add(pair.Key, ReadDoubleArr4());
                break;
            case DataTypeCode.StringArr4:
                ConfigDataDic.Add(pair.Key, ReadStringArr4());
                break;
        }
    }
}