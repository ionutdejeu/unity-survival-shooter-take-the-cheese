using Assets.Core.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

   
    public int damagePerShot = 20;                 
    public float timeBetweenBullets = 0.15f;       
    public float range = 100f;
    public bool canShoot = true;

    float timer;                                   
    Ray shootRay;                                  
    RaycastHit shootHit;                           
    int shootableMask;                             
    LineRenderer gunLine;                          
    Light gunLight;                                
    float effectsDisplayTime = 0.2f;               

    void Start()
    {
        
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();

         
    }


    void BlockShooting()
    {
        canShoot = false;
    }
    void UnBlockShooting()
    {
        canShoot = true;
    }
    
        
        
    void ShootHandler(MouseButtonInfo info)
    {
        timer += Time.deltaTime;

        if (canShoot && Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }
    
    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunLight.enabled = true;

        gunLine.enabled = true;
        gunLine.SetPosition(0, Vector3.zero);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

         
         
        gunLine.SetPosition(1, Vector3.forward * range);
        
    }
}