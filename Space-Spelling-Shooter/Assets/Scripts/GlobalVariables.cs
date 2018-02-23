using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : Singleton<GlobalVariables> {

    protected GlobalVariables() { } // guarantee this will be always a singleton only - can't use the constructor!

    // Audios
    public static float AUDIO_VOLUME = 1f;

    public enum ENUM_AUDIO
    {
        player_key_lock,
        player_dying,
        player_key,
        player_key_space,
        player_key_return,
        enemy_hit,
        enemy_dying,
        game_start,
    }
    public static Dictionary<ENUM_AUDIO, AudioClip> audio_player = new Dictionary<ENUM_AUDIO, AudioClip>()
    {
        { ENUM_AUDIO.player_key_lock , Resources.Load<AudioClip>("Audios/player/key_lock")},
        { ENUM_AUDIO.player_key , Resources.Load<AudioClip>("Audios/player/key")},
        { ENUM_AUDIO.player_key_space , Resources.Load<AudioClip>("Audios/player/key_space")},
        { ENUM_AUDIO.player_key_return , Resources.Load<AudioClip>("Audios/player/key_return")},
        { ENUM_AUDIO.player_dying, Resources.Load<AudioClip>("Audios/player/player_dying")},
        { ENUM_AUDIO.game_start, Resources.Load<AudioClip>("Audios/jogo/game_start")},
    };
    public static Dictionary<ENUM_AUDIO, AudioClip> audio_game = new Dictionary<ENUM_AUDIO, AudioClip>()
    {
        { ENUM_AUDIO.game_start, Resources.Load<AudioClip>("Audios/jogo/game_start")},
        { ENUM_AUDIO.player_key , Resources.Load<AudioClip>("Audios/player/key")},
        { ENUM_AUDIO.player_key_return , Resources.Load<AudioClip>("Audios/player/key_return")},
    };
    public static Dictionary<ENUM_AUDIO, AudioClip> audio_enemy = new Dictionary<ENUM_AUDIO, AudioClip>()
    {
        { ENUM_AUDIO.enemy_hit, Resources.Load<AudioClip>("Audios/inimigos/enemy_hit")},
        { ENUM_AUDIO.enemy_dying, Resources.Load<AudioClip>("Audios/inimigos/enemy_dying")},
    };

    // Prefabs
    public enum ENUM_PREFAB
    {
        canvas,
        defaultEnemy,
        background1,
        spaceship1Player,
        spaceship2Player,
        spaceship3Player,
        audioSource,
    }
    public static Dictionary<ENUM_PREFAB, GameObject> prefab_dict = new Dictionary<ENUM_PREFAB, GameObject>(){
        { ENUM_PREFAB.canvas, Resources.Load<GameObject>("Prefabs/inimigos/Canvas") },
        { ENUM_PREFAB.defaultEnemy, Resources.Load<GameObject>("Prefabs/inimigos/defaultEnemy") },
        { ENUM_PREFAB.background1, Resources.Load<GameObject>("Prefabs/background/background1") },
        { ENUM_PREFAB.spaceship1Player, Resources.Load<GameObject>("Prefabs/player/Ship1Player") },
        { ENUM_PREFAB.spaceship2Player, Resources.Load<GameObject>("Prefabs/player/Ship2Player") },
        { ENUM_PREFAB.spaceship3Player, Resources.Load<GameObject>("Prefabs/player/Ship3Player") },
        { ENUM_PREFAB.audioSource, Resources.Load<GameObject>("Prefabs/audio/AudioSource") },
    };

    // Layers
    public const int LAYER_INIMIGOS = 8;

    // Geral Movement
    public static float angularDrag = 0.8f;
    public static float linearDrag = 0.3f;
    public static float impulseThreshold = 50;
    public static float rotationThreshold = 8;

    // Player
    public static int defaultLifeCount = 3;
    public static Color invulnerableColor = Color.red;
    public static float invulnerableTime = 3f;
    public static float respawnTime = 3f;
    public static bool playerIsActive = true;
    public static float defaultPlayerSpeed = 4f;

    // Enemies
    public static float inputImpulse = 2f;
    public static float minInputImpulse = 0.2f;
    public static float inputRotation = 0f;
    public static Color enemyColor = Color.white;
    public static Color targetColor = Color.red;

    // Typing System
    public static Dictionary<char, bool> usedChars = new Dictionary<char, bool>();
    public static List<string> TAGS = new List<string>();
    public static float checkKeysTime = 0.005f;

    // Wave Management
    private static int defeatedEnemiesCount;
    public static int DefeatedEnemiesCount
    {
        get { return defeatedEnemiesCount; }
        set
        {
            defeatedEnemiesCount = value;
            GUIController.RefreshGUI();
        }
    }

    private static int scoreCount;
    public static int ScoreCount
    {
        get { return scoreCount; }
        set
        {
            scoreCount = value;
            GUIController.RefreshGUI();
        }
    }
    public static float averageWPM;
    public static float averageAccuracy;
    public static int averageWordLength; 

    // Enemies Spawn
    public static float spawnEnemyTime = 0.5f;
    public static EnemyMovement.ENUM_INITIALEDGE ENUM_INITIALEDGE = EnemyMovement.ENUM_INITIALEDGE.right;

    public static void AddTag(string tag)
    {
        if (!TAGS.Contains(tag))
        {
            TAGS.Add(tag);
        }
    }

    public static void AddUsedChar(char letter)
    {
        usedChars[letter] = true;
    }

    public static void RemoveUsedChar(char letter)
    {
        usedChars[letter] = false;
    }

    public static void SetDefaultEnemySpawnEdge(EnemyMovement.ENUM_INITIALEDGE e)
    {
        ENUM_INITIALEDGE = e;
    }
}
