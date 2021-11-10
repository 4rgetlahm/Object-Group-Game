using System;

public class BadSessionException : Exception{
    public BadSessionException(string message) : base(message){

    }
}