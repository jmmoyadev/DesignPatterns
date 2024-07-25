using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace DesignPatterns.Structural.AdapterPattern.Exercice;

//Adapter - is a structural design pattern
// that allows objects with incompatible interfaces to work together.


//Problem - We need to write a class for making requests to the 
// server using a pre-created interface (ISend),
//  however, we notice that in our program there already exists a class
//   that implements similar functionality (Requster),
//    but unfortunately it does not fit our interface.

interface ISend{
    void Send(string url);
}

class Requester{
    static HttpClient httpClient = new HttpClient();
    
    //similar method to the interface
    public async void SendRequest(string url){
        using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        using HttpResponseMessage responce = await httpClient.SendAsync(requestMessage);
        Console.WriteLine($"Status code: {responce.StatusCode}\n");
    }
}


//Solution - Due to the fact that when changing the Requester class
// we can accidentally break the logic of our application,
//  this means that we cannot change the Requester directly.
//   And here the Adapter pattern comes to our aid.

//An adapter is a simple layer with an interface on top of another class.
// In our case, we can create a new RequesterAdapter class,
//  which we inherit from the Request class
//   to interact with the logic already ready in it,
//    and also inherit the ISend interface to support the interface we need

class RequesterAdapter : Requester, ISend{
    public void Send(string url){
        //Call a method from a class with ready-made logic
        this.SendRequest(url);
    }
}