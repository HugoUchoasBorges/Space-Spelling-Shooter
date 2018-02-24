using UnityEngine;

public class Player : GameCharacter {

    public TypingSystem typingSystem;
    public PlayerMovement movement;

    GameObject playerBullet;

    private static int lifes;
    public static int Lifes
    {
        get { return lifes; }
        set
        {
            lifes = value;

            GUIController.RefreshGUI();
        }
    }

    // Use this for initialization
    protected override void Start () {

        base.Start();

        Lifes = GlobalVariables.defaultLifeCount;

        movement = gameObject.AddComponent<PlayerMovement>();
        typingSystem = gameObject.AddComponent<TypingSystem>();

        InitializesAudios(GlobalVariables.audio_player);

        PlayAudio(GlobalVariables.ENUM_AUDIO.game_start);
    }

    public void Shoot()
    {
        GameObject playerBullet = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.playerBullet]);
        playerBullet.transform.position = transform.position;
    }
}
