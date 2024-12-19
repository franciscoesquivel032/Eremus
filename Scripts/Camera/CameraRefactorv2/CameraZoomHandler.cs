using Godot;
using System;
using System.Diagnostics;

public partial class CameraZoomHandler : Node3D
{
    private Node3D _parent; // Parent node reference

    // TODO, [Resource refactor]
	private float _cameraZoomSpeed; // zoom speed
	private float _cameraZoomMin; // min zoom 
	private float _cameraZoomMax; // max zoom
	private float _cameraZoomDampingSpeed; // zoom smooth stop
    private float _cameraZoomStep;


    /// <summary>
    ///  Settings initialization 
    ///  Future refactor incoming { settings = _resourceSettings }
    /// </summary>
    public override void _Ready()
    {
        _parent = GetNode<Camera3D>("../Camera3D");
        _cameraZoomSpeed = 5f;
        _cameraZoomMin = 1f;
        _cameraZoomMax = 20f;
        _cameraZoomDampingSpeed = 5f;
        _cameraZoomStep = .4f;
    }



    /// <summary>
    /// Calculates Position.Z value based on _cameraZoomSpeed and _cameraZoomDirection over time
    /// Constraints new Z to _cameraZoomMin and _cameraZoomMax values
    /// Adjusts camera Position 
    /// Softens camera zoom movement end
    /// </summary>
    /// <param name="delta"></param>
    public void Process(float direction){

		 // Calculamos la nueva posición Y basándonos en el paso de zoom
        float newY = _parent.Position.Y + _cameraZoomStep * direction;

        // Limitamos el valor de Y al rango permitido
        newY = Mathf.Clamp(newY, _cameraZoomMin, _cameraZoomMax);

        // Actualizamos la posición del nodo
        _parent.Position = new Vector3(_parent.Position.X, newY, _parent.Position.Z);
	}
}