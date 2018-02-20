public class Player : GameCharacter {

    public TypingSystem typingSystem;
    public PlayerMovement movement;

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
}
