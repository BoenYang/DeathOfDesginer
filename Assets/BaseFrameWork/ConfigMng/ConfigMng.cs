using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigMng : MonoBehaviour
{
    #region 数据提供

    /// <summary>
    ///     通过Id取得一个ConfigData对象
    /// </summary>
    /// <typeparam name="T">ConfigDataBase的子类</typeparam>
    /// <param name="id">静态表Id</param>
    /// <returns>ConfigData</returns>
    public T GetConfig<T>(int id) where T : ConfigDataBase
    {
        Type type = typeof (T);
        if (m_configDic.ContainsKey(type) && m_configDic[type].ContainsKey(id))
        {
            return m_configDic[type][id] as T;
        }
        else
        {
            Debug.LogWarning("获取字段参数错误！ 类型：" + type.Name + " Id：" + id);
            return null;
        }
    }

    /// <summary>
    /// 是否有某个ID的配置文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HasConfig<T>(int id) where T : ConfigDataBase
    {
        Type type = typeof (T);
        if (m_configDic.ContainsKey(type))
        {
            return m_configDic[type].ContainsKey(id);
        }
        return false;
    }

    /// <summary>
    ///     获得一个静态表下的所有ConfigData对象的字典
    /// </summary>
    /// <param name="type">ConfigDataBase的子类</param>
    /// <returns>字典</returns>
    public Dictionary<int, T> GetAllConfigs<T>() where T : ConfigDataBase
    {
        Type type = typeof (T);

        //异常处理
        if (!m_configDic.ContainsKey(type))
        {
            Debug.LogError("ConfigDataBase不存在 " + type + " 类型的子类");
            return null;
        }

        //浅拷贝，防止污染数据
        var ret = new Dictionary<int, T>(m_configDic[type].Count);
        foreach (KeyValuePair<int, ConfigDataBase> pair in m_configDic[type])
        {
            ret.Add(pair.Key, pair.Value as T);
        }
        return ret;
    }

    #endregion

    #region 数据初始化

    /// <summary>
    ///     ConfigMng主字典
    /// </summary>
    private Dictionary<Type, Dictionary<int, ConfigDataBase>> m_configDic =
        new Dictionary<Type, Dictionary<int, ConfigDataBase>>();

    /// <summary>
    /// 所有配置表加载完成委托
    /// </summary>
    public delegate void LoadEnd();

    /// <summary>
    /// 所有配置表加载完成事件
    /// </summary>
    public event LoadEnd AllConfigLoadEnd;

    public static ConfigMng Instance { get; private set; }

    public void Start()
    {
        Instance = this;

        gameObject.isStatic = true;
        //协程加载数据表
        StartCoroutine(LoadConfig());
    }

    private IEnumerator LoadConfig()
    {
        foreach (Type type in ConfigList.AllConfigTypeList)
        {
            yield return null;
            string path = "Config/" + type.Name.Replace("Config", "");
            TextAsset asset = (TextAsset) Resources.Load(path);
            byte[] binary = asset.bytes;
            m_configDic.Add(type, BinaryParse(binary, type));
        }
        if (null != AllConfigLoadEnd)
        {
            AllConfigLoadEnd();
        }
    }

    /// <summary>
    ///     分析单个数据表
    /// </summary>
    private Dictionary<int, ConfigDataBase> BinaryParse(byte[] binary, Type configType)
    {
        Dictionary<int, ConfigDataBase> dic = new Dictionary<int, ConfigDataBase>();

        //Head Begin//

        //二进制源下标
        int index = 0;

        //创建行头清单
        //总行数
        int configArrayLength = BitConverter.ToInt32(binary, index);
        index += 4;
        int[] configArray = new int[configArrayLength];
        for (int i = 0; i < configArray.Length; i++)
        {
            configArray[i] = BitConverter.ToInt32(binary, index);
            index += 4;
        }

        //创建列头清单
        //总列数
        int columnListCount = BitConverter.ToInt32(binary, index);
        index += 4;
        List<KeyValuePair<string, DataTypeCode>> columnList =
            new List<KeyValuePair<string, DataTypeCode>>(columnListCount);
        for (int i = 0; i < columnListCount; i++)
        {
            int stringlength = BitConverter.ToInt32(binary, index);
            index += 4;
            string key = System.Text.Encoding.UTF8.GetString(binary, index, stringlength);
            index += stringlength;
            byte value = binary[index++];
            columnList.Add(new KeyValuePair<string, DataTypeCode>(key, (DataTypeCode) value));
        }

        int headOffset = index;
        //Head End//

        for (int i = 0; i < configArray.Length; i++)
        {
            ConfigDataBase configData = Activator.CreateInstance(configType) as ConfigDataBase;
            int id = configData.Init(binary, configArray[i] + headOffset, columnList);

            if (dic.ContainsKey(id))
            {
                Debug.LogError("静态表" + configType.ToString() + "存在重复ID " + id.ToString());
                continue;
            }

            dic.Add(id, configData);
        }
        return dic;
    }

    #endregion
}