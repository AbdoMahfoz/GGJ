using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerHealth : MonoBehaviour
{
    public int health = 240;

    bool hit = true;
    public GameObject flash;
    public SpriteRenderer sprite;
    Animator anim;
    Rigidbody2D myBody;

    public Color collideColor, collideColor2;
    // Audio
    public AudioClip axeHit,potionDrink;


    // UI
    public Slider healthSlider;

    // Manage Respawning
    void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value > health)
        {
            healthSlider.value -= 0.50f;
        }
        else if (healthSlider.value < health)
        {
            healthSlider.value = health;
        }
        if(transform.position.y <= -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void death()
    {
        myBody.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        Invoke("respawn", 1.0f);
    }
    public void TakeDamage(int damage)
    {
        if (hit)
        {
            StartCoroutine(playerHit());
            health -= damage;
        }
        if (health <= 0)
        {
            anim.SetBool("death",true); // Using set bool instead of the SetTrigger to stay on the same clip and not repeat.
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag== "FireBall")
        {
            TakeDamage(40);
            Destroy(target.gameObject);
        }
        if (target.tag == "Potion")
        {
            Destroy(target.gameObject);
            health += 50;
            if (health > 240)
                health = 240;
            soundManager.instance.playSfx(potionDrink, 0.5f);
        }
    }

    IEnumerator playerFlash()
    {
        for (int i = 0; i < 2; i++)
        {
            sprite.color = collideColor;// change player color to red. 
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < 4; i++)
        {
            sprite.color = collideColor2;// make player half visible.
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white; // return player to normal look.
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator playerHit()
    {
        soundManager.instance.playSfx(axeHit,.10f);
        FindObjectOfType<cameraShaking>().shakeItMed();
        flash.SetActive(true);
        hit = false;
        StartCoroutine(playerFlash());
        yield return new WaitForSeconds(1.3f);
        hit = true;
        flash.SetActive(false);
    }
}
