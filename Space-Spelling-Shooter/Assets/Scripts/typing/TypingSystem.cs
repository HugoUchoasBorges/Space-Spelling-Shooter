using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingSystem : MonoBehaviour {

    public static GameObject target = null;
    private static Text text;
    public static string word;
    private static Player player;

    // Use this for initialization
    void Start () {
        player = gameObject.GetComponent<Player>();
        StartCoroutine(CheckKeys());
    }
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator CheckKeys()
    {
        while (true)
        {
            if (GameManager.GAME_ISPAUSED)
                yield return new WaitUntil(() => GameManager.GAME_ISPAUSED == false);

            if (!GlobalVariables.playerIsActive)
                yield return new WaitUntil(() => GlobalVariables.playerIsActive == true);

            foreach (char c in Input.inputString.ToUpper())
            {
                switch (c)
                {
                    case '\b': // backspace/delete
                        print("Backspace");
                        player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_space);
                        break;

                    case '\n': // enter
                        print("Enter");
                        break;

                    case '\r': // return
                        print("Return");
                        if (target)
                        {
                            RemoveTarget();
                            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);
                        }
                        break;

                    default:
                        FindTarget(c);
                        break;
                }
            }
            // Espera um tempo para verificar novamente as teclas
            yield return new WaitForSeconds(GlobalVariables.checkKeysTime);
        }
    }

    private void FindTarget(char c)
    {
        if (!target)
        {
            GameObject target;

            if (target = GameManager.FindTarget(c))
            {
                LockTarget(target);
            }
        }

        if (target)
        {
            if (ConsumeLetter(c))
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
            }
            else
            {
                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_lock);
            }
        }
        else
        {
            player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_lock);
        }
    }

    private static void LockTarget(GameObject target)
    {
        TypingSystem.target = target;
        text = TypingSystem.target.GetComponentInChildren<Text>();
        word = text.text;
        text.color = GlobalVariables.targetColor;
    }

    public static void RemoveTarget()
    {
        if (text)
        {
            text.color = GlobalVariables.enemyColor;
            text.text = word;
        }

        target = null;
        text = null;
        word = null;
    }

    private bool ConsumeLetter(char c)
    {
        Text text = target.GetComponentInChildren<Text>();

        if (text.text[0] == c)
        {
            text.text = text.text.Remove(0, 1);

            if (text.text == "")
            {
                GameManager.DestroyEnemy(target, word[0]);
                RemoveTarget();
            }
            return true;
        }
        return false;
    }

    public static void destroiInimigo(GameObject enemy)
    {
        LockTarget(enemy);
        GameManager.DestroyEnemy(target, word[0]);
        RemoveTarget();
    }
}
