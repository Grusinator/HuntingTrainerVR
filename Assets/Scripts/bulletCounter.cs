using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class bulletCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletCountPrefab;

    [SerializeField]
    private GameObject bulletHolePrefab;

    private TMP_Text bulletCountText;

    // Start is called before the first frame update
    void Start()
    {
        bulletCountText = bulletCountPrefab.GetComponent<TMP_Text>();
        Debug.Log("Component Name: " + bulletCountText.name);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBulletCount();
    }

    void UpdateBulletCount()
    {
        if (bulletCountText != null)
        {
            int bulletCount = CountBulletHoles();
            bulletCountText.SetText("Bullet Count: " + bulletCount.ToString());
        }
    }

    int CountBulletHoles()
    {
        int count = 0;
        List<GameObject> children =new();
        gameObject.GetChildGameObjects(children);
        foreach (GameObject child in children)
        {
            // Debug statement to check the name of the child component
            Debug.Log("Child Component Name: " + child.name);

            if (child.CompareTag("BulletHole"))
            {
                count++;
            }
        }
        return count;
    }

}
