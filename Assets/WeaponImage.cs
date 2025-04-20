using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponImage : MonoBehaviour
{
    public RawImage rifleImage, pistolImage;


    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).name == "M4WH")
            {
                rifleImage.gameObject.SetActive(true);
                pistolImage.gameObject.SetActive(false);
            }
            else if (transform.GetChild(0).name == "HandWithGun2")
            {
                pistolImage.gameObject.SetActive(true);
                rifleImage.gameObject.SetActive(false);
            }
        }
    }
}
