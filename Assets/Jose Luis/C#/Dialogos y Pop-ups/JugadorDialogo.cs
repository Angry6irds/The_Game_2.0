using UnityEngine;
using System.Collections.Generic;

public class JugadorDialogo : MonoBehaviour
{
    public DialogoPopup dialogoUI;
    private ZonaDialogo zonaActual;

    public void ActivarDialogos(List<DialogoConAutor> mensajes, GameObject colisionador = null, ZonaDialogo zonaOrigen = null)
    {
        zonaActual = zonaOrigen;

        if (dialogoUI != null)
        {
            dialogoUI.MostrarDialogos(mensajes, colisionador, zonaActual);
        }
    }
}