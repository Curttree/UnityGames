using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.GPUSort;

//https://docs.unity.com/en-us/iap/upgrade-to-iap-v5
public class Purchaser : MonoBehaviour
{
    private static StoreController m_StoreController;          // The Unity Purchasing system.

    [SerializeField]
    private GameController gameController;
    public static string kProductIDVIP = "vip";

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    async void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        m_StoreController = UnityIAPServices.StoreController();
        m_StoreController.OnPurchasePending += OnPurchasePending;

        await m_StoreController.Connect();

        m_StoreController.OnProductsFetched += OnProductsFetched;
        m_StoreController.OnPurchasesFetched += OnPurchasesFetched;

        var initialProductsToFetch = new List<ProductDefinition>
        {
            new(kProductIDVIP, ProductType.NonConsumable)
        };

        m_StoreController.FetchProducts(initialProductsToFetch);
    }
    void OnProductsFetched(List<Product> products)
    {
        // Handle fetched products  
        m_StoreController.FetchPurchases();
    }
    void OnPurchasesFetched(Orders orders)
    { 
        if (orders.ConfirmedOrders.Count > 0)
        {
            //TODO: If more than one purchasable item is added, expand this.
            var product = orders.ConfirmedOrders.First<ConfirmedOrder>().CartOrdered.Items().First()?.Product;
            if (String.Equals(product.definition.id, kProductIDVIP, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));
                gameController.SetPaidUser();
            }
        }
    }

    public void BecomePaidUser()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(kProductIDVIP);
    }

    void OnPurchasePending(PendingOrder order)
    {
        // A non-consumable product has been purchased by this user.
        var product = order.CartOrdered.Items().First()?.Product;

        // Potentially delay until purchase is confirmed.
        if (String.Equals(product.definition.id, kProductIDVIP, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.definition.id));
            gameController.SetPaidUser();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", product.definition.id));
        }
        // Nothing to do here yet.
    }

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null;
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // Only one valid product..

            if(productId != kProductIDVIP)
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                return;
            }

            m_StoreController.PurchaseProduct(productId);
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        //if (Application.platform == RuntimePlatform.IPhonePlayer ||
        //    Application.platform == RuntimePlatform.OSXPlayer)
        //{
        //    // ... begin restoring purchases
        //    Debug.Log("RestorePurchases started ...");

        //    // Fetch the Apple store-specific subsystem.
        //    var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
        //    // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
        //    // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
        //    apple.RestoreTransactions((result) =>
        //    {
        //        // The first phase of restoration. If no more responses are received on ProcessPurchase then 
        //        // no purchases are available to be restored.
        //        Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
        //    });
        //}
        //// Otherwise ...
        //else
        //{
        //    // We are not running on an Apple device. No work is necessary to restore purchases.
        //    Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        //}
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

}
