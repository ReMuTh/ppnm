using System;
using static System.Console;
public class main{
public static void Main(){

        WriteLine("Creating genlist of integers");
		
        var intlist = new genlist<int>();
        
        WriteLine("\nPushing 8 integers (multiples of 7) to the list:");
        
        for(int i=1;i<9;i++)intlist.push(i*7);
		
		intlist.print();
		intlist.status();
		
	    WriteLine("\nPushing one more element -42 to the list should double the capacity:");
	
		intlist.push(-42);

		intlist.print();
		intlist.status();
		
		
		WriteLine("\nPushing one more element 113, no change in capacity:");
	
		intlist.push(113);

		intlist.print();
		intlist.status();

	    WriteLine("\nRemoving third element, no change in capacity:");
		intlist.remove(2);
		intlist.print();
		intlist.status();


	    WriteLine("\nRemoving third element again, capacity is halfed:");
		intlist.remove(2);
		intlist.print();
		intlist.status();
		
	    WriteLine("\nRemoving first element, no change in capacity:");
		intlist.remove(0);
		intlist.print();
		intlist.status();
		
	    WriteLine("\nRemoving element -1 changes nothing");
		intlist.remove(-1);
		intlist.print();
		intlist.status();
		
	    WriteLine("\nRemoving element 10 (larger than size-1) changes nothing");
		intlist.remove(10);
		intlist.print();
		intlist.status();
		
		
	
        }//main method
}//main class
