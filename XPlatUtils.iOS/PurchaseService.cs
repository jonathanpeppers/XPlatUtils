//  PurchaseService.cs
//
//  Author:
//  Jonathan Peppers
//
//  Copyright 2012 Jonathan Peppers
//
//  Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
//  with the License. You may obtain a copy of the License at
//  
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
//  on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for 
//  the specific language governing permissions and limitations under the License.

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

//Also supports Xamarin.Mac
#if MAC
using MonoMac.StoreKit;
using MonoMac.Foundation;
using MonoMac.AppKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.StoreKit;
#endif

namespace XPlatUtils
{
	/// <summary>
	/// A simplified class for making in-app purchases on Apple platforms, everything is wrapped in System.Threading.Tasks like a real playa
	/// </summary>
	public class PurchaseService
	{
		private const string DefaultError = "Could not load iTunes store."; //Copying Apple's generic error when something goes wrong
		private ObserverDelegate _observer;
		private SKProductsRequest _request = null;
		private RequestDelegate _requestDelegate = null;
        private TaskCompletionSource<List<SKProduct>> _getPurchasesSource;
        private TaskCompletionSource<SKPaymentTransaction> _purchaseSource;
        private TaskCompletionSource<string[]> _restoreSource;

		public PurchaseService ()
		{
			#if DEBUG
			Debug = true;
			#endif
		}

		/// <summary>
		/// A simple flag to check if IAPs are supported
		/// </summary>
		public bool Supported 
		{
			get 
            { 
#if MAC
                var version = NSProcessInfo.ProcessInfo.OperatingSystemVersionString;
                if (!version.Contains("10.7") && !version.Contains("10.8"))
                {
                    return false;
                }
                return SKPaymentQueue.CanMakePayments;
#else
                return SKPaymentQueue.CanMakePayments; 
#endif
            }
		}

		/// <summary>
		/// Set this to true to get Console.WriteLine output, it's on by default in debug, off in release
		/// </summary>
		public bool Debug
		{
			get;
			set;
		}

		/// <summary>
		/// Makes a purchase with an SKProduct instance
		/// </summary>
		public Task<SKPaymentTransaction> Purchase (SKProduct product)
		{
			if (_observer == null) 
			{
				_observer = new ObserverDelegate(this);
				SKPaymentQueue.DefaultQueue.AddTransactionObserver(_observer);
			}

            _purchaseSource = new TaskCompletionSource<SKPaymentTransaction>();
			SKPaymentQueue.DefaultQueue.AddPayment(SKPayment.PaymentWithProduct(product));
            return _purchaseSource.Task;
		}

		/// <summary>
		/// Gets a list of purchases with specified purchase IDs from iTunes connect
		/// </summary>
		public Task<List<SKProduct>> GetPurchases (params string[] purchaseIDs)
		{
            _getPurchasesSource = new TaskCompletionSource<List<SKProduct>>();
            _request = new SKProductsRequest(new NSSet(purchaseIDs)); 
			if (_requestDelegate == null)
			{
				_requestDelegate = new RequestDelegate(this);
			}
			_request.Delegate = _requestDelegate;
			_request.Start ();

			return _getPurchasesSource.Task;
		}

		/// <summary>
		/// Restores non-consumable purchases a user has previously purchased, but then deleted the app
		/// </summary>
		public Task<string[]> RestorePurchases ()
		{
			if (_observer == null) 
			{
				_observer = new ObserverDelegate(this);
				SKPaymentQueue.DefaultQueue.AddTransactionObserver(_observer);
			}
			
            _restoreSource = new TaskCompletionSource<string[]>();
			SKPaymentQueue.DefaultQueue.RestoreCompletedTransactions ();
            return _restoreSource.Task;
		}

        private void CompletePurchaseSuccessfully(SKPaymentTransaction transaction)
        {
            if (_purchaseSource != null)
            {
                _purchaseSource.SetResult(transaction);
                _purchaseSource = null;
            }
        }

        private void CompletePurchaseWithError(Exception exc)
        {
            if (_purchaseSource != null)
            {
                _purchaseSource.SetException(exc);
                _purchaseSource = null;
            }
        }

        private void CompleteRestoreSuccessfully(string[] purchaseIds)
        {
            if (_restoreSource != null)
            {
                _restoreSource.SetResult(purchaseIds);
                _restoreSource = null;
            }
        }

        private void CompleteRestoreWithError(Exception exc)
        {
            if (_restoreSource != null)
            {
                _restoreSource.SetException(exc);
                _restoreSource = null;
            }
        }

