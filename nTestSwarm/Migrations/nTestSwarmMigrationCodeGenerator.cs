using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Utilities;

namespace nTestSwarm.Migrations
{
    public class nTestSwarmMigrationCodeGenerator : CSharpMigrationCodeGenerator
    {
        protected override void WriteClassStart(string @namespace, string className, IndentedTextWriter writer,
                                                string @base, bool designer = false)
        {
            var baseClassName = @base == typeof (IMigrationMetadata).Name ? @base : typeof (nTestSwarmDbMigration).FullName;

            base.WriteClassStart(@namespace, className, writer, baseClassName, designer);
        }
    }
}