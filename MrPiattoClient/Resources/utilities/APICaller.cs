using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Models;
using Newtonsoft.Json;

namespace MrPiattoClient.Resources.utilities
{
    public class APICaller
    {
        //private static readonly string url = "http://200.23.157.109/api/";
        private static readonly string url = "http://192.168.100.207/api/";
        public APICaller() { }
        
        /*
         * CALLBACKS FOR THE VERIFIER
         */
        public List<Comment> GetCommentsVerifier()
        {
            List<Comment> comments;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Comments");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    comments = JsonConvert.DeserializeObject<List<Comment>>(json);
                }
                return comments;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /*
         * CALLBACKS FOR USER
         */

        //RestaurantActivity
        public CompleteRestaurant GetRestaurantMainInfo(int idRestaurant)
        {
            CompleteRestaurant restaurant;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Restaurants/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    restaurant = JsonConvert.DeserializeObject<CompleteRestaurant>(json);
                }
                return restaurant;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetSchedule(int idRestaurant)
        {
            string schedule;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Schedules/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                    schedule = reader.ReadToEnd();

                return schedule;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}