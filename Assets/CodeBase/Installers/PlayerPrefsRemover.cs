using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsRemover : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
}
