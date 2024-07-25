using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using DesignPatterns.Structural.AdapterPattern.Exercice;

namespace DesignPatterns.Tests;

public class AdapterTest{
    public void TestSendRequest(){
        RequesterAdapter requesterAdapter = new RequesterAdapter();
        requesterAdapter.Send("https://www.google.com");
        //output: Status code: OK
    }
}
