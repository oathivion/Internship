// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;




// public class Damage : MonoBehaviour

// {
    
//     public Player player_health;
//     public float damage;    
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private void OnCollisionEnter2D(Collider2D other)
//     {
//         Debug.Log("Triggered with: " + other.gameObject.name);

//         if (other.gameObject.CompareTag("Player"))
//         {
//             Debug.Log("Damage");
//             player_health.currentHealth -= damage;
//         }
//     }
// }
