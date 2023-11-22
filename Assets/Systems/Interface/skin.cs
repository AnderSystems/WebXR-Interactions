using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skin : MonoBehaviour
{
    static skin instance;
    public static skin main()
    {
        if (!instance)
        {
            instance = FindFirstObjectByType<skin>();
        }

        return instance;
    }

    public Button AR_BUTTON;
    public Button VR_BUTTON;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
