using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [Tooltip("Index nebo název scény, kterou chcete načíst (Scene 1 = index 1)")]
    public string sceneToLoad = "1"; 

    // tuto metodu přidáte do OnClick u tlačítka
    public void RestartLevel()
    {
        // zkusíme načíst podle čísla, pokud se nepodaří, načteme podle jména
        int index;
        if (int.TryParse(sceneToLoad, out index))
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(sceneToLoad);
    }
}