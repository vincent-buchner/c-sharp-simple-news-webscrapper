using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Xml.XPath;

namespace webscrapper;

class WebScrapper {

    public static void Main(String[] args) {
        
        // Set the url to webscrap from
        String url = "https://www.luther.edu/news";

        // Create an http client and send a get request to get the html
        HttpClient httpClient = new HttpClient();
        String html = httpClient.GetStringAsync(url).Result;

        // Parse the html in a HTMLDocument Object
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        // Select the nodes that match the XPath regex
        HtmlNodeCollection newsStories = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'news__item')]");
        
        // For each story/element, 
        foreach (HtmlNode story in newsStories) {

            // Get the title and date from new article
            String title = story.SelectSingleNode(".//div[@class='news__title']").InnerText;
            String date = story.SelectSingleNode(".//div[@class='news__meta']").InnerText;

            // FIND BETTER WAY: Get the second the paragraph tag for description
            HtmlNodeCollection allPTags = story.SelectNodes(".//p");
            String description = allPTags[1].InnerText;

            // Print info to console
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Date: {date}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine("");
        }
    }
}




