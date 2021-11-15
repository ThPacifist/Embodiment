using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    public GameObject Indicator;

    [SerializeField]
    SpecialInteractions interaction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Human") || collision.CompareTag("Bat"))
        {
            if (collision.CompareTag("Bat") && transform.parent.CompareTag("LBox"))
            {
                if (CheckBoundsForBat(collision))
                {
                    interaction = collision.GetComponent<SpecialInteractions>();
                    if (collision != null)
                    {
                        interaction.SetHeldBox(this.transform.parent.GetComponent<Rigidbody2D>(), this.transform.parent.tag);
                    }
                    Indicator.SetActive(true);
                }
            }
            else if(collision.CompareTag("Human"))
            {
                interaction = collision.GetComponent<SpecialInteractions>();
                if (collision != null)
                {
                    interaction.SetHeldBox(this.transform.parent.GetComponent<Rigidbody2D>(), this.transform.parent.tag);
                }
                Indicator.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interaction != null)
        {
            interaction.SetHeldBox(null, "");
            interaction = null;
            Indicator.SetActive(false);
        }
    }

    //Checks if player is within the created box to see if bat is above box
    bool CheckBoundsForBat(Collider2D plyCol)
    {
        Collider2D OuterCol = this.GetComponent<Collider2D>();
        Collider2D InnerCol = this.transform.parent.GetComponent<Collider2D>();

        Vector2 plyPoint = new Vector2(plyCol.bounds.center.x, plyCol.bounds.min.y);

        Vector2 tR = new Vector2(InnerCol.bounds.max.x, OuterCol.bounds.max.y);
        Vector2 bL = new Vector2(InnerCol.bounds.min.x, InnerCol.bounds.max.y);

        Debug.DrawLine(tR, bL);

        if (plyPoint.x < tR.x && plyPoint.x > bL.x && plyPoint.y < tR.y && plyPoint.y > bL.y - 0.01f)
        {
            Debug.Log("Inside Bounds");
            return true;
        }
        else
        {
            return false;
        }
    }
}
