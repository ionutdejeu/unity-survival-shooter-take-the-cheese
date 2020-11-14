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
    bool shoulFire = false;

    void Start()
    {
        
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        PlayerInputController.instance.pressed += StartShooting;
        PlayerInputController.instance.released += StopShooting;


    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (canShoot && shoulFire && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void StopShooting(PointerActionInfo info)
    {
        shoulFire = false;
    }
    void StartShooting(PointerActionInfo info)
    {
           
        shoulFire = true;
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
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        gunLine.SetPosition(1, transform.forward * range);
        
    }
}