using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // One tip for the visual is to not use the exact same size for the object
    // There is a bug with the mesh so make it a little bit bigger
    // The change doesn't come in the visual (material) itself, but in it's container 


    // This is the counter itself, who's referencing himbself
    [SerializeField] private BaseCounter selectedCounter;
    // This is the visual representation of the counter (material)
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        // We subscribe to the event of the player
        Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
    }

    // This is the method that is going to be called when the selected counter changes
    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == selectedCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
