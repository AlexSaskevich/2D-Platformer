using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Patrolling : MonoBehaviour
{
    private const string FlipRight = "FlipRight";
    private const string FlipLeft = "FlipLeft";

    private Animator _animator;
    private float _patrolLength = 4.0f;
    private bool _isAlive = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Patrol());
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator Patrol()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[0].length);

        while (_isAlive)
        {
            yield return StartCoroutine(MoveToTarget(transform.position + Vector3.left * _patrolLength));

            _animator.Play(FlipRight);
            yield return waitForSeconds;

            yield return StartCoroutine(MoveToTarget(transform.position + Vector3.right * _patrolLength));

            _animator.Play(FlipLeft);
            yield return waitForSeconds;
        }
    }
}