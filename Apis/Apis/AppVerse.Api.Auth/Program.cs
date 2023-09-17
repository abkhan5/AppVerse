ProgramStartupExtensions.CreateSerilogLogger("Authentication");
var builder = WebApplication.CreateBuilder(args);
await builder.RunWebHost<Startup>();

