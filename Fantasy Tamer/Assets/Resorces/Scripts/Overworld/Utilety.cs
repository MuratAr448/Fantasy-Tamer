using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Utilety : MonoBehaviour
{
    enum ChoseScene
    {
        Town,
        Dongeon
    }
    [SerializeField] private Image uiFill;
    public int Full;
    private int Filling;
    PlayerMovement player;
    [SerializeField] private Vector2 spawnPos;
    [SerializeField] private ChoseScene scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoadingZonePart1();
        }
    }
    private IEnumerator CoverScreen()
    {
        while (Filling >= 0)
        {

                uiFill.fillAmount = Mathf.InverseLerp(0, Full, Filling);
                Filling--;
                yield return new WaitForSeconds(1f);

            yield return null;
        }
        StartCoroutine(LoadingZonePart2());
    }
    private void LoadingZonePart1()
    {
        player = FindObjectOfType<PlayerMovement>();
        player.TPing = true;
        StartCoroutine(CoverScreen());
    }
    private IEnumerator LoadingZonePart2()
    {
        FindObjectOfType<PlayerMovement>().gameObject.transform.position = spawnPos;

        yield return new WaitForFixedUpdate();

        Debug.Log("SceneChange");
        /*
        switch (scene)
        {
            case ChoseScene.Town: SceneManager.LoadScene("Town"); break;
            case ChoseScene.Dongeon: SceneManager.LoadScene("Dungeon"); break;
            default: break;
        }*/
    }

}
