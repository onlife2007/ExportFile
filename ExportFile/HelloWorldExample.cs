using Neo4j.Driver;
using neo4j_driver;

public class HelloWorldExample : IDisposable
{
    private bool _disposed = false;
    private readonly IDriver _driver;

    ~HelloWorldExample() => Dispose(false);

    public HelloWorldExample(string uri, string user, string password)
    {
        _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    }

    public void PrintGreeting(string message)
    {
        using (var session = _driver.Session())
        {
            var greeting = session.WriteTransaction(tx =>
            {
                var result = tx.Run("CREATE (a:Greeting) " +
                                    "SET a.message = $message " +
                                    "RETURN a.message + ', from node ' + id(a)",
                    new { message });
                return result.Single()[0].As<string>();
            });
            Console.WriteLine(greeting);
        }
    }

    public void PrintGroupRelationship(string groupid)
    {
        using (var session = _driver.Session())
        {
            var greeting = session.WriteTransaction(tx =>
            {
                var result = tx.Run(@"MATCH p=(n)<-[r:GROUP_IN*]-(m)
                                    WHERE n.GroupId = $groupid
                                    UNWIND relationships(p) AS Edge
                                    RETURN DISTINCT endNode(Edge).GroupId AS Parent, startNode(Edge).GroupId AS GroupId",
                    new { groupid });
                return result.ToList();
            });

            foreach (var item in greeting)
            {
                Console.WriteLine($"Group: {item["GroupId"]} Parent: {item["Parent"]}");
            }
            //foreach (var item in greeting)
            //{
            //    Console.WriteLine($"Group: {item.GroupId} Parent: {item.Parent}");
            //}
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _driver?.Dispose();
        }

        _disposed = true;
    }
}
