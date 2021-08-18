 using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text;

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
            
            public string LoadDate { get; set; } = DateTime.Now.ToString("yyyy'-'MM'-'dd");

            public int Page { get; set; }

            public string Source { get; set; } = "https://www.zolo.ca";

            /// <summary>
            /// ///////////////
            /// </summary>
            /// <param name="args"></param>
            static void Main(string[] args)
            {
              // Determine total page of the website
                HtmlWeb web_ = new HtmlWeb();
                HtmlDocument doc_ = web_.Load("https://www.zolo.ca/toronto-real-estate");
                var xPath_ = @"//nav[@class='xs-hide md-flex']/a[@class='button button--mono button--large xs-mr1']"; 
                var page = doc_.DocumentNode.SelectNodes(xPath_);
                int totalPages = -1;
                for (int i = 0; i < page.Count; ++i)
                {
                    if (page[i] != null)
                    {
                        var temp = page[i].InnerText;
                        if (!temp.Equals(""))
                        {
                            int check = Int32.Parse(temp);
                            if (check > totalPages)
                            {
                                totalPages = check;
                            }
                        }
                    }
                }
               

                string url = "https://www.zolo.ca/toronto-real-estate/page-";
               
                string dateString = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                string path = "C:/Users/nguye/OneDrive/Desktop/csvFiles/RealEstatesForSale_" + dateString + ".csv";
                
                var houses = new List<House>();

                for (int j = 1; j < totalPages + 1; j++)
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
                            address_ = address.InnerText.Trim();
                        else
                            address_ = "";

                        if (price != null)
                            price_ = price.InnerText.TrimStart(myChar).Replace(",","").Trim();
                        else
                            price_ = "";

                        if (title != null)
                            title_ = title.InnerText.Trim();
                        else
                            title_ = "";

                        if (bed != null && !bed.InnerText.Contains("Register or Sign in"))
                        {
                            if ((bed.InnerText.Contains("–") || bed.InnerText.Contains("&ndash;")) && bed.InnerText.Contains("bed"))
                                bed_ = "";
                            else if (bed.InnerText.Contains("bed"))
                            {
                                bed_ = bed.InnerText.TrimEnd(myChar1).Trim();
                            }
                            else if (bed.InnerText.Contains("bath"))
                            {
                                bath_ = bed.InnerText.TrimEnd(myChar2).Trim();
                                bed_ = "";
                            }
                            else if (bed.InnerText.Contains("sqft"))
                            {
                                sqft_ = bed.InnerText.TrimEnd(myChar3).Trim();
                                bed_ = "";
                                bath_ = "";
                            }
                            else
                                bed_ = bed.InnerText.Trim();
                        }
                        else
                            bed_ = "";

                        if (bath != null && !bath.InnerText.Contains("Register or Sign in"))
                        {
                            if ((bath.InnerText.Contains("–") || bath.InnerText.Contains("&ndash;")) && bath.InnerText.Contains("bath"))
                                bath_ = "";
                            else if (bath.InnerText.Contains("bed"))
                            {
                                bed_ = bath.InnerText.TrimEnd(myChar1).Trim();
                            }
                            else if (bath.InnerText.Contains("bath"))
                            {
                                bath_ = bath.InnerText.TrimEnd(myChar2).Trim();
                            }
                            else if (bath.InnerText.Contains("sqft"))
                            {
                                sqft_ = bath.InnerText.TrimEnd(myChar3).Trim();
                                bath_ = "";
                            }
                            else
                                bath_ = bath.InnerText.Trim();
                        }
                        else
                            bath_ = "";

                        if (sqft != null && !sqft.InnerText.Contains("Register or Sign in"))
                        {
                            if ((sqft.InnerText.Contains("–") || sqft.InnerText.Contains("&ndash;")) && sqft.InnerText.Contains("sqft"))
                                sqft_ = "Null";
                            else if (sqft.InnerText.Contains("bed"))
                                bed_ = sqft.InnerText.TrimEnd(myChar1).Trim();
                            else if (sqft.InnerText.Contains("bath"))
                                bath_ = sqft.InnerText.TrimEnd(myChar2).Trim();
                            else if (sqft.InnerText.Contains("sqft"))
                                sqft_ = sqft.InnerText.TrimEnd(myChar3).Trim();
                            else 
                                sqft_ = sqft.InnerText.Trim();
                        }
                        else
                            sqft_ = "";

                        if (yearsOld != null)
                        {
                            yearsOld_ = yearsOld.InnerText.TrimEnd(myChar4).Trim();
                        }
                        else
                            yearsOld_ = "";

                        if (neighbourhood != null)
                            neighbourhood_ = neighbourhood.InnerText.TrimStart(myChar5).Trim();
                        else
                            neighbourhood_ = "";

                        if (listedBy != null)
                            listedBy_ = listedBy.InnerText.TrimStart(myChar5).Trim();
                        else
                            listedBy_ = "";

                        if (price_.Equals(""))
                            note_ = "Register or sign in to view";
                        else
                            note_ = "";
                            
                        houses.Add(new House { Price = price_, Address = address_, ListedBy = listedBy_, Bed = bed_, Bath = bath_, Sqft = sqft_, Title = title_, YearsOld = yearsOld_, NeighbourHood = neighbourhood_, Note = note_, Page = j });
                    }

                }
               
                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = "|", Encoding = Encoding.UTF8 }))
                {
                   
                    csv.WriteRecords(houses);
                }
            }
        }
    }
}




