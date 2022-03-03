using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Store {
	public string StoreCode { get; set; }
	public string StoreLocation { get; set; }
}

public class Order {
	public Store Store { get; set; }
	public Date Date { get; set; }
	public string SupplierName { get; set; }
	public string SupplierType { get; set; }
	public double Cost { get; set; }
}

public class Date {
	public int Week { get; set; }
	public int Year { get; set; }
}

namespace ExampleLoadData {

	public class StoreLoader {
		private static string folderPath = "StoreData";
		private static string storeCodesFile = "StoreCodes.csv";
		private static string storeDataFolder = "StoreData";

		public static Date smallestDate = new Date();
		public static Date highestDate = new Date();

		public static void LoadData(ref Dictionary<string, Store> stores, ref HashSet<Date> dates, ref List<Order> orders) {
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			string storeCodesFilePath = Directory.GetCurrentDirectory() + @"\" + folderPath + @"\" + storeCodesFile;
			string[] storeCodesData = File.ReadAllLines(storeCodesFilePath);
			foreach (var storeData in storeCodesData) {
				string[] storeDataSplit = storeData.Split(',');
				Store store = new Store { StoreCode = storeDataSplit[0], StoreLocation = storeDataSplit[1] };
				if (!stores.ContainsKey(store.StoreCode))
					stores.Add(store.StoreCode, store);

				//storeDataSplit[0] = store code
				//storeDataSplit[1] = store location
			}

			string[] fileNames = Directory.GetFiles(folderPath + @"\" + storeDataFolder);
			foreach (var filePath in fileNames) {
				string fileNameExt = Path.GetFileName(filePath);
				string fileName = Path.GetFileNameWithoutExtension(filePath);

				string[] fileNameSplit = fileName.Split('_');
				Store store = stores[fileNameSplit[0]];
				Date date = new Date { Week = Convert.ToInt32(fileNameSplit[1]), Year = Convert.ToInt32(fileNameSplit[2]) };
				dates.Add(date);
				if (date.Year <= smallestDate.Year) {
					if (date.Week < smallestDate.Week) {
						smallestDate = date;
					}
				} else if (date.Year >= highestDate.Year) {
					if (date.Week > highestDate.Week) {
						highestDate = date;
					}
				}
				//fileNameSplit[0] = store code
				//fileNameSplit[1] = week number
				//fileNameSplit[2] = year

				string[] orderData = File.ReadAllLines(folderPath + @"\" + storeDataFolder + @"\" + fileNameExt);
				foreach (var orderInfo in orderData) {
					string[] orderSplit = orderInfo.Split(',');
					Order order = new Order {
						Store = store,
						Date = date,
						SupplierName = orderSplit[0],
						SupplierType = orderSplit[1],
						Cost = Convert.ToDouble(orderSplit[2])
					};
					orders.Add(order);
					//orderSplit[0] = supplier name
					//orderSplit[1] = supplier type
					//orderSplit[2] = cost
				}
			}

			stopWatch.Stop();
			Console.WriteLine("TimeToLoad: " + stopWatch.Elapsed.TotalSeconds);
		}
	}
}