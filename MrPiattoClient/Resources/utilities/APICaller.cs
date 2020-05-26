using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MrPiattoClient.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MrPiattoClient.Resources.utilities
{
    public class APICaller
    {
        //private static readonly string url = "http://200.23.157.109/api/";
        private static readonly string url = "http://192.168.100.207/api/";
        public static readonly string urlPhotos = "http://192.168.100.207/images/";
        public APICaller() { }

        public HttpClient BaseClient(HttpClient client)
        {
            client.BaseAddress = new Uri($"{url}");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

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
        public List<NewRestaurant> GetNewRestaurants()
        {
            List<NewRestaurant> restaurants;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Restaurants");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    restaurants = JsonConvert.DeserializeObject<List<NewRestaurant>>(json);
                }
                return restaurants;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool DenyRestaurant(int id)
        {
            bool deny;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Restaurants/Deny/{id}");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    deny = JsonConvert.DeserializeObject<bool>(json);
                }
                return deny;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string AcceptRestaurant(int id)
        {
            string password;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Restaurants/Accept/{id}");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    password = JsonConvert.DeserializeObject<string>(json);
                }
                return password;
            }
            catch (Exception)
            {
                return "Error. Favor de intentar nuevamente.";
            }
        }
        public bool DeleteComment(int id)
        {
            bool delete;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Comments/Delete/{id}");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    delete = JsonConvert.DeserializeObject<bool>(json);
                }
                return delete;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool NotBadComment(int id)
        {
            bool notBad;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Comments/NotBad/{id}");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    notBad = JsonConvert.DeserializeObject<bool>(json);
                }
                return notBad;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<WarningRestaurant> GetInactiveRestaurants()
        {
            List<WarningRestaurant> restaurants;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Restaurants/Inactive");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    restaurants = JsonConvert.DeserializeObject<List<WarningRestaurant>>(json);
                }
                return restaurants;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool DeleteRestaurant(int id)
        {
            bool responseMessage;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Restaurants/DeleteInactive/{id}");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    responseMessage = JsonConvert.DeserializeObject<bool>(json);
                }
                return responseMessage;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
         * CALLBACKS FOR USER
         */

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
        public string NewPassword(string mail)
        {
            string msg;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Users/Password/{mail}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                    msg = reader.ReadToEnd();
                return msg;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string MailSubscription(int idUser, int idRestaurant)
        {
            string msg;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}UserRestaurants/Mail/{idUser}/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                    msg = reader.ReadToEnd();
                return msg;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<int> GetBars(int idRestaurant)
        {
            List<int> bar;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Surveys/Bars/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var bars = reader.ReadToEnd();
                    bar = JsonConvert.DeserializeObject<List<int>>(bars);
                }
                return bar;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<float> GetRatings(int idRestaurant)
        {
            List<float> ratings;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Surveys/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var rating = reader.ReadToEnd();
                    ratings = JsonConvert.DeserializeObject<List<float>>(rating);
                }
                return ratings;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Survey> GetRatingAndComments(int idRestaurant)
        {
            List<Survey> commentsRating;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Surveys/Comments/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var rating = reader.ReadToEnd();
                    commentsRating = JsonConvert.DeserializeObject<List<Survey>>(rating);
                }
                return commentsRating;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Policies GetPolicies (int idRestaurant)
        {
            Policies policies;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Policies/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    policies = JsonConvert.DeserializeObject<Policies>(reader.ReadToEnd());
                }
                return policies;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<IdcategoriesNavigation> GetCategories()
        {
            List<IdcategoriesNavigation> categories;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Categories");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    categories = JsonConvert.DeserializeObject<List<IdcategoriesNavigation>>(reader.ReadToEnd());
                }
                foreach (var c in categories)
                    c.urlPhoto = $"{urlPhotos}categories/{c.idcategory}/categorie.jpg";
                return categories;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetFavoritesJSON(int idUser)
        {
            string favorites;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}UserRestaurants/{idUser}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    favorites = reader.ReadToEnd().ToString();
                }
                return favorites;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetMainRestaurantsJSON()
        {
            string main;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Restaurants/MainPage");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    main = reader.ReadToEnd().ToString();
                }
                return main;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task FireAndForgetQRAsync(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Reservations/QR/{id}");
            await request.GetResponseAsync();
        }
        public async Task<string> NewReservation(int idRestaurant, int idUser, DateTime date, int amount)
        {
            Reservation reservation = new Reservation();
            reservation.iduser = idUser;
            reservation.idtable = idRestaurant;
            reservation.date = date;
            reservation.amountOfPeople = amount;
            reservation.url = ".";

            HttpClient client = new HttpClient();
            client = BaseClient(client);

            var content = JsonConvert.SerializeObject(reservation);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(content));
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("Reservations", byteContent).Result;
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> JoinMrPiatto(string name, string address, string phone, string mail)
        {
            CompleteRestaurant restaurant = new CompleteRestaurant();
            restaurant.name = name;
            restaurant.address = address;
            restaurant.phone = phone;
            restaurant.mail = mail;
            restaurant.confirmation = false;

            var locations = await Geocoding.GetLocationsAsync(address);
            var loc = locations?.FirstOrDefault();
            if (loc != null)
            {
                restaurant.@long = loc.Longitude;
                restaurant.lat = loc.Latitude;
            }

            HttpClient client = new HttpClient();
            client = BaseClient(client);

            var content = JsonConvert.SerializeObject(restaurant);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(content));
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("Restaurants", byteContent).Result;
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> PostSurvey(int idWaiter, List<float> score, string comment, int idRes)
        {
            PostComment newComment = new PostComment();
            newComment.idrestaurant = idRes;
            newComment.comment = comment;
            newComment.status = "Active";
            newComment.iduser = Preferences.Get("idUser", 0);
            newComment.date = DateTime.Now;

            HttpClient client = new HttpClient();
            client = BaseClient(client);

            var content = JsonConvert.SerializeObject(newComment);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(content));
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("Comments", byteContent).Result;
            newComment = JsonConvert.DeserializeObject<PostComment>(await response.Content.ReadAsStringAsync());

            Survey survey = new Survey();
            survey.idrestaurant = idRes;
            survey.iduser = newComment.iduser;
            survey.idwaiter = idWaiter; // Waiter score is storage at score[0]
            survey.foodRating = score[1];
            survey.comfortRating = score[2];
            survey.serviceRating = score[3];
            survey.generalScore = score[4];
            survey.idcomment = newComment.idcomment;
            survey.dateStatistics = DateTime.Now;

            await InsertWaiterScoreAsync(idWaiter, score[0]);

            var content2 = JsonConvert.SerializeObject(survey); ;
            var byteContent2 = new ByteArrayContent(Encoding.UTF8.GetBytes(content2));
            byteContent2.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var sResponse = client.PostAsync("Surveys", byteContent2).Result;
            return await sResponse.Content.ReadAsStringAsync();

        }
        private async Task InsertWaiterScoreAsync(int id, float rating)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Categories/WaitersStatistics/{id}/{rating}");
            await request.GetResponseAsync();
        }
        public async Task<int> CreateUserAsync(User user)
        {
            HttpClient client = new HttpClient();
            client = BaseClient(client);

            var content = JsonConvert.SerializeObject(user);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(content));
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("Users", byteContent).Result;
            
            return int.Parse(await response.Content.ReadAsStringAsync());
        }
        public string GetReservationsJSON(int idUser)
        {
            string reservation;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Reservations/{idUser}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    reservation = reader.ReadToEnd().ToString();
                }
                return reservation;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Strike> GetStrikes(int idUser)
        {
            List<Strike> strikes;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}UserStrikes/{idUser}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    strikes = JsonConvert.DeserializeObject<List<Strike>>(reader.ReadToEnd());
                }
                return strikes;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Waiter> GetWaiters(int idRestaurant)
        {
            List<Waiter> waiters;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Categories/Waiters/{idRestaurant}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    waiters = JsonConvert.DeserializeObject<List<Waiter>>(reader.ReadToEnd());
                }
                return waiters;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<CompleteRestaurant> GetVisitedRestaurants(int idUser)
        {
            List<CompleteRestaurant> visited;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}UserRestaurants/Visited/{idUser}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    visited = JsonConvert.DeserializeObject<List<CompleteRestaurant>>(reader.ReadToEnd());
                }
                return visited;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool ReportComment(int idComment)
        {
            bool ok;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Comments/Report/{idComment}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    ok = JsonConvert.DeserializeObject<bool>(reader.ReadToEnd());
                }
                return ok;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DisableMail(int idUser)
        {
            bool ok;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}UserRestaurants/DisableMail/{idUser}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    ok = JsonConvert.DeserializeObject<bool>(reader.ReadToEnd());
                }
                return ok;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string> AddToFavoriteAsync(int idRes)
        {
            UserRestaurant user = new UserRestaurant();
            user.idrestaurant = idRes;
            user.iduser = Preferences.Get("idUser", 0);
            user.favorite = true;

            HttpClient client = new HttpClient();
            client = BaseClient(client);

            var content = JsonConvert.SerializeObject(user);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(content));
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("UserRestaurants", byteContent).Result;
            return await response.Content.ReadAsStringAsync();
        }

        public int LogInUser(string mail, string password)
        {
            User user = new User();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Users/LogIn/{mail}/{password}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    user = JsonConvert.DeserializeObject<User>(reader.ReadToEnd());
                }
                if (user.UserType == "user")
                {
                    Preferences.Set("boolUser", true);
                    Preferences.Set("userName", user.FirstName);
                    Preferences.Set("idUser", user.Iduser);
                    return 1;
                }
                else
                    return 2;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}