using System;
using static System.Math;


public static class Minimization{

    private static int steps = 0;

    public static vector qnewton(Func<vector,double> f, vector start, double eps=1e-12){
        double alpha = 1e-2;

        steps = 0;

        int n = start.size;

        vector x = start.copy();
        vector f_grad_x = numgradient(f, x);
        matrix B = matrix.id(n);

        double lambda,fxs;
        vector s;

        while(f_grad_x.norm() > eps) {

            vector del_x = -B * f_grad_x; // eq. 6

            lambda = 1;
            do {
                s = del_x*lambda;                 // eq 8.

                fxs = f(x + s);
                // If lambda reaches minimum reset inverse Hessian to identidy matrix
                if(lambda < 1.0 / 32){
                    B.setid();
                    break;
                }
                lambda /= 2;
            } while (fxs >= f(x) + alpha * f_grad_x.dot(s)); // eq. 9 (Armijo condition)
            

            // SR1 update
            vector f_grad_xs = numgradient(f, x+s);

            // eq. 12
            vector y = f_grad_xs - f_grad_x;
            vector u = s - B*y;

            // eqn. 18 - SR1 update
            double uTy = u.dot(y);
            if(Abs(uTy) > eps){
                // using vector class update method
                B.update(u,u,1/uTy); // SR1 update
            }

            x += s;
            f_grad_x = f_grad_xs;

            // Increase iteration counter
            steps += 1;

        } 

        System.Console.WriteLine($"Iteration steps: {steps}");
        return x;
    }

    private static vector numgradient(Func<vector, double> f, vector x, double dx=1e-6){
        int n = x.size;
        vector gradient = new vector(n);
        double fx = f(x);
        matrix idx = matrix.id(n) * dx;

        for(int i = 0; i < n; i++){
            gradient[i] = (f(x+idx[i]) - fx)/dx;
        }
        return gradient;
    }

}


