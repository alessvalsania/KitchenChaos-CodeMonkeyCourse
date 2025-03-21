using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    // Basically I'm saying that it can be a ClearCounter or a Player
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
        // Before it wal all counters, now it can be player and counter
        // Everetithing below that says counter can be player and counter

        // Check if the kitchen object is already on a counter
        // If it is, clear the counter (This is the old counter)
        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }
        // Set the new counter
        kitchenObjectParent = newKitchenObjectParent;

        // If the new counter already has a kitchen object, log an error
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Trying to set a kitchen object to a counter that already has one");
        }

        // then we assigned to the new counter this kitchen object
        kitchenObjectParent.SetKitchenObject(this);

        // Set the position of the kitchen object to the new counter
        // This is the same as the Instantiate in ClearCounter.cs
        transform.parent = newKitchenObjectParent.GetHoldingPointObject();
        transform.localPosition = Vector3.zero;


    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

}
