using System;
using static System.Console;
using static System.Math;
using static System.Random;

class main{
	static void Main(){
		WriteLine("Testing vector class.");
		
		WriteLine("Contructor with no argument returns zero vector");	

		vec z = new vec();
		z.print("z = ");
		
		WriteLine("\nDefine two vectors with random integer coordinates between -100, 100");	
		var rand = new Random();
		
		vec u=new vec(rand.Next(201)-100,rand.Next(201)-100,rand.Next(201)-100);
		vec v=new vec(rand.Next(201)-100,rand.Next(201)-100,rand.Next(201)-100);

		u.print("u = ");
		v.print("v = ");
		
		WriteLine("\nVector addition and scalar multiplication:");			
		(u+v).print("u+v = ");
		(z+v).print("z+v = ");
		(5*v).print("5*v = ");
		
		var tmp=u*5;
		tmp.print("u*5 = ");
		
		int scale = rand.Next(11);
		var w=scale*u-v;		
		w.print($"w = {scale}*u-v = ");
		
		WriteLine("\nVector dot product:");			
		WriteLine($"u • v = {vec.dot(u,v)}");

		WriteLine("\nVector cross product:");			
		WriteLine($"u × v = {vec.cross(u,v)}");
		
		WriteLine("[also tests ToString override, as the cross product result is inserted in a formatted string.]");			
		

		WriteLine("\n[Test vector compare function]");			
		
		WriteLine($"\nTest vector compare function. Comparing w to {scale}*u-v. Should return True True:");
		WriteLine(vec.approx(w,scale*u-v));
		WriteLine(w.approx(scale*u-v));
		
		WriteLine($"\nFind single and double precision epsilon in current system:");
		
		double e_d=1; while(1+e_d!=1){e_d/=2;} e_d*=2;
		float e_f=1F; while((float)(1F+e_f) != 1F){e_f/=2F;} e_f*=2F;	
	
		double e_c = 1e-7;

		WriteLine($"Double precision epsilon: {e_d}");
		WriteLine($"Single precision (float) epsilon: {e_f}");
		WriteLine($"Crude epsilon: {e_c}");

		WriteLine($"\nCompare u to u plus the three epsilons added in one coordinate");
		WriteLine(u.approx(u+(new vec(e_d,0,0))));
		WriteLine(u.approx(u+(new vec(0,e_f,0))));
		WriteLine(u.approx(u+(new vec(0,0,e_c))));
		
		WriteLine($"\nCompare v to v scaled by (1+epsilons)");
		WriteLine(u.approx(v*(1+e_d)));
		WriteLine(u.approx(v*(1+e_f)));
		WriteLine(u.approx(v*(1+e_c)));
		
}
}
