using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsSetup : MonoBehaviour {

    public List<Location> beltLocations = new List<Location>();
    public List<EnumDirection> beltDirections = new List<EnumDirection>();

    public List<Location> splitterLocations = new List<Location>();
    public List<EnumDirection> splitterDirections = new List<EnumDirection>();

    public List<Location> machineLocations = new List<Location>();
    public List<EnumDirection> machineDirections = new List<EnumDirection>();
    public List<string> machineLabels = new List<string>();

    public GameObject belt;
    public GameObject splitter;
    public GameObject machine;

    // Use this for initialization
    void Start () {

        for (int i = 0; i < beltLocations.GetCount(); ++i) {
            var b = Instantiate(belt, beltLocations[i].position+transform.position, 
                Quaternion.Euler(0f,
                180f + beltDirections[i].GetAngle(), 
                0f));
            Grid.PlaceItem(b.GetComponent<AbstractPlacedItem>());
        }
        for (int i = 0; i < splitterLocations.GetCount(); ++i)
        {
            var b = Instantiate(splitter, splitterLocations[i].position + transform.position,
                Quaternion.Euler(0f,
                90f + splitterDirections[i].GetAngle(),
                0f));
            Grid.PlaceItem(b.GetComponent<AbstractPlacedItem>());
        }

        for (int i = 0; i < machineLocations.GetCount(); ++i)
        {
            var b = Instantiate(machine, machineLocations[i].position + transform.position +
                new Vector3(0f, machine.transform.position.y, 0),
                Quaternion.Euler(0f,
                0f + machineDirections[i].GetAngle(),
                0f));
            if (i < machineLabels.GetCount())
            {
                var category = machineLabels[i];
                var text = b.GetComponentInChildren<Text>();
                if (text) text.text = category;

                // HACK: hard-code credit lists
                List<string> names = new List<string>();
                switch (category)
                {
                    case "2D Art":
                        names.Add("Joelindi");
                        names.Add("RamonRobben");
                        names.Add("Jorisvanhaeren");
                        break;
                    case "3D Models":
                        names.Add("TheCoCe");
                        names.Add("Joelindi");
                        names.Add("BussianRanana");
                        names.Add("RamonRobben");
                        break;
                    case "Audio":
                        names.Add("ShannahQuilts");
                        names.Add("thesbros99");
                        names.Add("Kcaltrain");
                        break;
                    case "Code":
                        names.Add("arkandosDE");
                        names.Add("Hufo");
                        names.Add("Amerine");
                        break;
                    case "Gameplay":
                        names.Add("RamonRobben");
                        break;
                }
                names.Add("HardlyDifficult"); // Nick did a bit of everything !
                var m = b.GetComponent<CreditsMachine>();
                m.credits = names;
                m.maxTotalStorage = names.GetCount() * 2;
            }
            Grid.PlaceItem(b.GetComponent<AbstractPlacedItem>());
        }

        /*
        // this code is for extracting infos from children saved from a play
        foreach (var b in this.gameObject.GetComponentsInChildren<Belt>())
        {
            beltLocations.Add(b.location);
            beltDirections.Add(b.forward);
        }
        foreach (var b in this.gameObject.GetComponentsInChildren<Splitter>()) {
            splitterLocations.Add(b.location);
            splitterDirections.Add(b.forward);
        }*/

    }

    // Update is called once per frame
    void Update () {
		
	}
}
