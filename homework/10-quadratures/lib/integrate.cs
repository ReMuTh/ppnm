using System;
using static System.Double;
using static System.Math;
public class integrator {
    public int evals = 0;

public double quad(Func<double,double> f, double a, double b, double del=0.001, double eps=0.001, double f2=NaN, double f3=NaN) {
    double h=b-a;
    if(IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); this.evals+=2; } // first call, no points to reuse
    double f1=f(a+h/6), f4=f(a+5*h/6);
    this.evals += 2;
    double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
    double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule
    double err = Abs(Q-q);
    if (err <= Max(del,eps*Abs(Q))) return Q;
    else return quad(f,a,(a+b)/2,del/Sqrt(2),eps,f1,f2)+
            quad(f,(a+b)/2,b,del/Sqrt(2),eps,f3,f4);
} // quad

// Quadrature with Clenshaw-Curtis variable transformation
public  double cc_quad(Func<double,double> f, double a, double b, double del=0.001, double eps=0.001, double f2=NaN, double f3=NaN) {
    // Reformulating f with lambda function and passing it on to normal quad
    // with limits a=0, b=PI, same accuracy goals yiharr.

    return quad(theta=>f((a+b)/2+(b-a)/2*Cos(theta))*Sin(theta)*(b-a)/2,0,PI,del,eps);

} //cc_quad



} //integrator
