using System.Collections.Generic;

public class Palavra {

	public string texto { get; set; }
	public int tamanho { get; private set; }
    public List<string> tags { get; set; }
    public int pontos { get; protected set; }

    public Palavra() {
		this.texto = "";
        this.tamanho = 0;
        this.pontos = 0;
        this.tags = new List<string>();
    }

	public Palavra(string texto, List<string> tags)
    {
		this.texto = texto;
		this.tamanho = this.texto.Length;
		this.tags = tags;
		this.pontos = this.tamanho * 10;
	}

    public Palavra(string texto, string tag)
    {
        this.texto = texto;
        this.tamanho = this.texto.Length;
        this.tags = new List<string>();
        this.tags.Add(tag);
        this.pontos = this.tamanho * 10;
    }
}
