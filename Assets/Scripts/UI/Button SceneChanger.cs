using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSceneChanger : MonoBehaviour
{
    
    private Button button;
    [SerializeField] SceneChanger sceneChanger;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Q].wasPressedThisFrame)
        {
            
            callChanger();
        }
    }


    public void callChanger()
    {
        if (sceneChanger != null)
        {
            sceneChanger.LoadFinish();
            sceneChanger.JumperLevel();
            sceneChanger.LoadAsylum();
            sceneChanger.LoadAsylum();
            sceneChanger.LoadHouseTwo();
            sceneChanger.ObtacleHallway();
        }
    }
}
