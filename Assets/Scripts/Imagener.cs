using System;
using UnityEngine;
using UnityEngine.UI;

public class Imagener : MonoBehaviour
{
    public Image imageComponent;
    public Sprite imagenManana;
    public Sprite imagenTarde;
    public Sprite imagenNoche;

    private void Start()
    {
        CambiarImagenSegunHoraDelDia();
    }

    private void Update()
    {
        // Con esto llamas la funcion de cambio de hora cada minuto we para que actualize la imagen
        if (DateTime.Now.Second == 0)
        {
            CambiarImagenSegunHoraDelDia();
        }
    }

    private void CambiarImagenSegunHoraDelDia()
    {
        int horaActual = DateTime.Now.Hour;

        if (horaActual >= 6 && horaActual < 12) // Mañana
        {
            imageComponent.sprite = imagenManana;
        }
        else if (horaActual >= 12 && horaActual < 18) // Tarde
        {
            imageComponent.sprite = imagenTarde;
        }
        else // Noche
        {
            imageComponent.sprite = imagenNoche;
        }
    }
}
