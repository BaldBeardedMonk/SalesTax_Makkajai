/* Script to hold global parameters that will be static throughout the product. For Example - product names, types, sales tax etc
 * The class is static and will hold all static variables which can be easily accessed throughout the project. 
 * Author : BaldBeardedMonk
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables 
{
    public static int qtyAllowed;                                                           // variable to hold the maximum number of quantity allowed in quantity dropdown                                                      
    public static Dictionary<string,int> itemTypes = new Dictionary<string, int>();         // list to hold exempted item type and exemption code (0 = not exempted, 1 = exempted)                      
    public static Dictionary<string, string> items = new Dictionary<string, string>();      // dictionary to hold item name and it's type
    public static float salesTax,importDuty;                                                // variables to hold sales tax and import duty %

    public static void SetInitialParameters()
    {
        // function to manually set initial parameters

        qtyAllowed = 10;
        salesTax = 0.1f;
        importDuty = 0.05f;
        
        /* Add item category along with exemption code (0 = not exempted, 1= exempted)*/
        itemTypes.Add("books",1);
        itemTypes.Add("food",1);
        itemTypes.Add("medical",1);
        itemTypes.Add("music",0);
        itemTypes.Add("hygiene",0);
        itemTypes.Add("others",0);

        /* Add individual item name along with it's item type. (Another implementation is to allow selection of item type from the app dropdown. This can easily be changed and implemented at a later stage if needed
         * Or we can even store this data in a .csv file and get it from there at once)*/
        items.Add("Game Dev Book", "books");
        items.Add("Elementary mathematics", "books");
        items.Add("Music CD - Beatles", "music");
        items.Add("Santana Mp3 collection", "music");
        items.Add("Chocolate Bar", "food");
        items.Add("Box of chocolate", "food");
        items.Add("Bottle of perfume", "hygiene");
        items.Add("Beard Oil", "hygiene");
        items.Add("Headache pills", "medical");
        items.Add("Paracetamol", "medical");
    }
}
