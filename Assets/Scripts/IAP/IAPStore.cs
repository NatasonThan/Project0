// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Purchasing;
// using TMPro;
// using System;

// public class IAPStore : MonoBehaviour
// {

//     // [Header("Non Consumable")]
//     //public GameObject adsPurchasedWindow;

//     // private static IStoreController storeController;
//     // private static IExtensionProvider storeExtensionProvider;

//     // public static string CHARACTER_01 = "unlock_character_01"; // Product ID


//     [Serializable]
//     public class Skudetails
//     {
//         public string productId;
//         public string type;
//         public string title;
//         public string name;
//         public string iconUrl;
//         public string description;
//         public string price;
//         public long price_amount_micros;
//         public string price_currency_code;
//         public string skuDetailsToken;
//     }

//     [Serializable]
//     public class PayloadData
//     {
//         public string orderId;
//         public string packageName;
//         public string productId;
//         public long purchaseTime;
//         public int purchaseState;
//         public string purchaseToken;
//         public int quantity;
//         public bool acknowledged;
//     }

//     [Serializable]
//     public class Payload
//     {
//         public string json;
//         public string signature;
//         public List<Skudetails> skuDetails;
//         public PayloadData payloadData;
//     }

//     [Serializable]
//     public class Data
//     {
//         public string Payload;
//         public string Store;
//         public string TransactionID;
//     }

//     public Data data;
//     public Payload payload;
//     public PayloadData payloadData;
//     // Start is called before the first frame update
//     void Start()
//     {
//         // if (storeController == null)
//         // {
//         //     InitializePurchasing();
//         // }
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     // public void OnPurchaseFailed(Product product,PurchaseFailureDescription failureDescription)
//     // {
//     //     Debug.Log(product.definition.id);
//     //     Debug.Log(failureDescription.reason);
//     // }

//     //Non-Consumable

//     // public void InitializePurchasing()
//     // {
//     //     if (IsInitialized()) return;

//     //     var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//     //     builder.AddProduct(CHARACTER_01, ProductType.NonConsumable);
//     //     UnityPurchasing.Initialize(this, builder);
//     // }

//     // private bool IsInitialized()
//     // {
//     //     return storeController != null && storeExtensionProvider != null;
//     // }

//     // public void BuyCharacter01()
//     // {
//     //     BuyProductID(CHARACTER_01);
//     // }

//     // void BuyProductID(string productId)
//     // {
//     //     if (IsInitialized())
//     //     {
//     //         Product product = storeController.products.WithID(productId);

//     //         if (product != null && product.availableToPurchase)
//     //         {
//     //             storeController.InitiatePurchase(product);
//     //         }
//     //         else
//     //         {
//     //             Debug.Log("Product not found or not available for purchase.");
//     //         }
//     //     }
//     //     else
//     //     {
//     //         Debug.Log("Not initialized.");
//     //     }
//     // }

//     // public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     // {
//     //     storeController = controller;
//     //     storeExtensionProvider = extensions;
//     // }

//     // public void OnInitializeFailed(InitializationFailureReason error)
//     // {
//     //     Debug.Log("IAP Initialize Failed: " + error);
//     // }

//     // public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//     // {
//     //     if (args.purchasedProduct.definition.id == CHARACTER_01)
//     //     {
//     //         Debug.Log("Character 01 unlocked!");
//     //         PlayerPrefs.SetInt("Character01Unlocked", 1); // Save unlock status
//     //     }
//     //     else
//     //     {
//     //         Debug.Log("Purchase failed.");
//     //     }

//     //     return PurchaseProcessingResult.Complete;
//     // }

//     // public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//     // {
//     //     Debug.Log($"Purchase Failed: {product.definition.id}, {failureReason}");
//     // }





    
// }
