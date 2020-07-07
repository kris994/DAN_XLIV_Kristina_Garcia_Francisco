using DAN_XVIV_Kristina_Garcia_Francisco.Commands;
using DAN_XVIV_Kristina_Garcia_Francisco.Model;
using DAN_XVIV_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace DAN_XVIV_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Main Window view model
    /// </summary>
    class MainWindowViewModel : BaseViewModel
    {
        MainWindow main;
        Worker worker;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with Main Window param
        /// </summary>
        /// <param name="mainOpen">opens the main window</param>
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            ItemList = service.GetAllItems().ToList();
            UserShoppingCartList = service.GetAllUserShoppingCarts(Service.LoggedInUser[0].UserID).ToList();
            ShoppingCartList = service.GetAllShoppingCarts();
            UserOrderList = service.GetAllUserOrders(Service.LoggedInUser[0].UserID).ToList();
            Thread showMenu = new Thread(ShowMenu);
            showMenu.Start();
        }

        /// <summary>
        /// Constructor with Worker Window param
        /// </summary>
        /// <param name="workerOpen">opens the worker window</param>
        public MainWindowViewModel(Worker workerOpen)
        {
            worker = workerOpen;
            OrderList = service.GetAllOrders().ToList();
            PendingOrderList = service.GetAllPendingOrders().ToList();
            NonPendingOrderList = service.GetAllNonPendingOrders().ToList();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of all Items
        /// </summary>
        private List<tblItem> itemList;
        public List<tblItem> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
                OnPropertyChanged("ItemList");
            }
        }

        /// <summary>
        /// Specific Item
        /// </summary>
        private tblItem item;
        public tblItem Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                OnPropertyChanged("Item");
            }
        }

        /// <summary>
        /// List of all shopping carts
        /// </summary>
        private List<tblShoppingCart> shoppingCartList;
        public List<tblShoppingCart> ShoppingCartList
        {
            get
            {
                return shoppingCartList;
            }
            set
            {
                shoppingCartList = value;
                OnPropertyChanged("ShoppingCartList");
            }
        }

        /// <summary>
        /// List of all user shopping carts
        /// </summary>
        private List<tblShoppingCart> userShoppingCartList;
        public List<tblShoppingCart> UserShoppingCartList
        {
            get
            {
                return userShoppingCartList;
            }
            set
            {
                userShoppingCartList = value;
                OnPropertyChanged("UserShoppingCartList");
            }
        }

        /// <summary>
        /// Specific Shopping Cart
        /// </summary>
        private tblShoppingCart shoppingCart;
        public tblShoppingCart ShoppingCart
        {
            get
            {
                return shoppingCart;
            }
            set
            {
                shoppingCart = value;
                OnPropertyChanged("ShoppingCart");
            }
        }

        /// <summary>
        /// List of all user orders
        /// </summary>
        private List<tblOrder> userOrderList;
        public List<tblOrder> UserOrderList
        {
            get
            {
                return userOrderList;
            }
            set
            {
                userOrderList = value;
                OnPropertyChanged("UserOrderList");
            }
        }

        /// <summary>
        /// List of all user orders
        /// </summary>
        private List<tblOrder> orderList;
        public List<tblOrder> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                orderList = value;
                OnPropertyChanged("OrderList");
            }
        }

        /// <summary>
        /// List of all user pending orders
        /// </summary>
        private List<tblOrder> pendingOrderList;
        public List<tblOrder> PendingOrderList
        {
            get
            {
                return pendingOrderList;
            }
            set
            {
                pendingOrderList = value;
                OnPropertyChanged("PendingOrderList");
            }
        }

        /// <summary>
        /// List of all user non pending orders
        /// </summary>
        private List<tblOrder> nonPendingOrderList;
        public List<tblOrder> NonPendingOrderList
        {
            get
            {
                return nonPendingOrderList;
            }
            set
            {
                nonPendingOrderList = value;
                OnPropertyChanged("NonPendingOrderList");
            }
        }

        /// <summary>
        /// Specific Order
        /// </summary>
        private tblOrder itemOrder;
        public tblOrder ItemOrder
        {
            get
            {
                return itemOrder;
            }
            set
            {
                itemOrder = value;
                OnPropertyChanged("ItemOrder");
            }
        }
        #endregion

        /// <summary>
        /// Order Visibility
        /// </summary>
        private Visibility orderVisibility;
        public Visibility OrderVisibility
        {
            get
            {
                return orderVisibility;
            }
            set
            {
                orderVisibility = value;
                OnPropertyChanged("OrderVisibility");
            }
        }

        /// <summary>
        /// Order Visibility
        /// </summary>
        private Visibility delayVisibility;
        public Visibility DelayVisibility
        {
            get
            {
                return delayVisibility;
            }
            set
            {
                delayVisibility = value;
                OnPropertyChanged("DelayVisibility");
            }
        }

        public void ShowMenu()
        {
            for (int i = 0; i < UserOrderList.Count; i++)
            {
                if (UserOrderList[i].OrderStatus == "Accepted" || UserOrderList[i].OrderStatus == "Denied")
                {
                    DelayVisibility = Visibility.Collapsed;
                    Thread.Sleep(2000);
                    DelayVisibility = Visibility.Visible;
                    break;
                }
                else
                {
                    DelayVisibility = Visibility.Visible;
                }
            }
        }

        #region Commands       
        /// <summary>
        /// Command that tries to add or edit item
        /// </summary>
        private ICommand addItem;
        public ICommand AddItem
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(param => AddItemExecute(), param => CanAddItemExecute());
                }
                return addItem;
            }
        }

        /// <summary>
        /// Executes the add command
        /// </summary>
        public void AddItemExecute()
        {
            try
            {
                int itemID = Item.ItemID;
                service.AddItem(Item, Service.LoggedInUser[0].UserID);
                UserShoppingCartList = service.GetAllUserShoppingCarts(Service.LoggedInUser[0].UserID).ToList();
                ShoppingCartList = service.GetAllShoppingCarts();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if the item can be added
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanAddItemExecute()
        {
            // If any order is on status waiting no more items can be added
            for (int i = 0; i < UserOrderList.Count; i++)
            {
                if (UserOrderList[i].OrderStatus == "Waiting")
                {
                    return false;
                }
            }

            try
            {
                if (Item.Amount == 0)
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }
         
            return true;
        }

        /// <summary>
        /// Command that tried to delete an item
        /// </summary>
        private ICommand removeItem;
        public ICommand RemoveItem
        {
            get
            {
                if (removeItem == null)
                {
                    removeItem = new RelayCommand(param => RemoveItemExecute(), param => CanRemoveItemExecute());
                }
                return removeItem;
            }
        }

        /// <summary>
        /// Executes the add command
        /// </summary>
        public void RemoveItemExecute()
        {
            try
            {
                if (Item != null)
                {
                    service.RemoveItem(Item, Service.LoggedInUser[0].UserID);
                    UserShoppingCartList.RemoveAll(i => i.UserID == Service.LoggedInUser[0].UserID && i.ItemID == Item.ItemID);
                    ShoppingCartList.RemoveAll(i => i.UserID == Service.LoggedInUser[0].UserID && i.ItemID == Item.ItemID);
                    UserShoppingCartList = service.GetAllUserShoppingCarts(Service.LoggedInUser[0].UserID).ToList();
                    ShoppingCartList = service.GetAllShoppingCarts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the item can be added
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanRemoveItemExecute()
        {
            // If any order is on status waiting no more items can be added
            for (int i = 0; i < UserOrderList.Count; i++)
            {
                if (UserOrderList[i].OrderStatus == "Waiting")
                {
                    return false;
                }
            }

            try
            {
                if (Item.Amount == 0)
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Command that tries to delete an order
        /// </summary>
        private ICommand deleteOrder;
        public ICommand DeleteOrder
        {
            get
            {
                if (deleteOrder == null)
                {
                    deleteOrder = new RelayCommand(param => DeleteOrderExecute(), param => CanDeleteOrderExecute());
                }
                return deleteOrder;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteOrderExecute()
        {
            try
            {
                if (ItemOrder != null)
                {
                    service.DeleteOrder(ItemOrder);
                    NonPendingOrderList.Remove(ItemOrder);
                    NonPendingOrderList = service.GetAllNonPendingOrders().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the item can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteOrderExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to order
        /// </summary>
        private ICommand order;
        public ICommand Order
        {
            get
            {
                if (order == null)
                {
                    order = new RelayCommand(param => OrderExecute(), param => CanOrderExecute());
                }
                return order;
            }
        }

        /// <summary>
        /// Executes the order command
        /// </summary>
        public void OrderExecute()
        {
            try
            {
                service.AddOrder(Service.LoggedInUser[0].UserID);
                UserOrderList = service.GetAllUserOrders(Service.LoggedInUser[0].UserID).ToList();

                UserShoppingCartList.Clear();
                ShoppingCartList.RemoveAll(i => i.UserID == Service.LoggedInUser[0].UserID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if the order can be added
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanOrderExecute()
        {
            if (UserOrderList.Count > 0)
            {
                OrderVisibility = Visibility.Visible;
            }
            else
            {
                OrderVisibility = Visibility.Collapsed;
            }

            for (int i = 0; i < UserOrderList.Count; i++)
            {
                if (UserOrderList[i].OrderStatus == "Waiting")
                {
                    return false;
                }
            }

            if (!UserShoppingCartList.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                Login login = new Login();
                if(Service.LoggedInUser.Count != 0)
                {
                    // Empty Shopping Cart when exiting
                    UserShoppingCartList.Clear();
                    ShoppingCartList.RemoveAll(i => i.UserID == Service.LoggedInUser[0].UserID);
                    service.EmptyShoppingCart(Service.LoggedInUser[0].UserID);

                    Service.LoggedInUser.RemoveAt(0);
                    main.Close();
                }
                else
                {
                    worker.Close();
                }
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that accepts the order
        /// </summary>
        private ICommand accept;
        public ICommand Accept
        {
            get
            {
                if (accept == null)
                {
                    accept = new RelayCommand(param => AcceptExecute(), param => CanAcceptExecute());
                }
                return accept;
            }
        }

        /// <summary>
        /// Executes the accept command
        /// </summary>
        private void AcceptExecute()
        {
            try
            {
                service.AcceptOrder(ItemOrder);
                OrderList = service.GetAllOrders().ToList();
                PendingOrderList = service.GetAllPendingOrders().ToList();
                NonPendingOrderList = service.GetAllNonPendingOrders().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to accept
        /// </summary>
        /// <returns>true</returns>
        private bool CanAcceptExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that accepts the order
        /// </summary>
        private ICommand deny;
        public ICommand Deny
        {
            get
            {
                if (deny == null)
                {
                    deny = new RelayCommand(param => DenyExecute(), param => CanDenyExecute());
                }
                return deny;
            }
        }

        /// <summary>
        /// Executes the accept command
        /// </summary>
        private void DenyExecute()
        {
            try
            {
                service.DenyOrder(ItemOrder);
                OrderList = service.GetAllOrders().ToList();
                PendingOrderList = service.GetAllPendingOrders().ToList();
                NonPendingOrderList = service.GetAllNonPendingOrders().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to accept
        /// </summary>
        /// <returns>true</returns>
        private bool CanDenyExecute()
        {
            return true;
        }
        #endregion
    }
}
