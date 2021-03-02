/* Script to Calculate Sales Tax and display the final Invoice. The script is attached to a gameobject which is activated when submit is clicked. The Start function is called once the gameobject is activated and the calculations are done
 * We instantiate one TextMeshProUGUI gameobject for evey line of output. 
 * Author : BaldBeardedMonk
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CalculateSalesTax : MonoBehaviour
{
    GameObject[] InputItems;
    float totalTax,totalAmount;
    public Transform Content;
    public GameObject OutputText;
    void Start()
    {
        InputItems = GameObject.FindGameObjectsWithTag("InputItems");
        foreach(GameObject G in InputItems)
        {
            int qty;
            float rate,tax;
            TMP_Dropdown qtyDropDown = G.transform.Find("QtyDropDown").GetComponent<TMP_Dropdown>();
            qty = int.Parse(qtyDropDown.options[qtyDropDown.value].text);

            TMP_Dropdown itemDropDown = G.transform.Find("ItemNameDropDown").GetComponent<TMP_Dropdown>();
            string item = itemDropDown.options[itemDropDown.value].text;

            TMP_Dropdown importDropDown = G.transform.Find("ImportedDropDown").GetComponent<TMP_Dropdown>();
            int isImported = importDropDown.value-1;      // if imported then 0 else 1 . This is set by the order of the options while creating the dropdown in the unity editor

            TMP_InputField rateField = G.transform.Find("RateInputField").GetComponent<TMP_InputField>();
            rate = float.Parse(rateField.text);

            tax = CheckSalesTax(item, isImported, rate);
            rate = rate + tax;
            totalTax += tax;
            totalAmount += rate;
            GameObject Ot = Instantiate(OutputText) as GameObject;  // initialising the text prefab to show the output on the screen
            Ot.transform.SetParent(Content, false);
            Ot.GetComponent<TextMeshProUGUI>().text = qty +" " + item + " : " + rate;
            //Debug.Log(qty + item + " : " + rate);
        }
        GameObject Ot1 = Instantiate(OutputText) as GameObject;
        Ot1.transform.SetParent(Content, false);
        Ot1.GetComponent<TextMeshProUGUI>().text = "Sales Taxes : " + totalTax;

        GameObject Ot2 = Instantiate(OutputText) as GameObject;
        Ot2.transform.SetParent(Content, false);
        Ot2.GetComponent<TextMeshProUGUI>().text = "Total :" + totalAmount;
    }

    float CheckSalesTax(string item, int isImported,float rate)
    {
        float tax=0;
        if(isImported==0)
        {
            // if item is imported
            if(CheckExempted(item)) tax = GlobalVariables.importDuty * rate;    // if item is exempted we only levy the import duty
            else
            {
                tax = (GlobalVariables.salesTax + GlobalVariables.importDuty) * rate;
            }
            
        }
        else if(isImported==1)
        {
            // if item is not imported
            if (CheckExempted(item)) tax = 0;   // if item is exempted there is no tax
            else tax = (GlobalVariables.salesTax) * rate;
        }
        tax = Mathf.Ceil(tax * 20) / 20;   //rounding up to nearest 0.05 . I have fucking no idea how this works, got it from stackoverflow :)
        return tax;
    }

    bool CheckExempted(string item)
    {
        string itemType = "";
        int isExempted = 0;
        foreach (KeyValuePair<string, string> keyValue in GlobalVariables.items)
        {
            if (keyValue.Key.Equals(item))
            {
                // itemType found. assign it and break
                itemType = keyValue.Value;
                break;
            }
        }
        foreach (KeyValuePair<string, int> keyValue1 in GlobalVariables.itemTypes)
        {
            if (keyValue1.Key.Equals(itemType))
            {
                // isExempted found. assign it and break
                isExempted = keyValue1.Value;
                break;
            }
        }
        if (isExempted == 1) return true;
        else return false;
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
}
