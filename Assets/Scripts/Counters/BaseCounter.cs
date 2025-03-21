using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    // All of this is for testing moving a kitchen object from one counter to another
    // [SerializeField] private ClearCounter secondClearCounter;
    // [SerializeField] private bool testing;
    // private void Update()
    // {
    //     if (testing && Input.GetKeyDown(KeyCode.T))
    //     {
    //         if (kitchenObject != null)
    //         {
    //             kitchenObject.SetKitchenObject(secondClearCounter);
    //         }
    //     }
    // }

    // Its a virtual method so it can be overriden by the child classes
    public virtual void Interact(Player player)
    {
        Debug.LogWarning("Interact method not implemented");
    }
    public Transform GetHoldingPointObject()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject newKitchenObject)
    {
        kitchenObject = newKitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
