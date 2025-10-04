using System.Collections.Generic;
using UnityEngine;

public class DictionaryExample : MonoBehaviour
{
    Dictionary<string, float> _products;

    private void Start()
    {
        _products = new Dictionary<string, float>();
        _products.Add("Coffee", 120.25f);
        _products.Add("Matcha", 90.0f);
        _products.Add("Latte", 110f);

        Debug.Log(GetPrice("Doughnut"));
        Debug.Log(GetPrice("Coffee"));
    }

    private float GetPrice(string productId)
    {
        if (_products.ContainsKey(productId))
        {
            return _products[productId];
        }
        return 0;
    }
}
