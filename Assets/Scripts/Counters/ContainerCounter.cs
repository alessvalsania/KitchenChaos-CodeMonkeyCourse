using System;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player is not carrying anything

            // Spawn a the kitchen object attached to the counter
            // Instantiate spawns a prefab under the transform parent
            // Basically under CounterTopPoint spawns the prefab
            // Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            // Because now the positioning is done in the SetKitchenObject method we can just pass the prefab
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            // Set the kitchen object to the counter
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
