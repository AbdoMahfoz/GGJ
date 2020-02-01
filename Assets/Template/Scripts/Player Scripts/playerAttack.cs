using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    Animator anim;
    bool activeTimeToReset;
    float defaultComboTimer = 0.2f, currentComboTimer;

    float coolDown;// for air sword attack
    public GameObject arrow;
    bool canShoot;
    int arrowCount = 10;
    int combo;
    public Transform arrowPos;

    public Transform attackPos;
    public LayerMask enemyLayer;

    public float attackRange;
    public int damage;
    
    // Sound
    public AudioClip bowShot, swordHit;
    public AudioClip[] swordClips;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void bowAttack()
    {
        if (Input.GetKeyDown(KeyCode.K) && canShoot == true)
        {
            if (arrowCount > 0)
            {
                anim.SetTrigger("bowShot");
                canShoot = false;
                arrowCount--;
            }
        }
    }

    public void arrowSpawn()
    {
        Instantiate(arrow, arrowPos.position, Quaternion.identity);
        soundManager.instance.playSfx(bowShot, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        coolDownTimer();
        resetComboState();
        swordAttack();
        bowAttack();
    }

    void swordAttack()
    {
        if (Input.GetKeyDown(KeyCode.X) && !anim.GetBool("jumping"))
        {
            if(combo <= 2)
            {
                anim.SetBool("attacking1", true);
                activeTimeToReset = true;
                currentComboTimer = defaultComboTimer;


                // Attack
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange,enemyLayer);

                for(int i = 0; i < enemies.Length; i++)
                {
                    if(enemies[i].GetComponent<skeletonHealth>().health > 0)
                    {
                        enemies[i].GetComponent<skeletonHealth>().takeDamage(damage);
                        soundManager.instance.playSfx(swordHit, Random.Range(0.1f,0.3f));
                    }
                }

                soundManager.instance.playSfx(swordClips[combo], Random.Range(0.2f,0.3f));// Miss attack 
            }
            else
            {
                anim.SetBool("attacking1", false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.X) && anim.GetBool("jumping")&& coolDown == 0)
        {
            anim.SetBool("airAttack", true);
            coolDown = 1;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponent<skeletonHealth>().health > 0)
                {
                    enemies[i].GetComponent<skeletonHealth>().takeDamage(damage+5);
                    soundManager.instance.playSfx(swordHit, Random.Range(0.1f, 0.3f));
                }
            }

            soundManager.instance.playSfx(swordClips[0], Random.Range(0.2f, 0.3f)); // Miss attack 
        }
    }

    void coolDownTimer()
    {
        if (coolDown > 0)
        {
            anim.SetBool("airAttack", false);
            coolDown -= Time.deltaTime;
        }
        if (coolDown < 0)
        {
            coolDown =0;
        }
    }

    public void increaseComboNumber()
    {
        combo++;
    }
    public void resetCombo()
    {
        combo = 0;
        canShoot = true;
    }

    public void resetComboState()
    {
        if (activeTimeToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if(currentComboTimer <= 0)
            {
                anim.SetBool("attacking1", false);
                activeTimeToReset = false;
                currentComboTimer = defaultComboTimer; 
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
