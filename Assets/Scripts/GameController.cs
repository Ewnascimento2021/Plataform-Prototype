using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private Rigidbody2D projectile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartProjetile();
    }

    private void StartProjetile()
    {
        if (Input.GetButtonDown("Flechas"))
        {
            Instantiate(projectile, spawn.position, spawn.rotation);
        }
    }


    //if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //        }
}
