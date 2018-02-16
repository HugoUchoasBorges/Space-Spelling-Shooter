using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowObject : MonoBehaviour {

    protected GameObject trackObject;
    protected Dictionary<string,Vector3> offset;

    private float raio;

    private void Awake()
    {
        trackObject = gameObject.transform.parent.transform.parent.gameObject;
        raio = trackObject.GetComponent<CircleCollider2D>().radius;
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackObject.GetComponent<CircleCollider2D>().bounds.center);
    }

    private void Start()
    {

        offset = new Dictionary<string, Vector3>()
        {
            {"left", Vector3.left * 2*raio},
            {"right", Vector3.right * 2*raio},
            {"up", Vector3.up * raio},
            {"down", Vector3.down * raio},
            {"default", Vector3.down * raio}

        };

        trackObject.GetComponentInChildren<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackObject.GetComponent<CircleCollider2D>().bounds.center + offset["default"]/1.5f);

        verificaVisibilidade();
    }

    // Método para manter o texto do inimigo visível na tela
    private void verificaVisibilidade()
    {
        //Vector2 posicao = Camera.main.ScreenToWorldPoint(transform.position);
        Vector2 posicao = trackObject.transform.position;
        
        if (Mathf.Abs(posicao.x) >= GeradorDeArestas.bottomRightCorner.x - raio / 2)
            if (posicao.x >= 0)
            {
                offset["default"] = offset["left"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
                GetComponent<RectTransform>().Rotate(Vector3.forward, 90);
            }
            else
            {
                offset["default"] = offset["right"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
                GetComponent<RectTransform>().Rotate(Vector3.forward, -90);
            }

        else if (Mathf.Abs(posicao.y) >= GeradorDeArestas.upperRightCorner.y - raio / 2)
            if (posicao.y >= 0)
            {
                offset["default"] = offset["down"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
            }
            else
            {
                offset["default"] = offset["up"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
            }

        else
        {
            offset["default"] = offset["down"];
            GetComponent<RectTransform>().rotation = Quaternion.identity;
        }
    }
}
