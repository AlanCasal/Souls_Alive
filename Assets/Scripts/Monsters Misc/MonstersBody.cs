using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersBody : MonoBehaviour {
    string typeOfDamage;

    public void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == Layers.player) {
            switch (info.gameObject.tag) {
                case "Untagged":
                    typeOfDamage = "player";
                    switch (transform.parent.tag) {
                        case "hunter":     transform.parent.GetComponent<Hunter>().        ReceiveDamage(typeOfDamage); break;
                        case "purple":     transform.parent.GetComponent<GhostPurple>().   ReceiveDamage(typeOfDamage); break;
                        case "red":        transform.parent.GetComponent<GhostRed>().      ReceiveDamage(typeOfDamage); break;
                        case "green":      transform.parent.GetComponent<GhostGreen>().    ReceiveDamage(typeOfDamage); break;
                        case "boss":       transform.parent.GetComponent<Boss>().          ReceiveDamage(typeOfDamage); break;
                        case "clon boss":  transform.parent.GetComponent<Clones>().        ReceiveDamage(typeOfDamage); break;
                        case "clon red":   transform.parent.GetComponent<ClonGhostRed>().  ReceiveDamage(typeOfDamage); break;
                        case "clon green": transform.parent.GetComponent<ClonGhostGreen>().ReceiveDamage(typeOfDamage); break;
                    } break;

                case "bala1":
                    Destroy(info.gameObject);
                    typeOfDamage = "bala1";
                    switch (transform.parent.tag) {
                        case "hunter":     transform.parent.GetComponent<Hunter>().        ReceiveDamage(typeOfDamage); break;
                        case "purple":     transform.parent.GetComponent<GhostPurple>().   ReceiveDamage(typeOfDamage); break;
                        case "red":        transform.parent.GetComponent<GhostRed>().      ReceiveDamage(typeOfDamage); break;
                        case "green":      transform.parent.GetComponent<GhostGreen>().    ReceiveDamage(typeOfDamage); break;
                        case "boss":       transform.parent.GetComponent<Boss>().          ReceiveDamage(typeOfDamage); break;
                        case "clon boss":  transform.parent.GetComponent<Clones>().        ReceiveDamage(typeOfDamage); break;
                        case "clon red":   transform.parent.GetComponent<ClonGhostRed>().  ReceiveDamage(typeOfDamage); break;
                        case "clon green": transform.parent.GetComponent<ClonGhostGreen>().ReceiveDamage(typeOfDamage); break;
                    } break;

                case "bala2":
                    typeOfDamage = "bala2";
                    switch (transform.parent.tag) {
                        case "hunter":     transform.parent.GetComponent<Hunter>().        ReceiveDamage(typeOfDamage); break;
                        case "purple":     transform.parent.GetComponent<GhostPurple>().   ReceiveDamage(typeOfDamage); break;
                        case "red":        transform.parent.GetComponent<GhostRed>().      ReceiveDamage(typeOfDamage); break;
                        case "green":      transform.parent.GetComponent<GhostGreen>().    ReceiveDamage(typeOfDamage); break;
                        case "boss":       transform.parent.GetComponent<Boss>().          ReceiveDamage(typeOfDamage); break;
                        case "clon boss":  transform.parent.GetComponent<Clones>().        ReceiveDamage(typeOfDamage); break;
                        case "clon red":   transform.parent.GetComponent<ClonGhostRed>().  ReceiveDamage(typeOfDamage); break;
                        case "clon green": transform.parent.GetComponent<ClonGhostGreen>().ReceiveDamage(typeOfDamage); break;
                    } break;
            }
        }
    }
}
