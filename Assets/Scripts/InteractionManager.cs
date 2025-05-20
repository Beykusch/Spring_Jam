using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;

    public AmmoBox hoveredAmmoBox = null;

    public AudioSource LockedSFX;
    public AudioSource UnlockedSFX;
    public AudioSource PickedUpSFX;
    public GameObject LockedText;
    public GameObject PickUpKeyText;
    bool card1 = false, Door1Opened=false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) 
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if(objectHitByRaycast.gameObject.tag == "Card1" && Input.GetKeyDown(KeyCode.F))
            {
                PickedUpSFX.Play();
                PickUpKeyText.SetActive(true);
                card1 = true;
                Destroy(objectHitByRaycast.gameObject);
                StartCoroutine(SayPickedUpKey());
            }

            if (objectHitByRaycast.gameObject.tag == "Door1" && Input.GetKeyDown(KeyCode.F) && !Door1Opened && hit.distance < 10)
            {
                if (card1)
                {
                    Door1Opened = true;
                    UnlockedSFX.Play();
                    Vector3 posDoor1 = objectHitByRaycast.gameObject.transform.position;
                    objectHitByRaycast.gameObject.transform.Rotate(0, 0, 90);
                    objectHitByRaycast.gameObject.transform.position = new Vector3(posDoor1.x + 3, posDoor1.y, posDoor1.z - 3);
                }
                else
                {
                    LockedSFX.Play();
                    LockedText.SetActive(true);
                    StartCoroutine(SayLocked());
                }
            }

            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false && hit.distance < 3)
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }

                hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }
            
            //AmmoBox
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {
                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                    Destroy(objectHitByRaycast.gameObject);
                }
            }
            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
    IEnumerator SayLocked()
    {
        yield return new WaitForSeconds(1f);
        LockedText.SetActive(false);
    }
    IEnumerator SayPickedUpKey()
    {
        yield return new WaitForSeconds(1.5f);
        PickUpKeyText.SetActive(false);
    }
}
