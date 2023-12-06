using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;

public class ArcherShoother : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCam;
    private ThirdPersonController tpController;
    private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private float AimSensivity;
    [SerializeField] private float NormalSensivity;
    [SerializeField] private GameObject crossAir;
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private  GameObject arrowPrefab;
    [SerializeField] private Transform arrowTransform;

    private Vector3 mouseWorldPos;
    private Transform target;
    private bool canShoot=true;
    public ParticleSystem hitEffect;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        tpController = GetComponent<ThirdPersonController>();
       
    }
    void Update()
    {

        mouseWorldPos = transform.position;
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 999, aimMask))
        {    
            mouseWorldPos = hit.point;
           
        }
       

       
            if (starterAssetsInputs.aim && tpController.Grounded && !starterAssetsInputs.sprint)
            {

            
                aimVirtualCam.gameObject.SetActive(true);
                tpController.SetSensitivty(AimSensivity);
                tpController.RotateOnMove(false);
                crossAir.SetActive(true);
                tpController._animator.SetBool("Aim", true);
                tpController._animator.SetLayerWeight(1, Mathf.Lerp(tpController._animator.GetLayerWeight(1), 1f, Time.deltaTime * 10));

                Vector3 AimTarget = mouseWorldPos;
                AimTarget.y = transform.position.y;
                Vector3 dir = (AimTarget - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 100);
    

            if (starterAssetsInputs.Shoot && canShoot)
                {
                Instantiate(hitEffect, mouseWorldPos, transform.rotation);
                Vector3 aimDir = (mouseWorldPos - arrowTransform.position).normalized;
                
                Instantiate(arrowPrefab, arrowTransform.position,Quaternion.LookRotation(aimDir,Vector3.up));
               
                    canShoot = false;
                    StartCoroutine(ResetShoot());
                }

            }
            else
            {
                aimVirtualCam.gameObject.SetActive(false);
                tpController.SetSensitivty(NormalSensivity);
                tpController.RotateOnMove(true);
                crossAir.SetActive(false);
                tpController._animator.SetBool("Aim", false);
                tpController._animator.SetLayerWeight(1, Mathf.Lerp(tpController._animator.GetLayerWeight(1), 0f, Time.deltaTime * 10));

            }
        
       

 
    }
    IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(1);
        if (starterAssetsInputs.Shoot)
        {
            starterAssetsInputs.Shoot = false;
            canShoot = true;
        }
    }
}
