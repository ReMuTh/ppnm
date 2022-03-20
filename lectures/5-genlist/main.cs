using System;
using static System.Console;
class main{
public static void Main(){
	genlist<int> list = new genlist<int>();
	list.push(0);
	list.push(1);
	list.push(2);
	list.push(3);
	for(int i=0;i<list.data.Length;i++)
		WriteLine($"item number {i} is {list.data[i]}");
}

}
