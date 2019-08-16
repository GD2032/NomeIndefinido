using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class UI : InterfaceEvent_1
{
    [SerializeField] public Image[] FADES;
    /// <summary>
    /// Inicia o fade tipo 1, podendo ser IN ou OUT
    /// </summary>
    /// <param name="in_OR_out">true para FADEIN,false para FADEOUT</param>
    /// <param name="seconds">velocidade do término em SEGUNDOS</param>
    static protected void Fade_1(bool in_OR_out,float seconds){
        Instantiate(FADES[0]);
    }    
}
