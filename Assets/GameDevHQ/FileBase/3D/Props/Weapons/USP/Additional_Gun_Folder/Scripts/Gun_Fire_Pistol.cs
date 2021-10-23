using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Fire_Pistol : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smoke;

    [SerializeField]
    private ParticleSystem _bulletCasing;

    [SerializeField]
    private ParticleSystem _muzzleFlashSide;

    [SerializeField]
    private ParticleSystem _Muzzle_Flash_Front;

    private Animator _anim;

    [SerializeField]
    private AudioClip _gunShotAudioClip;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _bloodPrefab;

    public bool FullAuto;
    public bool hasAmmo = true;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(hasAmmo)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Shoot gun");
                if (FullAuto == false)
                {
                    _anim.SetTrigger("Fire");
                }

                if (FullAuto == true)
                {
                    _anim.SetBool("Automatic_Fire", true);
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (FullAuto == true)
                {
                    _anim.SetBool("Automatic_Fire", false);
                }

                if (FullAuto == false)
                {
                    _anim.SetBool("Fire", false);
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _anim.SetTrigger("Reload");
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                //out of ammo sound
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            //switching sound effect
            FullAuto = !FullAuto;
        }

        hasAmmo = AmmoManager.Instance.HasAmmo();
    }

    public void FireGunParticles()
    {
        Debug.Log("Fired gun particles");
        _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        GunFireAudio();
    }

    public void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }

    public void CombineFireAction()
    {
        ExecuteFire();
        DecreaseAmmo();
    }
    
    void DecreaseAmmo()
    {
        AmmoManager.Instance.DecreaseAmmo();
    }

    void ExecuteFire()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 0, QueryTriggerInteraction.Ignore))
        {
            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
            {
                GameObject instBlood = Instantiate(_bloodPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                instBlood.transform.SetParent(hit.transform);
                Destroy(instBlood, 0.5f);
                health.Damage(5);
            }
        }
    }
}
