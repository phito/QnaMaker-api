C# Bindings for QnAMaker
========================

Usage example
-------------
```cs
using(var client = new QnaMakerClient("<your subscription key>"))
{
    var result = await client.GenerateAnswer(new Guid("<your knowledge base id>"), "How do I delete my Facebook account?");
    Console.WriteLine(result.Answers[0].Answer);
}
```

More doc to come 🤓