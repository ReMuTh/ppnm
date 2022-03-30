using System;
using static System.Console;
using static System.Math;

class main{

static double lngamma(double x){
///single precision gamma function (Gergo Nemes, from Wikipedia)
if(x<0)return Log(PI)-Log(Sin(PI*x))-lngamma(1-x);
if(x<9)return lngamma(x+1)-Log(x);
return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
}

static double gamma(double x){
///single precision gamma function (Gergo Nemes, from Wikipedia)
if(x<0)return PI/Sin(PI*x)/gamma(1-x);
if(x<9)return gamma(x+1)/x;
double loggamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
return Exp(loggamma);
}


public static void Main(string[] args){
	// Shifting x values by half of dx to avoid evaluating the poles
	double dx = 1.0/100;
	
	if(args[0]=="lngamma"){
		 for(double x=0+dx;x<=20;x+=dx) {
			 WriteLine($"{x} {lngamma(x)}");
		 }
	}
    if(args[0]=="gamma") {
	
		 for(double x=-5+dx/2;x<=5-dx/2;x+=dx) {
			 WriteLine($"{x} {gamma(x)}");
		 }
	
	}
}
}
