using Newtonsoft.Json;
using RestSharp;
using RestSharpDemo.HelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo
{
    //RestSharpDemo is used to handle request and mapping the objects from the response.
    public class Demo<T>
    {
        //Firstly we need to create a response to handle the request and response of the GET request
        public ListOfUsersDTO GetUsers(string endpoint)
        {
            var user = new APIHelper<ListOfUsersDTO>();
            var url = user.SetUrl(endpoint);
            var request = user.CreateGetRequest();
            var response = user.GetResponse(url, request);
            ListOfUsersDTO content = user.GetContent<ListOfUsersDTO>((RestResponse)response);
            return content;

            ////to send request we need to use these lines of code
            //var restClient = new RestClient("https://reqres.in/");
            //var restRequest = new RestRequest("/api/users?page=2", Method.GET);
            //restRequest.AddHeader("Accept", "application/json");
            //restRequest.RequestFormat = DataFormat.Json;

            ////to fetch the response we need to use IRestResponse
            ////IRestResponse has been deprecated from RestSharp from v107

            //IRestResponse response = restClient.Execute(restRequest);
            //var content = response.Content;

            ////convert the json --deserialization

            //var users = JsonConvert.DeserializeObject<ListOfUsersDTO>(content);
            //return users;
        }

        public CreateUserRequestDTO CreateUser(string endpoint, dynamic payload)
        {
            //var actually checks the data at complilation time and dynamic checks the data type at run time
            var user = new APIHelper<CreateUserRequestDTO>();
            var url = user.SetUrl(endpoint);
            var request = user.CreatePostRequest(payload);
            var response = user.GetResponse(url, request);
            CreateUserRequestDTO content = user.GetContent<CreateUserRequestDTO>((RestSharp.RestResponse)response);

            return content;
        }
    }
}
