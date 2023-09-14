using System.Data;

namespace MinimalAPIDapper.Data;

public class TarefaContext
{
    public delegate Task<IDbConnection> GetConnection();
}

