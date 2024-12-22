



using Godot;

/// <summary>
/// A node that allows an entity to listen for interaction events from the Manager
/// </summary>
[GlobalClass]
public partial class Interactable: Area3D
{

    public override void _EnterTree()
    {
        base._EnterTree();

        SetCollisionLayerValue(1, false);
        SetCollisionMaskValue(1, false);

        SetCollisionLayerValue((int) UnitManager.Layers.Interactable, true);
        SetCollisionMaskValue((int) UnitManager.Layers.Interactable, true);

    }

    public void Interact()
    {

    }

    public bool IsInteracting { get; set; }

    public void CancelInteraction()
    {
        
    }

    /*

    Create an interaction class? To hold some kind of runnable or callback?

    public void AddInteraction(Interaction? interaction);

    public void ClearInteractionQueue();

    public void GetInteractionQueueCount???();

    */

}