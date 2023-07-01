using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingImageLoadingScreenScript : MonoBehaviour
{
    [SerializeField]
    Sprite[] images;

    bool _canChange = true;

    int _index;


    void Update()
    {
        if (_canChange)
        {
            StartCoroutine(ChangeImage());
        }
    }

    IEnumerator ChangeImage()
    {
        _canChange = false;

        yield return new WaitForSeconds(0.8f);

        _index = Random.Range(0, images.Length);

/*        if (_index < images.Length - 1)
        {
            _index++;
        }
        else
        {
            _index = 0;
        }*/

        GetComponent<Image>().sprite = images[_index];

        _canChange = true;
    }
}
