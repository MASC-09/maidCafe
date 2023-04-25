using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClienteAtendido : MonoBehaviour
{
    private int clienteAtendido = 0;
    [SerializeField] private Text textoContador;
    [SerializeField] private string UI_TEXT = "Clientes Atendidos";

    void Start()
    {
        textoContador = GetComponent<Text>();
        ActualizaContadorTexto();
    }

    // Update is called once per frame
    void Update()
    {
        ActualizaContadorTexto();
    }

    public void atendido()
    {
        clienteAtendido++;
        ActualizaContadorTexto();
    }

    private void ActualizaContadorTexto()
    {
        if (textoContador != null)
        {
            textoContador.text = UI_TEXT + ": " + clienteAtendido.ToString();
        }
    }
}
