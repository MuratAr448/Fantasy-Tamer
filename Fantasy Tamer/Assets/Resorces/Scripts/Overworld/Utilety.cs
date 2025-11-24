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
        Filling = 0;
        while (Filling <= 10)
        {
            Debug.Log("Happened" + Filling);
            uiFill.fillAmount += 0.1f;
            Filling++;
            yield return new WaitForSeconds(0.1f);

            yield return null;
        }
        Debug.Log("Happened");
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
        
        switch (scene)
        {
            case ChoseScene.Town: SceneManager.LoadScene("Town"); break;
            case ChoseScene.Dongeon: SceneManager.LoadScene("Dungeon"); break;
            default: break;
        }
    }

}
