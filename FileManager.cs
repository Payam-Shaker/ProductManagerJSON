using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace ProductManager
{
    public class FileManager
    {
        private string jsonPath;
        private Category[] categories;
        public string GetUserHomePath()
        {
            return
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.DoNotVerify);
        }

        public void Run()
        {
            var path = GetUserHomePath();
            var folderPath = Path.Combine(path, "ProductManager");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            jsonPath = Path.Combine(folderPath, "Products.json");
            if (!File.Exists(jsonPath))
            {
                File.Create(jsonPath);
            }
            //AddCatagory();v  
            //AddProduct();
            //DeleteProduct();
            //UpdateProduct();
            ReadAllProduct();
        }

        public void AddProduct()
        {
            Console.WriteLine("To which category wish you add the product? ");

            
            Console.WriteLine("1    - Women");
            Console.WriteLine("2    - Men");
            Console.WriteLine("3    - Barn"); 
            string userChoi = Console.ReadLine();
            Console.WriteLine("Enter Product ID: ");
            int prodID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Product Name: ");
            string prodName = Console.ReadLine();
            var newProduct = "{'ID': " + prodID + ", 'Name': '"+ prodName +"'}";

            try
            {
                var jsonfile = File.ReadAllText(jsonPath);
                var jsonobject = JObject.Parse(jsonfile);
                var chosenCatArr = jsonobject.GetValue(userChoi) as JArray;
                var toBeAddedProd = JObject.Parse(newProduct);
                chosenCatArr.Add(toBeAddedProd);

                jsonobject[userChoi] = chosenCatArr;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonobject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonPath, newJsonResult);


            }
            catch (Exception ex)
            {

                Console.WriteLine(" Add in to Json file Faild!", ex.StackTrace);

            }

        }

        public void DeleteProduct()
        {

            Console.WriteLine("To which category wish you delete the product? ");
            string userChoi = Console.ReadLine();

            try
            {
                var jsonfile = File.ReadAllText(jsonPath);
                var jsonobject = JObject.Parse(jsonfile);
                JArray chosenCatArr = (JArray)jsonobject[userChoi];
                Console.WriteLine("To delete Enter Product ID: ");
                int prodID = Convert.ToInt32(Console.ReadLine());
                if (prodID > 0 )
                {
                    var toBeDeletedProd = chosenCatArr.FirstOrDefault(p => p["ID"].Value<int>() == prodID);
                    chosenCatArr.Remove(toBeDeletedProd);
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonobject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonPath, newJsonResult);

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(" Delete operation has Faild!", ex.StackTrace);
            }
        }

        public void UpdateProduct()
        {
            Console.WriteLine("To which category wish you update the product? ");
            string userChoi = Console.ReadLine();
            try
            {
                var jsonfile = File.ReadAllText(jsonPath);
                var jsonobject = JObject.Parse(jsonfile);
                JArray chosenCatArr = (JArray)jsonobject[userChoi];
                Console.WriteLine("To update Enter Product ID: ");
                int prodID = Convert.ToInt32(Console.ReadLine());
                if (prodID > 0)
                {
                    Console.WriteLine("Enter new product name: ");
                    string newName = Console.ReadLine();
                    var toBeUpdateProd = chosenCatArr.Where(p => p["ID"].Value<int>() == prodID);
                    foreach (var product in toBeUpdateProd)
                    {
                        product["Name"] = !string.IsNullOrEmpty(newName) ? newName : string.Empty;
                    }
                    jsonobject[userChoi] = chosenCatArr;
                    string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonobject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonPath, newJsonResult);

                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        public void ReadAllProduct()
        {
            Console.WriteLine("Product(s) of which category wish you to print? ");


            Console.WriteLine("1    - Women");
            Console.WriteLine("2    - Men");
            Console.WriteLine("3    - Barn");
            string userChoi = Console.ReadLine();
            try
            {
                var jsonfile = File.ReadAllText(jsonPath);
                var jsonobject = JObject.Parse(jsonfile);
                JArray chosenCatArr = (JArray)jsonobject[userChoi];
                Console.WriteLine("All the product under the category {0}", userChoi);
                if (chosenCatArr != null)
                {
                    foreach (var item in chosenCatArr)
                    {
                        Console.WriteLine("-" + item["ID"] + "\t" + item["Name"].ToString());
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddCatagory()
        {
            //Console.WriteLine("How many catagory wish you add? ");
            //int catNum = Convert.ToInt32(Console.ReadLine());
            //string newCategory = string.Empty;
            //categories = new Category[catNum];
            //for (int i = 0; i < catNum; i++)
            //{
            //    Console.WriteLine("Enter Category number {0}", i+1);
            //    string catName = Console.ReadLine();
            //    categories[i] = new Category(catName);
            //    newCategory = "{'Category Name': " + categories[i].Title + "}";

            //}
            try
            {
               // var jsonFile = File.ReadAllText(jsonPath);
                //var jsonObject = JObject.Parse(newCategory);
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(jsonPath);
                list.Add(new Category("barn"));
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonPath, newJsonResult);
            }
            catch (Exception ex)
            {

                Console.WriteLine(" Add in to Json file Faild!", ex.Message.ToString());
            }

            //Console.WriteLine("You have added: ");
            //foreach (var lines in categories)
            //{
            //    Console.WriteLine(lines.Title);
            //}
        }
    }
}
