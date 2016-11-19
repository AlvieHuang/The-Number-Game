using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Observable<T>
{
    List<IObserver<T>> observers;

    //constructor
    public Observable()
    {
        observers = new List<IObserver<T>>();
    }

    public void Subscribe(IObserver<T> subscriber) { observers.Add(subscriber); }

    public void Unsubscribe(IObserver<T> subscriber) { observers.Remove(subscriber); }

    public void Post(T message)
    {
        foreach(IObserver<T> observer in observers)
        {
            observer.Notify(message);
        }
    }
}

public interface IObserver<T>
{
    void Notify(T message);
}

//interface to indicate that a monobehaviour has an observable on it
public interface IObservable<T>
{
    Observable<T> Observable();
    //  { return <*your observable here*>; }
}

//base message; child classes can be used to carry data
public class Message 
{
    public readonly string messageType;
    public Message(string type)
    {
        messageType = type;
    }
}
//I suggest that child classes of message have hard-coded names to make filtering and casting the incoming messages easier

//static class that uses strings to route messages. messages must be cast on the recieving end

public static class Observers
{
    static Dictionary<string, Observable<Message>> theObservables = new Dictionary<string, Observable<Message>>();

    public static void Subscribe(IObserver<Message> subscriber, params string[] messageTypes)
    {
        foreach (string messageType in messageTypes)
        {
            if (!theObservables.ContainsKey(messageType))
                theObservables[messageType] = new Observable<Message>();

            theObservables[messageType].Subscribe(subscriber);
        }
    }

    public static void Unsubscribe(IObserver<Message> subscriber, params string[] messageTypes)
    {
        foreach (string messageType in messageTypes)
            if (theObservables.ContainsKey(messageType))
                theObservables[messageType].Unsubscribe(subscriber);
    }

    public static void Post(Message message)
    {
        theObservables[message.messageType].Post(message);
    }
}