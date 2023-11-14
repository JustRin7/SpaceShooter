using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAsteroids : MonoBehaviour
{
    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private int AsteroidValue;
    public void CreateAst() 
{
        for(int i = 0; i < AsteroidValue; i ++)
        {
            GameObject asteroid = Instantiate(AsteroidPrefab);
            asteroid.transform.position = transform.position;
            asteroid.transform.position = new Vector3( transform.position.x + i, transform.position.x + i, 0);
            asteroid.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            //asteroid.transform.position = new Vector3(i, i, 0);
            
        }
        
    }

}
