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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		public Dictionary<string, Store> stores;
		public HashSet<Date> dates;
		public List<Order> orders;

		int[] storeIndex;

		int selectedStores;
		int selectedDates;
		int selectedOrders;

		public MainWindow() {
			Dictionary<string, Store> stores = new Dictionary<string, Store>();
			HashSet<Date> dates = new HashSet<Date>();
			List<Order> orders = new List<Order>();

			storeIndex = new int[256];
			InitializeComponent();

			DataContext = this;
			Debug.WriteLine("test");
			ExampleLoadData.StoreLoader.LoadData(ref stores, ref dates, ref orders);
			storeList.ItemsSource = stores;

		}
	}
}
