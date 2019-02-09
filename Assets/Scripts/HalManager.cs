using UnityEngine;
using UnityEngine.SceneManagement;

public class HalManager : MonoBehaviour {
    public AudioSource suaraBtn;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void SuaraTombol()
    {
        suaraBtn.PlayOneShot(suaraBtn.clip);
    }

    public void Permainan2P()
    {
        SceneManager.LoadScene("Main2P");
        Destroy(gameObject);
        Time.timeScale = 1;
        return;
    }
    public void PermainanCPU()
    {
        SceneManager.LoadScene("MainCPU");
        Destroy(gameObject);
        Time.timeScale = 1;
        return;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu"); ;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
