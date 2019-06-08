using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int geneSize;
    [SerializeField] private GameObject BlobPrefab;
    [SerializeField] private Transform startingPos;
    [SerializeField] private Transform goalPos;

    private List<Blob> blobs;

    private int populationSize;
    private float matingChance;
    private float mutationChance;
    private float mutationAmount;
    private float stepInterval;

    private bool isInCycle = false;
    private int currentGeneIndex = 0;
    private float currentStepInterval = 0.0f;

    public void StartInitialPopulation(int _populationSize, float _matingChance, float _mutationChance, float _mutationAmount, float _stepInterval) {
        blobs = new List<Blob>();
        populationSize = _populationSize;
        matingChance = _matingChance;
        mutationChance = _mutationChance;
        mutationAmount = _mutationAmount;
        stepInterval = _stepInterval * 0.01f;

        for(int i = 0; i < populationSize; ++i)
        {
            Blob b = Instantiate(BlobPrefab).GetComponent<Blob>();
            b.gameObject.transform.position = startingPos.position;
            b.CreateRandomGenes(geneSize);
            blobs.Add(b);
        }
        isInCycle = true;
        Debug.Log("time scale " + Time.timeScale);
    }

    private void Update()
    {
        currentStepInterval += Time.deltaTime;
        if (isInCycle && currentStepInterval>= stepInterval)
        {
            if(currentGeneIndex < geneSize)
            {
                foreach(Blob b in blobs)
                {
                    b.UpdateMovement(currentGeneIndex, goalPos.position);
                }
                currentGeneIndex++;
                if(currentGeneIndex >= geneSize)
                {
                    Time.timeScale = 0;
                    StartCoroutine(GetReadyNextCycle());
                }
            }
            currentStepInterval = 0.0f;
        }
    }

    private IEnumerator GetReadyNextCycle()
    {
        foreach(Blob b in blobs)
        {
            b.CalculateFitness(goalPos.position);
        }

        List<Blob> tempBlobs = blobs;
        tempBlobs.Sort();
        Blob bestBlob = null;
        Blob secondBestBlob = null;

        bestBlob = tempBlobs[0];
        secondBestBlob = tempBlobs[1];

        Vector2[] newGene = MateBestBlobs(bestBlob, secondBestBlob);

        foreach (Blob b in blobs)
        {
            b.CreateFromGivenGene(newGene, mutationChance, mutationAmount, startingPos.position);
        }

        //List<Blob> blobsToRemove = new List<Blob>();
        //int blobsToExterminateStartPos = blobs.Count - (int)(blobs.Count * 0.25f);
        //
        //for(int i = blobsToExterminateStartPos; i < blobs.Count; ++i)
        //{
        //    blobsToRemove.Add(tempBlobs[i]);
        //}
        //
        //foreach (Blob b in blobsToRemove)
        //{
        //    blobs.Remove(b);
        //    Destroy(b.gameObject);
        //}

        currentGeneIndex = 0;
        currentStepInterval = 0;
        Time.timeScale = 1.0f;

        yield return null;
    }

    Vector2[] MateBestBlobs(Blob blob1, Blob blob2)
    {
        Vector2[] newGene = new Vector2[geneSize];
        for(int i = 0; i < geneSize; ++i)
        {
            if(i % 2 == 0)
            {
                newGene[i] = blob1.genes[i];
            }
            else
            {
                newGene[i] = blob2.genes[i];
            }
        }
        return newGene;
    }


}
