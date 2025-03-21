using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen object in the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player i'snt carrying anything
            }
        }
        else
        {
            // There is a kitchen object in the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying something so nothing happens
            }
            else
            {
                // Player i'snt carrying anything
                // Player grabs the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}