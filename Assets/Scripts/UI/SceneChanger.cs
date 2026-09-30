using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private bool Finish;
    [SerializeField] private bool Death;
    [SerializeField] private bool HouseTwo;
    [SerializeField] private bool LongHallWway;
    [SerializeField] private bool ObstacleHallWay;
    [SerializeField] private bool Jumper;
    [SerializeField] private bool Asylum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("First House");
    }
    public void LoadFinish()
    {
        if (Finish == true)
        {
            SceneManager.LoadScene("Finish Screen");
        }

    }
    public void LoadDeath()
    {
        if (Death == true)
        {
            SceneManager.LoadScene("Death Screen");
        }
    }
    public void LoadHouseTwo()
    {
        if (HouseTwo == true)
        {
            SceneManager.LoadScene("Second House");
        }
    }

    public void LoadLongHallway()
    {
        if (LongHallWway == true)
        {
            SceneManager.LoadScene("First HallWay");
        }
    }

    public void ObtacleHallway()
    {
        if(ObstacleHallWay == true)
        {
            SceneManager.LoadScene("Runner Hallway");
        }
    }
    public void JumperLevel()
    {
        if(Jumper == true)
        {
            SceneManager.LoadScene("Jumper");
        }
    }
    public void LoadAsylum()
    {
        if(Asylum == true)
        {
            SceneManager.LoadScene("Asylum");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadFinish();
            LoadDeath();
            LoadHouseTwo();
            LoadLongHallway();
            ObtacleHallway();
            JumperLevel();
            LoadAsylum();
        }

    }
}
