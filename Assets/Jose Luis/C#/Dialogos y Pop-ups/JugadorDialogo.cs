using UnityEngine;
using System.Collections.Generic;


public class JugadorDialogo : MonoBehaviour
{
    public DialogoPopup dialogoUI;

    public void ActivarDialogos(List<DialogoConAutor> mensajes)
    {
        if (dialogoUI != null)
        {
            dialogoUI.MostrarDialogos(mensajes);
        }
    }
}