using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingSystem : MonoBehaviour {

    private static Player player;
    public static List<Enemy> targets;

    // Use this for initialization
    void Start () {
        player = gameObject.GetComponent<Player>();
        StartCoroutine(CheckKeys());

        targets = new List<Enemy>();
    }

    public static void AddTarget(Enemy enemy)
    {
        targets.Add(enemy);
    }

    public static void RemoveTarget(Enemy enemy)
    {
        targets.Remove(enemy);
    }

    public static Enemy GetLastTarget()
    {
        if(targets.Count > 0)
            return targets[targets.Count - 1];
        return null;
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
                        player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_space);
                        break;

                    case '\n': // enter
                        print("Enter");
                        break;

                    case '\r': // return

                        foreach(Enemy enemy in targets)
                        {
                            if (enemy.IsTarget())
                            {
                                RestoreTarget(enemy);
                                UnlockTarget(enemy);
                                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_return);

                                break;
                            }
                        }

                        break;

                    default:
                        Enemy target = null;

                        if (GetLastTarget() != null && GetLastTarget().IsTarget())
                        {
                            target = GetLastTarget();
                        }
                        else
                        {
                            target = FindTarget(c);

                            if(target!= null)
                            {
                                LockTarget(target);
                            }
                        }

                        if(target != null)
                        {

                            if (target.text.text != null || target.text.text != "")
                            {
                                if (CheckLetter(c, target))
                                {
                                    WaveManager.AddTypedLetter(true);
                                    ConsumeLetter(c, target);
                                    player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key);
                                }
                                else
                                {
                                    WaveManager.AddTypedLetter();
                                    player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_lock);
                                }
                            }
                            else
                            {
                                WaveManager.AddTypedLetter();
                                player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_key_lock);
                            }
                        }

                        break;
                }
            }
            // Espera um tempo para verificar novamente as teclas
            yield return new WaitForSeconds(GlobalVariables.checkKeysTime);
        }
    }

    private Enemy FindTarget(char c)
    {
        foreach(Enemy enemy in targets)
        {
            if (enemy.IsTarget())
            {
                return enemy;
            }
        }

        Enemy targetEnemy = null;

        foreach (Enemy enemy in GameManager.Enemies)
        {
            if (targets.Contains(enemy))
                continue;

            if (enemy.text.text.StartsWith("" + c))
            {
                targetEnemy = enemy;
                break;
            }
        }

        return targetEnemy;
    }

    public static void LockTarget(Enemy target)
    {
        AddTarget(target);
        target.SetAsTarget();
        target.text.color = GlobalVariables.targetColor;
    }

    public static void UnlockTarget(Enemy target = null)
    {
        if(target == null)
        {
            target = GetLastTarget();
        }

        RemoveTarget(target);
        target.SetAsNotTarget();
    }

    public static void RestoreTarget(Enemy enemy = null)
    {
        if(enemy == null)
        {
            enemy = GetLastTarget();
        }
        enemy.text.color = GlobalVariables.enemyColor;
        enemy.text.text = enemy.word.text;
    }

    private bool CheckLetter(char c, Enemy target = null)
    {
        if(target == null)
        {
            target = GetLastTarget();
        }

        if (target.text.text.StartsWith("" + c))
            return true;
        
        return false;
    }

    private void ConsumeLetter(char c, Enemy target = null)
    {
        if(target == null)
        {
            target = GetLastTarget();
        }

        player.Shoot(target);
        target.text.text = target.text.text.Remove(0, 1);

        if (target.text.text == "")
        {
            StartCoroutine(GameManager.DestroyEnemy(target));

            WaveManager.EnemyDestroyed();

            GlobalVariables.RemoveUsedChar(target.word.text[0]);

            WaveManager.AddTypedLetter(true);

            UnlockTarget(target);
        }
    }
}