		private void CompletePurchasesRequestSuccessfully (List<SKProduct> product)
		{
			if (_getPurchasesSource != null) 
			{
				_getPurchasesSource.SetResult (product);
				_getPurchasesSource = null;
			}
			if (_request != null)
			{
				_request.Delegate = null;
				_request.Dispose ();	
				_request = null;
			}
		}

        private void CompletePurchasesRequestWithError (Exception exc)
        {
            if (_getPurchasesSource != null) 
            {
                _getPurchasesSource.SetException (exc);
                _getPurchasesSource = null;
            }
            if (_request != null)
            {
                _request.Delegate = null;
                _request.Dispose ();    
                _request = null;
            }
        }

		private void Log(string text)
		{
			if (Debug)
				Console.WriteLine (text);
		}

		/// <summary>
		/// Delegate for retrieving purchase info
		/// </summary>
		private class RequestDelegate : SKProductsRequestDelegate
		{
			private readonly PurchaseService _purchaseService;
			
			public RequestDelegate (PurchaseService purchaseService)
			{
				_purchaseService = purchaseService;
			}
			
			public override void ReceivedResponse (SKProductsRequest request, SKProductsResponse response)
			{
				if (response.Products == null || response.Products.Length == 0)
				{
					_purchaseService.Log (DefaultError);
                    _purchaseService.CompletePurchasesRequestWithError (new Exception());
				}
				else
				{
					_purchaseService.CompletePurchasesRequestSuccessfully (response.Products.ToList());
				}
			}
			
			public override void RequestFailed (SKRequest request, NSError error)
			{
				//This crap is null randomly in production, I wrote a strongly worded letter to Tim Cook
                if (error == null)
                {
					_purchaseService.Log ("SKProductsRequest failed: error is null");
					_purchaseService.CompletePurchasesRequestWithError(new Exception(PurchaseService.DefaultError));
                }
                else
                {
					_purchaseService.Log ("SKProductsRequest failed: " + error.LocalizedDescription);
                    _purchaseService.CompletePurchasesRequestWithError(new Exception(error.LocalizedDescription));
                }
			}
		}

		/// <summary>
		/// Observer for the callbacks on actual transactions
		/// </summary>
		private class ObserverDelegate : SKPaymentTransactionObserver
		{
			private readonly PurchaseService _purchaseService;
            private List<string> _purchases;

			public ObserverDelegate (PurchaseService purchaseService)
			{
				_purchaseService = purchaseService;
			}

			public override void UpdatedTransactions (SKPaymentQueue queue, SKPaymentTransaction[] transactions)
			{
				foreach (SKPaymentTransaction transaction in transactions)
				{
					switch (transaction.TransactionState)
					{
						case SKPaymentTransactionState.Failed:
							_purchaseService.Log ("SKPayment failed: " +  transaction.Error.LocalizedDescription);
							SKPaymentQueue.DefaultQueue.FinishTransaction (transaction);
                            _purchaseService.CompletePurchaseWithError(new Exception(transaction.Error.LocalizedDescription));
							break;
						case SKPaymentTransactionState.Purchased:
							_purchaseService.Log ("Successfully purchased: " + transaction.Payment.ProductIdentifier);
                            _purchaseService.CompletePurchaseSuccessfully(transaction);
							SKPaymentQueue.DefaultQueue.FinishTransaction (transaction);
							break;
						case SKPaymentTransactionState.Restored:
							_purchaseService.Log ("Successfully restored: " + transaction.Payment.ProductIdentifier);
							if (_purchases == null)
                                _purchases = new List<string>();
                            _purchases.Add(transaction.Payment.ProductIdentifier);
							SKPaymentQueue.DefaultQueue.FinishTransaction (transaction);
							break;
						default:
							break;
					}
				}
			}

			public override void PaymentQueueRestoreCompletedTransactionsFinished (SKPaymentQueue queue)
			{
                _purchaseService.CompleteRestoreSuccessfully(_purchases == null ? null : _purchases.ToArray());
                _purchases = null;
			}

			public override void RestoreCompletedTransactionsFailedWithError (SKPaymentQueue queue, NSError error)
			{
				//This crap is null randomly in production, I wrote a strongly worded letter to Tim Cook
				if (error == null)
				{
					_purchaseService.Log ("RestoreCompletedTransactionsFailedWithError failed: error is null");
					_purchaseService.CompleteRestoreWithError(new Exception(PurchaseService.DefaultError));
				}
				else
				{
					_purchaseService.Log ("SKPayment failed: " +  error.LocalizedDescription);
                	_purchaseService.CompleteRestoreWithError(new Exception(error.LocalizedDescription));
				}
			}
		}
	}
}

