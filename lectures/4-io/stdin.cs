using System;
using System.IO;
using static System.Console;
class main{
	static public int Main(){
	System.Console.WriteLine("this goes to stdout");
	System.Console.Out.WriteLine("this goes to stdout");
	System.Console.Error.WriteLine("this goes to stderr");
	System.IO.TextWriter stdout = System.Console.Out;
	stdout.WriteLine("another stdout");

	string line = System.Console.ReadLine();
	WriteLine($"line = {line}");	
	string[] words = line.Split();
	foreach(string word in words){
		WriteLine($"word={word}");
		double x = double.Parse(word);
		WriteLine($"x={x}");
	}

	/*
	string line = System.Console.In.ReadLine();
	var stdin = System.Console.In;
	string s = stdin.ReadLine();
	*/


return 0;
}
}
