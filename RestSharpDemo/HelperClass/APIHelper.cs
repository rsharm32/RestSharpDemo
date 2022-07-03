using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo.HelperClass
{
    public class APIHelper<T>
    {
        public RestClient restClient;
        public RestRequest restRequest;

        //we are hard coding this now, but it will move to app.config file.
        // as the base URL changes for different environment but the end point(API) will remain same.
        
        public string baseUrl = "https://reqres.in/";

        public RestClient SetUrl(string endpoint)
        {
            var url = Path.Combine(baseUrl + endpoint);
            var restClient = new RestClient(url);
            return restClient;
        }
        public RestRequest CreateGetRequest()
        {
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }


        //Payload means body also
        public RestRequest CreatePostRequest(string Payload)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", Payload, ParameterType.RequestBody);
            return restRequest;
        }

        public RestRequest CreatePutRequest(string Payload)
        {
            var restRequest = new RestRequest(Method.PUT);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", Payload, ParameterType.RequestBody);
            return restRequest;
        }

       

        public RestRequest CreateDeleteRequest()
        {
            var restRequest = new RestRequest(Method.DELETE);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }


        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }
        //the above response will return a string and we need to handle this so we need to deserialise it.

        public DTO GetContent<DTO>(RestResponse response)
        {
            var content = response.Content;
            DTO dtoObject = JsonConvert.DeserializeObject<DTO>(content);

            return dtoObject;
        }





    }
}



