using UnityEngine;

public class GameStart : MonoBehaviour
{

    public static GameModeBase Game;

	void Start ()
	{
        ConfigMng configMng = GlobalMng.GlobalSingleton<ConfigMng>();
        configMng.Init();
	    configMng.AllConfigLoadEnd += OnConfigLoaded;
	}

    private void OnConfigLoaded()
    {
        Game = GameModeBase.CreateGameMode("Normal");
        Game.Init();
        Game.StartGame();
    }

}
