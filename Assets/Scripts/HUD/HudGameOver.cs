using UnityEngine;

public class HudGameOver : MonoBehaviour
{
   
    public static HudGameOver Instance;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
       
    }
    public void ShowGameOver() 
    { 
        canvasGroup.alpha = 1;
    }
}
