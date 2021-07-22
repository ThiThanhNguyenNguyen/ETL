using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.IO;
using System.Globalization;
using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AngleSharp.Html.Dom;
using System.Net.Http;
using System.Linq;

namespace GetHouseSaleInfo
{
    class Program
    {
        public class House
        {
            public string Price { get; set; }
            public string Address { get; set; }
            public string NeighbourHood { get; set; }
            public string Bed { get; set; }
            public string Bath { get; set; }
            public string Sqft { get; set; }
            public string Title { get; set; }
            public string YearsOld { get; set; }
            public string ListedBy { get; set; }
            public string Note { get; set; }
            public string Page { get; set; } 

            static void Main(string[] args)
            {
                const int totalPages = 10;
             
                string url = "https://www.zolo.ca/index.php?sarea=&s=";
                DateTime aDate = DateTime.Now;
                string dateString = aDate.ToString("MMddyyyy");
                string path = "C:/Users/nguye/OneDrive/Desktop/RealEstateForSale" + dateString + ".csv";
                Console.WriteLine(dateString);

                var houses = new List<House>();

                for (int j = 1; j < totalPages; j++)
                {
                    string url_ = url + j;

                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument doc = web.Load(url_);

                    var xPath = @"//div[@class='card-listing--details xs-p2 xs-text-4 fill-white flex xs-flex-column xs-flex-shrink-0 xs-relative']";
                    var listOfHouse = doc.DocumentNode.SelectNodes(xPath);
                    foreach (HtmlNode item in listOfHouse)
                    {
                        var address = item.SelectSingleNode(".//h3[@class='card-listing--location text-5 xs-inline']");
                        var price = item.SelectSingleNode(".//ul[@class='card-listing--values truncate list-unstyled xs-flex-order-1 xs-mb05']/li[@class='price xs-block xs-mb1 text-2 heavy']");
                        var title = item.SelectSingleNode(".//div[@class='fill-green text-white pill xs-text-6 xs-line-height-1 bold xs-inline-flex xs-flex-align-center xs-py05 xs-px1 xs-mr-auto']/svg/title");
                        var bed = item.SelectSingleNode(".//ul[@class='card-listing--values truncate list-unstyled xs-flex-order-1 xs-mb05']/li[@class='xs-inline xs-mr1'][1]");
                        var bath = item.SelectSingleNode(".//ul[@class='card-listing--values truncate list-unstyled xs-flex-order-1 xs-mb05']/li[@class='xs-inline xs-mr1'][2]");
                        var sqft = item.SelectSingleNode(".//ul[@class='card-listing--values truncate list-unstyled xs-flex-order-1 xs-mb05']/li[@class='xs-inline xs-mr1'][3]");
                        var yearsOld = item.SelectSingleNode(".//ul[@class='card-listing--values truncate list-unstyled xs-flex-order-1 xs-mb05']/li[@class='xs-inline']");
                        var neighbourhood = item.SelectSingleNode(".//span[@class='neighbourhood']");
                        var listedBy = item.SelectSingleNode(".//div[@class='card-listing--brokerage truncate']");

                        string address_ = "";
                        string price_ = "";
                        string title_ = "";
                        string bed_ = "";
                        string bath_ = "";
                        string sqft_ = "";
                        string yearsOld_ = "";
                        string neighbourhood_ = "";
                        string listedBy_ = "";
                        string note_ = "";
                        char[] myChar = { '$'};
                        char[] myChar1 = { 'b', 'e', 'd', ' ' };
                        char[] myChar2 = { 'b', 'a', 't', 'h', ' ' };
                        char[] myChar3 = { 's', 'q', 'f', 't', ' ' };
                        char[] myChar4 = { 'Y', 'e', 'a', 'r', 's', 'O', 'l', 'd', ' ' };
                        char[] myChar5 = { 'b', 'u', 'l', 'l', ';', '&' };

                        if (address != null)
                            address_ = address.InnerText;
                        else
                            address_ = "Null";

                        if (price != null)
                            price_ = price.InnerText.TrimStart(myChar);
                        else
                            price_ = "Null";

                        if (title != null)
                            title_ = title.InnerText;
                        else
                            title_ = "Null";

                        if (bed != null && !bed.InnerText.Contains("Register or Sign in"))
                        {
                            if ((bed.InnerText.Contains("–") || bed.InnerText.Contains("&ndash;")) && bed.InnerText.Contains("bed"))
                                bed_ = "Null";
                            else if (bed.InnerText.Contains("bed"))
                            {
                                bed_ = bed.InnerText.TrimEnd(myChar1);
                            }
                            else if (bed.InnerText.Contains("bath"))
                            {
                                bath_ = bed.InnerText.TrimEnd(myChar2);
                                bed_ = "Null";
                            }
                            else if (bed.InnerText.Contains("sqft"))
                            {
                                sqft_ = bed.InnerText.TrimEnd(myChar3);
                                bed_ = "Null";
                                bath_ = "Null";
                            }
                            else
                                bed_ = bed.InnerText;
                        }
                        else
                            bed_ = "Null";

                        if (bath != null && !bath.InnerText.Contains("Register or Sign in"))
                        {
                            if ((bath.InnerText.Contains("–") || bath.InnerText.Contains("&ndash;")) && bath.InnerText.Contains("bath"))
                                bath_ = "Null";
                            else if (bath.InnerText.Contains("bed"))
                            {
                                bed_ = bath.InnerText.TrimEnd(myChar1);
                            }
                            else if (bath.InnerText.Contains("bath"))
                            {
                                bath_ = bath.InnerText.TrimEnd(myChar2);
                            }
                            else if (bath.InnerText.Contains("sqft"))
                            {
                                sqft_ = bath.InnerText.TrimEnd(myChar3);
                                bath_ = "Null";
                            }
                            else
                                bath_ = bath.InnerText;
                        }
                        else
                            bath_ = "Null";

                        if (sqft != null && !sqft.InnerText.Contains("Register or Sign in"))
                        {
                            if ((sqft.InnerText.Contains("–") || sqft.InnerText.Contains("&ndash;")) && sqft.InnerText.Contains("sqft"))
                                sqft_ = "Null";
                            else if (sqft.InnerText.Contains("bed"))
                                bed_ = sqft.InnerText.TrimEnd(myChar1);
                            else if (sqft.InnerText.Contains("bath"))
                                bath_ = sqft.InnerText.TrimEnd(myChar2);
                            else if (sqft.InnerText.Contains("sqft"))
                                sqft_ = sqft.InnerText.TrimEnd(myChar3);
                            else 
                                sqft_ = sqft.InnerText;
                        }
                        else
                            sqft_ = "Null";

                        if (yearsOld != null)
                        {
                            yearsOld_ = yearsOld.InnerText.TrimEnd(myChar4);
                        }
                        else
                            yearsOld_ = "Null";

                        if (neighbourhood != null)
                            neighbourhood_ = neighbourhood.InnerText.TrimStart(myChar5);
                        else
                            neighbourhood_ = "Null";

                        if (listedBy != null)
                            listedBy_ = listedBy.InnerText.TrimStart(myChar5);
                        else
                            listedBy_ = "Null";

                        if (price_.Equals("Null"))
                            note_ = "Register or sign in to view";
                        else
                            note_ = "Null";
                            
                        houses.Add(new House { Price = price_, Address = address_, ListedBy = listedBy_, Bed = bed_, Bath = bath_, Sqft = sqft_, Title = title_, YearsOld = yearsOld_, NeighbourHood = neighbourhood_, Note = note_, Page = "Page" + j });
                    }

                }

                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(houses);
                }
            }
        }
    }
}




