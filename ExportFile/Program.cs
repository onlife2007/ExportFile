// See https://aka.ms/new-console-template for more information

using ExportFile;

Console.WriteLine("Hello, World!");

// Export files
//ExportFunc.ExportFiles();

// Print examples
var example = new HelloWorldExample("neo4j+s://8f950ac0.databases.neo4j.io:7687", "neo4j", "D6tzEaZ0purV3QP4XKeAsVZ4gu8eHMl5boIHh5THTT8");
string groupid = "10";
example.PrintGroupRelationship(groupid);