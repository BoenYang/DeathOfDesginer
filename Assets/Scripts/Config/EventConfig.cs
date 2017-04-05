// 由游戏编辑器自动创建修改，请勿手动修改
// 根据 Event.xlsx 文件创建

public sealed class EventConfig : ConfigDataBase
{
	/// <summary>事件类型</summary>
	public int Type { get { return Get<int>("Type"); } }

	/// <summary>是否需要选择</summary>
	public int Needchoice { get { return Get<int>("Needchoice"); } }

	/// <summary>事件主角</summary>
	public int Hero { get { return Get<int>("Hero"); } }

	/// <summary>事件内容</summary>
	public string Event { get { return Get<string>("Event"); } }

	/// <summary>第一个选择内容</summary>
	public string ChoiceOne { get { return Get<string>("ChoiceOne"); } }

	/// <summary>选择第一个后索引</summary>
	public int[] ChoiceOneid { get { return Get<int[]>("ChoiceOneid"); } }

	/// <summary>选择第一个后分数变化</summary>
	public int[] ChoiceOneSorce { get { return Get<int[]>("ChoiceOneSorce"); } }

	/// <summary>第二个选择内容</summary>
	public string ChoiceTwo { get { return Get<string>("ChoiceTwo"); } }

	/// <summary>选择第二个后的索引</summary>
	public int[] ChoiceTwoid { get { return Get<int[]>("ChoiceTwoid"); } }

	/// <summary>选择第二个后分数变化</summary>
	public int[] ChoiceTwoSorce { get { return Get<int[]>("ChoiceTwoSorce"); } }

	public static EventConfig Get(int id)
	{
		return ConfigMng.Instance.GetConfig<EventConfig>(id);
	}

}
