using System;
using static System.Console;
using static System.Math;

class main{


static complex G(complex x){
	//single precision gamma function (Gergo Nemes, from Wikipedia)
	//now as complex. The recursive steps now depending on the complex norm of the argument
	if(cmath.abs(x)<0)return PI/cmath.sin(PI*x)/G(1-x);
	if(cmath.abs(x)<9)return G(x+1)/x;
	complex loggamma=x*cmath.log(x+1/(12*x-1/x/10))-x+cmath.log(2*PI/x)/2;
	return cmath.exp(loggamma);
} // G


public static void Main(){
	// Shifting x values by 1/10 of dx to avoid evaluating the poles
	double dy = 10.0/100;
	double dx = 10.0/200;
	for(double y=-5;y<=5;y+=dy) {
		for(double x=-5+dx/2;x<=5;x+=dx) {
			complex z = new complex(x,y);
			WriteLine($"{z.Re} {z.Im} {cmath.abs(G(z))}");
		}
	}
} // function Main

} // class main
