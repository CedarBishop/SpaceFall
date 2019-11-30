using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction {Up,Down, Left, Right }
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public float Speed = 2;
    public LayerMask wallLayer;
    public LayerMask tileLayer;
    public Vector3 target;
    AudioSource audioSource;
    public AudioClip moveClip;

    bool canMove;

    Rigidbody rigidbody;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = moveClip;
        canMove = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    private void Update()
    {
        GetInput();
       
    }


    void GetInput()
    {
        if (canMove == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(MoveTo(Direction.Up));
            if (!Physics.Raycast(new Ray(transform.position, Vector3.forward), 1, wallLayer))
            {
                StartCoroutine(MoveTo(Direction.Up));
            }
            
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!Physics.Raycast(new Ray(transform.position, Vector3.back), 1, wallLayer))
            {
                StartCoroutine(MoveTo(Direction.Down));
            }

        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!Physics.Raycast(new Ray(transform.position, Vector3.left), 1, wallLayer))
            {
                StartCoroutine(MoveTo(Direction.Left));
            }

        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!Physics.Raycast(new Ray(transform.position, Vector3.right), 1, wallLayer))
            {
                StartCoroutine(MoveTo(Direction.Right));
            }

        }
    }

    IEnumerator MoveTo (Direction direction)
    {
        canMove = false;
        switch (direction)
        {
            case Direction.Up:
                target = transform.position + Vector3.forward;
                break;
            case Direction.Down:
                target = transform.position + Vector3.back;
                break;
            case Direction.Left:
                target = transform.position + Vector3.left;
                break;
            case Direction.Right:
                target = transform.position + Vector3.right;
                break;
            default:
                target = transform.position;
                break;
        }

        target = new Vector3(Mathf.RoundToInt(target.x), target.y, Mathf.RoundToInt(target.z));

        audioSource.Play();

        while (Vector3.Distance(target,transform.position) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position,target, Speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        

        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position,Vector3.down),out hit,1, tileLayer))
        {
            if (hit.collider.GetComponent<UnWalkableTile>())
            {
                hit.collider.GetComponent<UnWalkableTile>().AddGravity();
                rigidbody.useGravity = true;

            }
        }

        canMove = true;

    }



}
