using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour 
{
    //var de velocidade
	float speed = 20.0F;
    //

    //var de velocidade da rotacao
    float rotationSpeed = 120.0F;
    //

    //prefab da bullet(tiro)
    public GameObject bulletPrefab;
    //get posicao da bullet
    public Transform bulletSpawn;

    void Update() {
        //movimentação com os Inputs da propria Engine(UNITY)
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;


        //deslocamento do player
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        //tiro
        if(Input.GetKeyDown("space"))
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*2000);

        }
    }

}
