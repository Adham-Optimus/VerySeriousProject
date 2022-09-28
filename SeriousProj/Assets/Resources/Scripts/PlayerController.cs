using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float fullHP;
    public int damage;
    public float radius;
    public Animator animator;
    public LayerMask playerL;
    public Image hpImage;
    //public Transform pairPlayer;
    public bool isFighting = false;
    public bool hasBeating = false;
    public PlayerController pairPlayerController;
    public float hp;

    private NavMeshAgent agent;
    private Collider[] hitColliders;
    void Start()
    {
        fullHP = Random.Range(10, 17);
        damage = Random.Range(2, 4);
        hp = fullHP;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2;
        hitColliders = new Collider[2];
    }



    void Update()
    {
        if (!pairPlayerController)
        {
            hitColliders = Physics.OverlapSphere(transform.position, radius, playerL);

            if (hitColliders.Length >= 2)
            {
                pairPlayerController = hitColliders[1].GetComponent<PlayerController>();
                if (pairPlayerController.pairPlayerController == null)
                {
                    pairPlayerController.pairPlayerController = this;
                }
                else
                {
                    pairPlayerController = null;
                }
            }
        }
        if (pairPlayerController && isFighting == false)
        {
            agent.SetDestination(pairPlayerController.transform.position) ;
            Vector3 distance = transform.position - pairPlayerController.transform.position;
            if (distance.magnitude <= 2)
            {
                isFighting = true;
            }
        }
        if (isFighting)
        {
            if (!hasBeating)
            {
                StartCoroutine(Beating());
            }
        }
    }

    private IEnumerator Beating()
    {
        hasBeating = true;
        yield return new WaitForSeconds(1);
        if(pairPlayerController)
        {
            animator.SetBool("canBeat", true);
            pairPlayerController.TakeDamage(damage);
        }
        hasBeating = false;
    }

    public void TakeDamage(int _damage)
    {
        Debug.Log(_damage);

        hp -= _damage;
        hpImage.fillAmount = hp / fullHP;
        if (hp <= 0)
        {
            if (pairPlayerController)
            {
                pairPlayerController.animator.SetBool("canBeat", false);
                pairPlayerController.isFighting = false;
                pairPlayerController.pairPlayerController = null;
            }
            StartCoroutine(Died());
        }
    }

    private IEnumerator Died()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    

    /*void Update()
    {
        if (!pairPlayer)
        {
            hitColliders = Physics.OverlapSphere(transform.position, radius, playerL);
            
            if (hitColliders.Length >= 2)
            {
                pairPlayerController = hitColliders[1].GetComponent<PlayerController>();
                if (pairPlayerController.pairPlayer == null)
                {
                    pairPlayerController.pairPlayer = transform;
                    pairPlayer = pairPlayerController.transform;
                    pairPlayerController.pairPlayerController = this;
                }
            }
        }
        if (pairPlayer && isFighting == false)
        {
            agent.SetDestination(pairPlayer.position);
            Vector3 distance = transform.position - pairPlayer.position;
            if (distance.magnitude <= 2)
            {
                isFighting = true;
            }
        }
        if (isFighting)
        {
            if (!hasBeating)
            {
                StartCoroutine(Beating());
            }
        }
    }

    private IEnumerator Beating()
    {
        hasBeating = true;
        yield return new WaitForSeconds(1);

        
        {
            animator.SetBool("canBeat", true);
            pairPlayerController.TakeDamage(damage);
        }
        hasBeating = false;
    }

    public void TakeDamage(int _damage)
    {
        Debug.Log(_damage);

        hp -= _damage;
        hpImage.fillAmount = hp/fullHP;
        if (hp <= 0)
        {
            //if (pairPlayerController)
            {
                pairPlayerController.animator.SetBool("canBeat", false);
                pairPlayerController.pairPlayer = null;
                pairPlayerController.isFighting = false;
            }
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }*/

}
