using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Pomelo
{
    public static class PomeloOptionsBuilderExtensions
    {


       public static DbContextOptionsBuilder ConfigureMySql(this DbContextOptionsBuilder builder, string connectionString, int retryCount)
        {
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b =>
            {
                b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                if (retryCount > 0)
                    b.EnableRetryOnFailure(retryCount);

                b.TranslateParameterizedCollectionsToConstants(); //<-- fixes querying by collection of primitives. see https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/1960#issuecomment-2541898357
            });
            return builder;
        }


    }
}