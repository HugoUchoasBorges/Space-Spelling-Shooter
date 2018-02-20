using System.Collections.Generic;

public class Word {

	public string text { get; set; }
	public int length { get; private set; }
    public List<string> tags { get; set; }

    public Word() {
		this.text = "";
        this.length = 0;
        this.tags = new List<string>();
    }

	public Word(string texto, List<string> tags)
    {
		this.text = texto;
		this.length = this.text.Length;
		this.tags = tags;
	}

    public Word(string texto, string tag)
    {
        this.text = texto;
        this.length = this.text.Length;
        this.tags = new List<string>();
        this.tags.Add(tag);
    }
}
