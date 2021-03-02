/* Script to populate dropdowns in the input items prefab based on the values provided in GlobalVariables.cs
 * Author : BaldBeardedMonk
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateInputItems : MonoBehaviour
{
    public Transform Content;
    public GameObject InputItemsPrefab,ResultPanel;
    void Start()
    {
        GlobalVariables.SetInitialParameters();
    }

    public void OnAddItemClicked()
    {
        GameObject G = Instantiate(InputItemsPrefab) as GameObject;
        G.transform.SetParent(Content, false);

        // populate the qty dropdown 
        TMP_Dropdown qtyDropDown = G.transform.Find("QtyDropDown").GetComponent<TMP_Dropdown>();
        //qtyDropDown.ClearOptions();
        for (int i = 1; i <= GlobalVariables.qtyAllowed; i++) AddDataToDropDown(qtyDropDown, i.ToString());

        //populate the items dropdown
        TMP_Dropdown itemsDropDown = G.transform.Find("ItemNameDropDown").GetComponent<TMP_Dropdown>();
        //itemsDropDown.ClearOptions();
        foreach (KeyValuePair<string, string> keyValue in GlobalVariables.items) AddDataToDropDown(itemsDropDown, keyValue.Key);
    }

    void AddDataToDropDown(TMP_Dropdown dropdown, string data)
    {
        dropdown.options.Add(new TMP_Dropdown.OptionData() { text = data });
    }

    public void OnSubmitClicked()
    {
        ResultPanel.SetActive(true);
    }
}
