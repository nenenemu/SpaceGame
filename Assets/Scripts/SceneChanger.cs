using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneScript2()
    {
        SceneManager.LoadScene("SecoundScene");
    }

    public void SceneScript3()
    {
        SceneManager.LoadScene("ThirdScene");
    }

    public void SceneScript4()
    {
        SceneManager.LoadScene("FourScene");
    }

    public void SceneScript5()
    {
        SceneManager.LoadScene("FiveScene");
    }

    public void SceneScript6()
    {
        SceneManager.LoadScene("SixScene");
    }

    public void TitleScript()
    {
        SceneManager.LoadScene("TitleScene");
    }





    public void NextGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NextZukan()
    {
        SceneManager.LoadScene("ZukanScene");
    }

    public void NextNagameru()
    {
        SceneManager.LoadScene("NagameruScene");
    }
}
