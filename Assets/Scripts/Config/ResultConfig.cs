// 由游戏编辑器自动创建修改，请勿手动修改
// 根据 Result.xlsx 文件创建

public sealed class ResultConfig : ConfigDataBase
{
	/// <summary>类型</summary>
	public int Type { get { return Get<int>("Type"); } }

	/// <summary>结束</summary>
	public int Finish { get { return Get<int>("Finish"); } }

	/// <summary>描述内容</summary>
	public string Describe { get { return Get<string>("Describe"); } }

	public static ResultConfig Get(int id)
	{
		return ConfigMng.Instance.GetConfig<ResultConfig>(id);
	}

}
