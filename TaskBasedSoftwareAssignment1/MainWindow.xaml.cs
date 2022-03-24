using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskBasedSoftwareAssignment1 {

	public enum Threading
    {
		SINGLE = 0,
    }

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public Dictionary<string, Store> stores; // key = storeCode.
		public List<Order> orders;
		public HashSet<Date> dates;
		public HashSet<Date> displayedDates;


		Threading threadState;

		private string[] selectedStores;
		private string selectedStore;

		private int[] selectedDates;
		private int[] selectedOrders;

		public MainWindow() {
			stores = new Dictionary<string, Store>();
			orders = new List<Order>();
			dates = new HashSet<Date>();
			displayedDates = new HashSet<Date>();

/*		System.Windows.Data Error: 40 :
			BindingExpression path error:
				'selectedStores' property not found on 'object' ''MainWindow' (Name='window')'.
				BindingExpression:Path = selectedStores;
				DataItem = 'MainWindow'(Name = 'window');
				target element is 'ListBox'(Name = 'storeList');
			target property is 'SelectedItem'(type 'Object')

		System.Windows.Data Error: 40 :
			BindingExpression path error:
				'weekIndex' property not found on 'object' ''MainWindow' (Name='window')'.
				BindingExpression:Path = weekIndex;
				DataItem = 'MainWindow'(Name = 'window');
				target element is 'ListBox'(Name = 'weekList');
				target property is 'SelectedItem'(type 'Object')
*/

			selectedStores = new string[300];
			selectedDates = new int[500000]; // temp numbers for now until CSV file loading is added.
			selectedOrders = new int[1000000];
			InitializeComponent();

			DataContext = this;

			Debug.WriteLine("test");
			ExampleLoadData.StoreLoader.LoadData(ref stores, ref dates, ref orders);
			displayedDates = dates;
			storeList.ItemsSource = stores; // Must be empty before populating, I must do what WPF demands!
			weekList.ItemsSource = displayedDates;
		}

		private void StoreSelectAll_Click(object sender, RoutedEventArgs e) {
			storeList.SelectAll();
		}

		private void StoreDeselectAll_Click(object sender, RoutedEventArgs e) {
			storeList.UnselectAll();
		}
		private void WeekSelectAll_Click(object sender, RoutedEventArgs e)
		{
			weekList.SelectAll();
		}
		private void WeekDeselectAll_Click(object sender, RoutedEventArgs e)
		{
			weekList.UnselectAll();
		}
		private void UpdateWeeks()
        {
			switch (threadState)
			{
				case Threading.SINGLE:
					SingleThreadedLoadWeeks();
					break;
				default:
					SingleThreadedLoadWeeks();
					break;
			}
		}



		private void SingleThreadedLoadWeeks()
		{
			var storesSelected = storeList.SelectedItems;

			foreach (int i in storesSelected.Count)
			{
				Store currentStore = stores[storeCode];
				if (currentStore == null) { continue; }
				for (int j = 0; j < orders.Count; j++)
				{
					if (orders[j].Store.StoreCode == currentStore.StoreCode)
					{
						Date currentDate = orders[j].Date;
						if (!displayedDates.Contains(currentDate))
						{
							displayedDates.Add(currentDate);
						}
					}
				}
			}
		}

		private void ListOrders(Date startDate, Date endDate) {
			for (int i = 0; i < stores.Count(); i++) {
			}
		}

        private void UpdateWeeks_Click(object sender, RoutedEventArgs e)
        {
			UpdateWeeks();
        }
    }
}