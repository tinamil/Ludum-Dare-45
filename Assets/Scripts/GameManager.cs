using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public SpriteRenderer selectionBoxIcon;
    public Building[] buildings;
    public Image[] selectionIcons;

    private SpriteRenderer mouseDragBox;
    private Vector2 mouseDown;
    private Vector2 mouseUp;
    private List<Peasant> selectedPeasants;

    private Building ghostBuilding;

    void Start()
    {
        mouseDragBox = Instantiate(selectionBoxIcon);
        mouseDragBox.gameObject.SetActive(false);
        selectedPeasants = new List<Peasant>();
    }

    public void StartBuilding(int index)
    {
        if (ghostBuilding == null)
        {
            ghostBuilding = Instantiate(buildings[index]);
            ghostBuilding.SetGhost(true);
        }
    }

    void Update()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Placing Ghost Building
            if (ghostBuilding != null)
            {
                ghostBuilding.SetGhost(false);
                ghostBuilding = null;
            }
            //Selection of peasants/building
            else
            {

                foreach (var peasant in selectedPeasants)
                {
                    peasant.SetSelected(false);
                }
                foreach (var icon in selectionIcons)
                {
                    icon.color = Color.clear;
                }
                selectedPeasants.Clear();

                mouseDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseDragBox.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDragBox.gameObject.SetActive(false);
            mouseUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var colliders = Physics2D.OverlapAreaAll(mouseDown, mouseUp);
            foreach (var c in colliders)
            {
                var peasant = c.GetComponentInParent<Peasant>();
                if (peasant != null)
                {
                    peasant.SetSelected(true);
                    selectionIcons[selectedPeasants.Count].sprite = peasant.icon;
                    selectionIcons[selectedPeasants.Count].color = Color.white;
                    selectedPeasants.Add(peasant);
                    if (selectedPeasants.Count >= selectionIcons.Length)
                    {
                        break;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDragBox.transform.position = (currentPosition + mouseDown) / 2.0f;
            mouseDragBox.size = currentPosition - mouseDown;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (var peasant in selectedPeasants)
            {
                peasant.InteractAt(targetPosition);
            }
        }
    }

}
