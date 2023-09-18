
ProgramStartupExtensions.CreateSerilogLogger("Conference");
var builder = WebApplication.CreateBuilder(args);
await builder.RunWebHost<Startup>();

