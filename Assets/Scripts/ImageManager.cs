using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CambiarImagenes : MonoBehaviour
{
    public Image[] imagenes; // Lista de im�genes que deseas cambiar.
    public Sprite[] nuevasImagenes; // Lista de nuevas im�genes.

    private int currentIndex = 0;

    private void Start()
    {
        // Comenzar una rutina para cambiar las im�genes cada 3 segundos.
        StartCoroutine(ChangeImagesRoutine());
    }

    private IEnumerator ChangeImagesRoutine()
    {
        while (true)
        {
            // Cambiar la imagen actual al �ndice actual.
            imagenes[currentIndex].sprite = nuevasImagenes[currentIndex];

            // Esperar 3 segundos antes de cambiar a la siguiente imagen.
            yield return new WaitForSeconds(3.0f);

            // Avanzar al siguiente �ndice y reiniciar si llegamos al final.
            currentIndex = (currentIndex + 1) % imagenes.Length;
        }
    }
}
