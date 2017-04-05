using UnityEngine;

public class GameStart : MonoBehaviour {


	void Start ()
	{
        ConfigMng configMng = GlobalMng.GlobalSingleton<ConfigMng>();
        configMng.Init();
	    configMng.AllConfigLoadEnd += OnConfigLoaded;
	}

    private void OnConfigLoaded()
    {
        GameModeBase game = GameModeBase.CreateGameMode("Normal");
        game.Init();
        game.StartGame();
    }

}
