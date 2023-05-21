using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<GameObject> collectedItems; // Lista de objetos de piedra recolectados

    public Inventory()
    {
        collectedItems = new List<GameObject>();
    }

    public void AddStone(GameObject stone)
    {
        collectedItems.Add(stone); // Agrega el objeto de piedra a la lista
    }

    public void RemoveStone(GameObject stone)
    {
        collectedItems.Remove(stone); // Remueve el objeto de piedra de la lista
    }

    public int GetStoneCount()
    {
        return collectedItems.Count; // Devuelve el número de piedras en el inventario
    }
}
