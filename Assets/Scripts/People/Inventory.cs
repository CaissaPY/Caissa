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

    public string ShowItems()
    {
        string itemsString = "";
        foreach (GameObject item in collectedItems)
        {
            itemsString += item.tag + ", ";
        }
        return itemsString;
    }

    public void AddItem(GameObject Item)
    {
        collectedItems.Add(Item); // Agrega el objeto a la lista
    }

    public void RemoveItem(GameObject Item)
    {
        collectedItems.Remove(Item); // Remueve el objeto de la lista
    }

    public int GetItemCount()
    {
        return collectedItems.Count; // Devuelve el número de items en el inventario
    }

    public int GetFilteredItemCount(string tag)
    {
        // Filtra los elementos por la etiqueta mandada por parámetro y devuelve el conteo del mismo
        int filteredCount = collectedItems.FindAll(item => item.CompareTag(tag)).Count;
        return filteredCount;
    }

}
