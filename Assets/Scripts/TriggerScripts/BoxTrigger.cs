using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    [SerializeField]
    SpecialInteractions interaction;

    [SerializeField]
    BatController bat;
    [SerializeField]
    HumanController human;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Human") || collision.CompareTag("Bat"))
        {
            if (collision.CompareTag("Bat") && transform.parent.CompareTag("LBox"))
            {
                if (CheckBoundsForBat(collision))
                {
                    bat = collision.GetComponent<BatController>();
                    if (collision != null)
                    {
                        bat.SetHeldBox(this.transform.parent.GetComponent<Rigidbody2D>(), this.transform.parent.tag);
                    }
                }
            }
            else if(collision.CompareTag("Human"))
            {
                if (CheckBoundsForHuman(collision))
                {
                    human = collision.GetComponent<HumanController>();
                    if (!human.boxHeld)
                    {
                        if (collision != null)
                        {
                            human.SetHeldBox(this.transform.parent.GetComponent<Rigidbody2D>(), this.transform.parent.tag);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (bat != null)
        {
            if (bat.box == this.transform.parent.GetComponent<Rigidbody2D>())
            {
                bat.SetHeldBox(null, "");
                bat = null;
            }
        }

        if (human != null)
        {
            if (human.box == this.transform.parent.GetComponent<Rigidbody2D>())
            {
                human.SetHeldBox(null, "");
                human = null;
            }
        }
    }

    //Checks if player is within the created box to see if bat is above box
    bool CheckBoundsForBat(Collider2D plyCol)
    {
        Collider2D OuterCol = this.GetComponent<Collider2D>();
        Collider2D InnerCol = this.transform.parent.GetComponent<Collider2D>();

        Vector2 plyPoint = new Vector2(plyCol.bounds.center.x, plyCol.bounds.min.y);

        Vector2 tR = new Vector2(InnerCol.bounds.max.x, OuterCol.bounds.max.y);
        Vector2 bL = new Vector2(InnerCol.bounds.min.x, InnerCol.bounds.max.y - 0.02f);

        if (plyPoint.x < tR.x && plyPoint.x > bL.x && plyPoint.y < tR.y && plyPoint.y > bL.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckBoundsForHuman(Collider2D plyCol)
    {
        Collider2D OuterCol = this.GetComponent<Collider2D>();
        Collider2D InnerCol = this.transform.parent.GetComponent<Collider2D>();

        Vector2 plyPoint = new Vector2(plyCol.bounds.center.x, plyCol.bounds.min.y);

        Vector2 tR = new Vector2(OuterCol.bounds.max.x, InnerCol.bounds.max.y - 0.5f);
        Vector2 bL = new Vector2(OuterCol.bounds.min.x, InnerCol.bounds.min.y - 0.2f);

        Debug.DrawLine(tR, bL);

        if (plyPoint.y < tR.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Collider2D OuterCol = this.GetComponent<Collider2D>();
        Collider2D InnerCol = this.transform.parent.GetComponent<Collider2D>();

        Vector2 tR = new Vector2(InnerCol.bounds.max.x, OuterCol.bounds.max.y);
        Vector2 bL = new Vector2(InnerCol.bounds.min.x, InnerCol.bounds.max.y - 0.02f);

        Vector3 center = new Vector3((tR.x + bL.x) / 2, (tR.y + bL.y) / 2, 0);
        Vector3 size = new Vector3(Mathf.Abs(tR.x - bL.x), Mathf.Abs(tR.y - bL.y), 0);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);

        tR = new Vector2(OuterCol.bounds.max.x, InnerCol.bounds.max.y - 0.5f);
        bL = new Vector2(OuterCol.bounds.min.x, InnerCol.bounds.min.y - 0.2f);

        center = new Vector3((tR.x + bL.x) / 2, (tR.y + bL.y) / 2, 0);
        size = new Vector3(Mathf.Abs(tR.x - bL.x), Mathf.Abs(tR.y - bL.y), 0);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
