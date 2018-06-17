﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour {

	NavMeshAgent navMeshAgent;
	NavMeshPath path;
	public float timeForNewPath;
	Animator anim;
	bool inCoRoutine;
	Vector3 target;
	bool validPath;

	void Start ()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
		path = new NavMeshPath();
		anim = GetComponent<Animator>();
	}

	void FixedUpdate ()
	{
		if (!inCoRoutine)
			StartCoroutine(DoSomething());
	}

	Vector3 getNewRandomPosition ()
	{
		float x = Random.Range(-20, 20);
		float z = Random.Range(-20, 20);

		Vector3 pos = new Vector3(x, 0, z);
		return pos;
	}

	IEnumerator DoSomething ()
	{
		inCoRoutine = true;
		yield return new WaitForSeconds(timeForNewPath);
		GetNewPath();
		validPath = navMeshAgent.CalculatePath(target, path);
		//if (!validPath) Debug.Log("Found an invalid Path");

		while (!validPath)
		{
			yield return new WaitForSeconds(0.01f);
			GetNewPath();
			validPath = navMeshAgent.CalculatePath(target, path);
			anim.SetBool("isWalking", true);
		}
		inCoRoutine = false;
	}

	void GetNewPath ()
	{
		target = getNewRandomPosition();
		navMeshAgent.SetDestination(target);
	}

	private void OnCollisionEnter(Collision collision)
    {
    	Debug.Log("Collisiooooooooooooon with: " + collision.gameObject.name);
    }

}