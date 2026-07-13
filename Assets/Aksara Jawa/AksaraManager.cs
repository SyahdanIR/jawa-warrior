using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AksaraManager : MonoBehaviour
{
    public static AksaraManager Instance;
    public int aksaraCount;
    public Text aksaraText;
    [SerializeField] GameObject secret;
    private bool secretDestroyed = false;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (aksaraCount == 7 && !secretDestroyed)
        {
            secretDestroyed = true;
            Destroy(secret);
        }
        aksaraText.text = "Aksara : " + aksaraCount.ToString() + "/7";
    }
    public void ResetAksara()
    {
        aksaraCount = 0;
    }
}
