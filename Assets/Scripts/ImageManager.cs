using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CambiarImagenes : MonoBehaviour
{
    public Image[] imagenes; // Lista de imágenes que deseas cambiar.
    public Sprite[] nuevasImagenes; // Lista de nuevas imágenes.

    private int currentIndex = 0;

    private void Start()
    {
        // Comenzar una rutina para cambiar las imágenes cada 3 segundos.
        StartCoroutine(ChangeImagesRoutine());
    }

    private IEnumerator ChangeImagesRoutine()
    {
        while (true)
        {
            // Cambiar la imagen actual al índice actual.
            imagenes[currentIndex].sprite = nuevasImagenes[currentIndex];

            // Esperar 3 segundos antes de cambiar a la siguiente imagen.
            yield return new WaitForSeconds(3.0f);

            // Avanzar al siguiente índice y reiniciar si llegamos al final.
            currentIndex = (currentIndex + 1) % imagenes.Length;
        }
    }
}
