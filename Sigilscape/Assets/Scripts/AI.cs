using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
    public float targetDistance;
    public float moveDistance;
    public float enemyLookDistance;
    public float enemyMoveSpeed;
    public float attackDistance;
    public float damping;
    public GameObject target;
    Rigidbody rigidbody;
    Renderer rederer;

    // Use this for initialization
    void Start()
    {
        this.rederer = GetComponent<Renderer>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.target = GameObject.FindGameObjectWithTag("Player");
        moveDistance = moveDistance < enemyLookDistance ? moveDistance : enemyLookDistance;
        attackDistance = attackDistance < moveDistance ? attackDistance : moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(target.transform.position, transform.position);
        // and at same height
        if (targetDistance < enemyLookDistance)
        {
            //lookAtPlayer();
            this.transform.LookAt(target.transform.position);

            if (targetDistance < attackDistance)
            {
                //attack add public function to enemy script
            }
            else if(targetDistance < moveDistance)
            {
                moveTowardsPlayer();
            }
        }
    }

    //smooth look
    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    //move body towards player
    void moveTowardsPlayer()
    {
        rigidbody.AddForce(transform.forward * enemyMoveSpeed);
    }
}