using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverPersistent : MonoBehaviour
{
    // Start is called before the first frame update
    public static RecoverPersistent Instance;
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
        DontDestroyOnLoad(gameObject);
    }
}
