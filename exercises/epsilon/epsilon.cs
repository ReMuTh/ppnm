using static System.Console;
using static System.Math;

class mathdings{

	static void Main() {
	
		// Check highest int with brute force
	
		int i=1; while(i+1>i) {i++;}
		Write("Max int by while loop bruteforce = {0}\n",i);

		Write("int.MaxValue = {0}\n\n",int.MaxValue);
		
		i=1; while(i-1<i) {i--;}

		Write("Min int by while loop bruteforce = {0}\n",i);
		Write("int.MinValue = {0}\n\n",int.MinValue);

		double x=1; while(1+x!=1){x/=2;} x*=2;
		float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;	
		
		Write("Double precision epsilon = {0}\n",x);
		Write("System.Math.Pow(2,-52) = {0}\n\n",Pow(2,-52));
		
		Write("Float epsilon = {0}\n",y);
		Write("System.Math.Pow(2,-23) = {0}\n\n",Pow(2,-52));
		
		float sumA = 1;
		float tiny = y/2;
		i=1;while(i<100){sumA+=tiny;i++;}
		
		float sumB = 0;
		i=1;while(i<100){sumB+=tiny;i++;}{sumB += 1;}
		
		Write("sumA -1 = {0}\n",sumA-1);
		Write("sumB -1 = {0}\n\n",sumB-1);
	}
	

}