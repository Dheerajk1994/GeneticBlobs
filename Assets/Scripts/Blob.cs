using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Blob : MonoBehaviour,IComparable<Blob>
{
    public int id = -1;
    public Vector2[] genes;
    public float fitness = 0.0f;
    private float distanceToGoal = 0.0f;
    private void Start()
    {
        //genes = new List<Vector2>();
    }

    public void CreateRandomGenes(int size)
    {
        fitness = 0;
        genes = new Vector2[size];
        for(int i = 0; i < size; ++i)
        {
            Vector2 randomPos = new Vector2(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
            genes[i] = randomPos;
        }
        //Debug.Log("blob genes count " + genes.Count);
    }

    public void CreateFromGivenGene(Vector2[] parentGene, float mutationChance, float mutationAmount, Vector2 startPos)
    {
        fitness = 0;
        this.transform.position = startPos;

        int copyPosition = UnityEngine.Random.Range(0, parentGene.Length);
        int copyAmount = UnityEngine.Random.Range(0, parentGene.Length);

        while(copyPosition < parentGene.Length && copyAmount > 0)
        {
            genes[copyPosition] = parentGene[copyPosition];
            if (UnityEngine.Random.Range(0, 100) <= mutationChance * 100) {
                genes[copyPosition] = new Vector2(
                    genes[copyPosition].x + UnityEngine.Random.Range(-mutationAmount, mutationAmount), 
                    genes[copyPosition].y + UnityEngine.Random.Range(-mutationAmount, mutationAmount));
            }
            copyPosition++;
            copyAmount--;
        }

    }

    public void UpdateMovement(int id, Vector2 goal)
    {
        //this.gameObject.GetComponent<Rigidbody2D>().AddForce(genes[id].normalized * 20f);
        float currentDistance = Vector2.Distance(goal, this.transform.position);
        if(currentDistance == distanceToGoal)
        {
            fitness -= 0.002f;
        }
        else if(currentDistance > distanceToGoal)
        {
            fitness -= 0.0003f;
        }
        else
        {
            fitness += 0.0001f;
        }
        distanceToGoal = currentDistance;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = genes[id].normalized * 20f;
    }

    public float CalculateFitness(Vector2 goal)
    {
        return (fitness += 1 / Vector2.Distance(this.transform.position, goal));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blob"))
            {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        }
    }

    public int CompareTo(Blob other)
    {
        if(other == null)
        {
            return 1;
        }
        else
        {
            //return this.fitness.CompareTo(other.fitness);
            return other.fitness.CompareTo(this.fitness);
        }
    }
}
    