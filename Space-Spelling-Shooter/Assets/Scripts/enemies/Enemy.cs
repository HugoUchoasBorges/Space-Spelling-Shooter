using UnityEngine;
using UnityEngine.UI;

public class Enemy : GameCharacter {

    public Word word { get; protected set; }

    public GameObject canvas { get; protected set; }
    public GameObject panel { get; protected set; }
    public Text text { get; protected set; }
    public Slider life { get; protected set; }

    protected EnemyMovement movement;

    protected void Spawn()
    {
        // Getting a word
        word = WordGenerator.RequestsWord();

        if (word == null)
        {
            Destroy(gameObject);
            return;
        }

        canvas = Instantiate(GlobalVariables.prefab_dict[GlobalVariables.ENUM_PREFAB.canvas]) as GameObject;
        canvas.transform.SetParent(transform);

        text = canvas.GetComponentInChildren<Text>();
        life = canvas.GetComponentInChildren<Slider>();

        panel = text.transform.parent.gameObject;

        text.text = word.text;
        movement = gameObject.AddComponent<EnemyMovement>();

        panel.AddComponent<FollowObject>();
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();

        Spawn();
        InitializesAudios(GlobalVariables.audio_enemy);
    }
	
	// Update is called once per frame
	void Update () {
		
        RefreshLife();
	}

    private void RefreshLife(){
        
        Image fill = life.transform.GetChild(1).GetComponentInChildren<Image>();
        life.value = text.text.Length / ((float)word.text.Length);

        if (life.value < 0.35f)
        {
            fill.color = Color.red;
        }
        else if(life.value < 0.7f)
        {
            fill.color = Color.yellow;
        }else{
            fill.color = Color.green;
        }
    }
}
