using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Representa uma Object Pool
/// </summary>
public class ObjectPool
{
    /// <summary>
    /// A Pool de objetos
    /// </summary>
    private List<GameObject> pool = new List<GameObject>();

    /// <summary>
    /// O Objeto usado para se criar novos
    /// </summary>
    private GameObject objectType;

    /// <summary>
    /// Define se a lista poderá crescer quando necessário
    /// </summary>
    private bool canGrow = true;

    /// <summary>
    /// Cria uma pool para este objeto
    /// </summary>
    /// <param name="pObjType">O tipo de objeto que ficará na pool</param>
    /// <param name="startSize">O tamanho inicial</param>
    /// <param name="pCanGrow">Define se esta pool poderá ou não crescer</param>
    public ObjectPool(GameObject pObjType, int startSize = 0, bool pCanGrow = true)
    {
        objectType = pObjType;
        canGrow = pCanGrow;

        for (int i = 0; i < startSize; i++)
        {
            GameObject gObj = Object.Instantiate(pObjType);
            gObj.SetActive(false);
            pool.Add(gObj);
        }
    }

    /// <summary>
    /// Pega um elemento disponível da pool.
    /// </summary>
    /// <returns>O Objeto</returns>
    public GameObject Get()
    {
        // Busca por um objeto desativado na pool
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        // Se não conseguiu um objeto, tenta criar um novo
        if (canGrow)
        {
            GameObject gObj = Object.Instantiate(objectType);
            gObj.SetActive(false);
            pool.Add(gObj);

            return gObj;
        }
        else
        {
            Debug.LogError("Limite da Pool excedido.");
            return null;
        }
    }
}