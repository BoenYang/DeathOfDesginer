using UnityEngine;

public class GameStart : MonoBehaviour
{

    public static NormalMode Game;

	void Start ()
	{
        ConfigMng configMng = GlobalMng.GlobalSingleton<ConfigMng>();
        configMng.Init();
	    configMng.AllConfigLoadEnd += OnConfigLoaded;
	}

    private void OnConfigLoaded()
    {
        Game = (NormalMode)GameModeBase.CreateGameMode("Normal");
        Game.Init();
        Game.StartGame();
    }

}
