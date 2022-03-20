using System;
using System.IO;
using static System.Console;
class main{
	static public int Main(){
	var reader = new System.IO.StreamReader("input.txt");
	var writer = new System.IO.StreamWriter("outfile.txt");

	string line = reader.ReadLine();
	writer.WriteLine($"line = {line}");	
	string[] words = line.Split();
	foreach(string word in words){
		writer.WriteLine($"word={word}");
		double x = double.Parse(word);
		writer.WriteLine($"x={x}");
	}

reader.Close();
writer.Close();
return 0;
}
}
