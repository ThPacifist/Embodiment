using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : Controller
{
    [Header("Human Settings")]
    public bool boxHeld;
    public bool heavyBoxHeld;
    [HideInInspector]
    public Rigidbody2D box;
    string boxTag;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    //Sets the value of the held box
    public void SetHeldBox(Rigidbody2D rb, string inputTag)
    {
        box = rb;
        boxTag = inputTag;
        if (PlayerBrain.PB.prefabInstance == null)
        {
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, box.transform);
        }
        else if (rb == null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
        }
        else if (PlayerBrain.PB.prefabInstance != null)
        {
            Destroy(PlayerBrain.PB.prefabInstance);
            PlayerBrain.PB.prefabInstance = Instantiate(PlayerBrain.PB.IndicatorPrefab, box.transform);
        }
    }
}
