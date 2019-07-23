using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text TextTitle = null;
    [SerializeField] private Text TextStart = null;
    [SerializeField] private Button ButtonNewGame = null;
    [SerializeField] private Button ButtonInfo = null;
    [SerializeField] private Button ButtonExit = null;
    [SerializeField] private GameObject PanelInfo = null;
    [SerializeField] private Button ButtonInfoClose = null;

    public static Menu Instanse { get; private set; } = null;

    private void Awake()
    {
        Instanse = this;
        
        ButtonNewGame.onClick.AddListener( OnNewGameClick );
        ButtonInfo.onClick.AddListener( OnInfoClick );
        ButtonInfoClose.onClick.AddListener(OnInfoCloseClick);
        ButtonExit.onClick.AddListener( OnExitClick );

        StartCoroutine(FadeLogo(0.01f));
        
        StartCoroutine(FadeSin(0.15f, 1f, 0.05f));
    }

    private IEnumerator FadeSin( float a, float b, float speed )
    {
        float fadeFrom = 0f;
        float result;
        while (true)
        {
            fadeFrom += speed;
            result = (Mathf.Sin(fadeFrom) + 1f) * 0.5f * (b - a) + a;
            TextTitle.color = new Color(255f / 255f, 118f / 255f, 0f, result );

            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator FadeLogo(float speed)
    {
        TextStart.gameObject.SetActive(true);
        TextTitle.gameObject.SetActive(false);
        ButtonNewGame.gameObject.SetActive(false);
        ButtonInfo.gameObject.SetActive(false);
        ButtonExit.gameObject.SetActive(false);

        float fadeFrom = 0f;
        while (fadeFrom < 1f)
        {
            fadeFrom += speed;
            TextStart.color = new Color(255f / 255f, 118f / 255f, 0f, fadeFrom);
            yield return null;
        }

        while (fadeFrom < 8f)
        {
            fadeFrom += speed;
            yield return null;
        }

        fadeFrom = 1f;
        while (fadeFrom > 0f)
        {
            fadeFrom -= speed;
            TextStart.color = new Color(255f / 255f, 118f / 255f, 0f, fadeFrom);
            yield return null;
        }

        yield return null;
        TextStart.gameObject.SetActive(false);
        TextTitle.gameObject.SetActive(true);
        ButtonNewGame.gameObject.SetActive(true);
        ButtonInfo.gameObject.SetActive(true);
        ButtonExit.gameObject.SetActive(true);
    }

    public void OnNewGameClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnInfoClick()
    {
        PanelInfo.SetActive(true);
    }

    public void OnInfoCloseClick()
    {
        PanelInfo.SetActive(false);
    }
    
    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
