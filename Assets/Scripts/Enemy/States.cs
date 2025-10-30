using UnityEngine;

public abstract class States : MonoBehaviour
{
    protected FSMController _controller;

    public virtual void InitController(FSMController controller) //Inicializa el controlador para cualquier estado.
    {
        this._controller = controller;
    }
    public abstract void OnEnter(); //Método a ejecutar al entrar en un estado.
    public abstract void OnUpdate(); //Método a realizar cuando se mantiene el estado.
    public abstract void OnExit(); //Método a ejecutar al salir del estado.
}
