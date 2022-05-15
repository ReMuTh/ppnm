using System;
using static System.Double;
using static System.Math;
static public class Integrator {

// The full version of my quad from part C, that accepts infinite limits, and returns errors is also used
// in part A and B, no need not to.


// Signature will return a tuple: (integration result,estimated error)
public static (double,double) quad(Func<double,double> fi, double ai, double bi, double del=0.001, double eps=0.001, double f2=NaN, double f3=NaN) {
    double a,b;
    Func<double,double> f;

    //Three cases of infinate limit variable substitutions
    if(Double.IsNegativeInfinity(ai)) {

        // both a and b are inf:
        if(Double.IsPositiveInfinity(bi)) {
            f = t => (fi((1-t)/t) + fi(-(1-t)/t)) * 1/(t*t);
        }
        // only a is inf
        else {
            f = t => fi(bi-(1-t)/t) * 1/(t*t);
        }

        // new limits
        a = 0; b = 1;

    }
    // only b is inf
    else if(Double.IsPositiveInfinity(bi)) {
        f = t => fi(ai+(1-t)/t)/(t*t);
        a = 0; b = 1;
    }

    else {
        f = fi;
        a = ai; b = bi;
    }
    
    
    double h=b-a;

    if(IsNaN(f2)){

        f2=f(a+2*h/6);
        f3=f(a+4*h/6);
    
    } // first call, no points to reuse

    double f1=f(a+h/6), f4=f(a+5*h/6);

    double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
    double q = (  f1+f2+f3+  f4)/4*(b-a); // lower order rule

    double err = Abs(Q-q);

    // If error is acceptable return result
    if (err <= Max(del,eps*Abs(Q)))  return (Q,err);
    

    // Otherwise call recursively pick up error estimates from each segment and calculate error propagation

    else {

        (double quad1,double err1) = quad(f, a, (a+b)/2, del/Sqrt(2), eps, f1, f2);
        (double quad2,double err2) = quad(f, (a+b)/2, b, del/Sqrt(2), eps, f3, f4);

        return (quad1+quad2,Sqrt(err1*err1+err2*err2));
    }

} // quad

// Quadrature with Clenshaw-Curtis variable transformation

public static (double,double) cc_quad(Func<double,double> f, double a, double b, double del=0.001, double eps=0.001, double f2=NaN, double f3=NaN) {
    // Reformulating f with lambda function and passing it on to standard quad
    // with limits a=0, b=PI, same accuracy goals, yiharr.
 
    if (a == -1 && b == 1) {

        return quad(theta=>f(Cos(theta))*Sin(theta),0,PI,del,eps);

    }

    else {

        return quad(theta=>f((a+b)/2+(b-a)/2*Cos(theta))*Sin(theta)*(b-a)/2,0,PI,del,eps);

    }

} //cc_quad

public static void test_quad(Func<double,double> f,double limit_a, double limit_b,
    double reference,string text,string method = "cc_quad",
    double d=1e-6,double e=1e-6,int python_compare=0) {
    int i=0;

    WL($"\nIntegrating {text} from {limit_a} to {limit_b} using {method} with accuracy delta={d} epsilon={e}");

    // Wrapping f in f_counter that will tally evaluations by i++ increments

    Func<double,double> f_counter = x => {i++; return f(x);};
    double res = 0, err = 0;
    if(method == "quad") {(res, err) = quad(f_counter, limit_a, limit_b, d, e);}
    if(method == "cc_quad") {(res, err) = cc_quad(f_counter, limit_a, limit_b, d, e);}

    WL($"Result is {res}, estimated error is {err:E1}");
    WL($"Reference value is {reference}");

    if (approx(res, reference, d, e)) WL("PASSED");
    else WL("FAILED");

    WL($"C# {method} routine used {i} integrant evaluations.");

    if(python_compare > 0) {
        WL($"Python used {python_compare} evaluations");

    }
}

static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){

    if(Abs(a-b)<acc) return true;
    if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;

return false;
}

static public void WL(string s="") {
    System.Console.WriteLine(s);
} // lazy write



} //integrator


