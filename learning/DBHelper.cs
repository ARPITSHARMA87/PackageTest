using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SQLite;

namespace learning
{
    public class DBHelper
    {
        private static DBHelper dBHelper = null;
        public static DBHelper GetInstance()
        {
            if (dBHelper == null)
            {
                dBHelper = new DBHelper();
            }
            return dBHelper;
        }

        private SQLiteAsyncConnection conn;
        private string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public string Status { get; set; } = string.Empty;

        public DBHelper()
        {
            //Create Database
            conn = new SQLiteAsyncConnection(System.IO.Path.Combine(folder, "Packages.db"));
            conn.CreateTableAsync<Package>().Wait();
        }

        public async Task<string> AddNewPackageAsync(Package pack)
        {
            try
            {
                //Validation
                if (string.IsNullOrEmpty(pack.Barcode))
                    throw new Exception("Please enter valid Barcode");
                if (string.IsNullOrEmpty(pack.Height))
                    throw new Exception("Please enter valid Height");
                if (string.IsNullOrEmpty(pack.Width))
                    throw new Exception("Please enter valid Width");
                if (string.IsNullOrEmpty(pack.Depth))
                    throw new Exception("Please enter valid Depth");

                await conn.InsertAsync(pack);
                Status = string.Format("Dimm ({0} x {1} x {2}) {3}\nAdded Successfully",
                                    pack.Width, pack.Height, pack.Depth, pack.Barcode);
                Debug.WriteLine(Status);

            }
            catch (Exception ex)
            {
                Status = string.Format("Failed to add package item.\nException Message: {0}", ex.Message);
                Debug.WriteLine(Status);
            }
            return Status;
        }

        /// <summary>
        /// Gets all package async.
        /// </summary>
        /// <returns>The all package async.</returns>
        public async Task<List<Package>> GetAllPackageAsync()
        {
            try
            {
                return await conn.Table<Package>().ToListAsync();
            }
            catch (Exception ex)
            {
                Status = string.Format("Failed to get package data.\nException Message: {0}", ex.Message);
                Debug.WriteLine(Status);
            }
            return new List<Package>();
        }
    }
}

