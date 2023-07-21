
using UnityEngine;

namespace GameFramework.Core
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        // Propiedad estática que proporciona acceso a la instancia única de la clase T.
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Si la instancia es nula, busca todos los objetos en la escena de tipo T.
                    T[] objs = FindObjectsOfType<T>();

                    if (objs.Length > 0)
                    {
                        // Si se encontró algún objeto de tipo T en la escena, toma el primero y lo asigna como la instancia única.
                        T instance = objs[0];
                        _instance = instance;
                    }
                    else
                    {
                        // Si no se encontraron objetos de tipo T en la escena, crea un nuevo objeto vacío.
                        GameObject go = new GameObject();
                        go.name = typeof(T).Name;

                        // Agrega un componente de tipo T al objeto y lo asigna como la instancia única.
                        _instance = go.AddComponent<T>();

                        // Marca el objeto para que no se destruya al cargar una nueva escena.
                        DontDestroyOnLoad(go);
                    }
                }

                // Devuelve la instancia única de la clase T.
                return _instance;
            }
        }
    }
}
