using System;
using System.IO;
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
            AddProduct();
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
