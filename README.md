C# Bindings for QnAMaker 🤓
========================

Usage examples
-------------
### Creating a client
```cs
using(var client = new QnaMakerClient("<your subscription key>"))
{
    var result = await client.GenerateAnswer(new Guid("<your knowledge base id>"), "How do I delete my Facebook account?");
    Console.WriteLine(result.Answers[0].Answer);
}
```

Installation
-------------

[Nuget Package](https://www.nuget.org/packages/QnaMaker)
> PM > Install-Package QnaMaker

Links
-----

* [qnamaker.ai](https://qnamaker.ai)
* [Official API documentation](https://westus.dev.cognitive.microsoft.com/docs/services/58994a073d9e04097c7ba6fe)