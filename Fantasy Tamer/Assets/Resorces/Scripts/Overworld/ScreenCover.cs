using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCover : MonoBehaviour
{
    private PlayerMovement Player;
    private int Filling;
    [SerializeField] private Image uiFill;
    private void Start()
    {
        uiFill = FindObjectOfType<LoadingScreen>().gameObject.GetComponent<Image>();
        Player = FindObjectOfType<PlayerMovement>();
        Player.cam = Camera.main;
        Player.cam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.cam.transform.position.z);
        if (Player.TPing)
        {
            StartCoroutine(CoverScreen());
        }
        else
        {
            uiFill.transform.gameObject.SetActive(false);
        }
    }
    private IEnumerator CoverScreen()
    {
        Filling = 10;
        while (Filling >= 0)
        {
            uiFill.fillAmount -= 0.1f;
            Filling--;
            yield return new WaitForSeconds(0.1f);

            yield return null;
        }
        Debug.Log("Happened");
        LoadingZonePart1();
    }
    private void LoadingZonePart1()
    {
        Player.TPing = false;
    }
}
