using System;

public abstract class Handler
{
    protected Handler nextHandler;

    public Handler SetNext(Handler handler)
    {
        nextHandler = handler;
        return handler;
    }

    public virtual void Handle(Request request)
    {
        if (nextHandler != null)
        {
            nextHandler.Handle(request);
        }
    }
}

public class ConcreteHandlerA : Handler
{
    public override void Handle(Request request)
    {
        if (request.EventType == "A")
        {
            Console.WriteLine($"Handler A обработал запрос: {request.Content}");
        }
        else
        {
            base.Handle(request);
        }
    }
}

public class ConcreteHandlerB : Handler
{
    public override void Handle(Request request)
    {
        if (request.EventType == "B")
        {
            Console.WriteLine($"Handler B обработал запрос: {request.Content}");
        }
        else
        {
            base.Handle(request);
        }
    }
}

public class ConcreteHandlerC : Handler
{
    public override void Handle(Request request)
    {
        if (request.EventType == "C")
        {
            Console.WriteLine($"Handler C обработал запрос: {request.Content}");
        }
        else
        {
            base.Handle(request);
        }
    }
}

public class Request
{
    public string EventType { get; }
    public string Content { get; }

    public Request(string eventType, string content)
    {
        EventType = eventType;
        Content = content;
    }
}

public class Program
{
    public static Handler CreateChain()
    {
        var handlerA = new ConcreteHandlerA();
        var handlerB = new ConcreteHandlerB();
        var handlerC = new ConcreteHandlerC();

        handlerA.SetNext(handlerB).SetNext(handlerC);

        return handlerA;
    }

    public static void Main(string[] args)
    {
        // Создаем цепочку обработчиков
        var chain = CreateChain();

        // Создаем запросы
        var requests = new[]
        {
            new Request("A", "Запрос для A"),
            new Request("B", "Запрос для B"),
            new Request("C", "Запрос для C"),
            new Request("D", "Запрос для D") // Этот запрос не будет обработан
        };

        // Отправляем запросы по цепочке
        foreach (var req in requests)
        {
            Console.WriteLine($"Обработка запроса: {req.EventType} - {req.Content}");
            chain.Handle(req);
            Console.WriteLine();
        }
    }
}