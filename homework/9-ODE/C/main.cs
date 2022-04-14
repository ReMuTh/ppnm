 /*
x1=−x2=0.97000436−0.24308753i,x3=0;
V⃗ =x ̇3=−2x ̇1=−2x ̇2=−0.93240737−0.86473146i T=12T=6.32591398, I(0)=2, m1=m2=m3=1

*/

using System;
using static System.Math;
using static rk45;

class main{


static void Main(){

WL(@"HOMEWORK 9-ODE PART C

Surprising solution to three-body problem
Coding up the dynamics function quite literal, which OK for just three bodies
For more than three it should be done more iterative.

Before jumping to the stable solution, let's see if we can
replicate the more typical chaotic behaviour.

Setting up with initial zero velocity and some positions…
Plot three_body_1.svg shows expected erratic trajectories ;-)
");

vector r0 = new vector("-22 -8 6 -13 8 12 0 0 0 0 0 0");
double acc=1e-6,eps=1e-6;

(var tlist,var rlist) = driver_ab_incr(three_body_problem,0,500,r0,0.1,acc,eps);

var outstream=new System.IO.StreamWriter("three_body_data_1.txt");

outstream.WriteLine($"t\tx1\ty1\tx2\ty2\tx3\ty3");

for (int i=0;i<tlist.size;i++) {
	vector r = rlist[i];
	outstream.WriteLine($"{tlist[i]}\t{r[0]}\t{r[1]}\t{r[2]}\t{r[3]}\t{r[4]}\t{r[5]}");	
}
outstream.Close();

WL(@"Now setting up Carles Simo's initial values Taken from the Chenciner and Montgomery paper.");
double T_per = 6.32591398;

r0 = new vector("0.97000436 -0.24308753 -0.97000436 0.24308753 0 0 0.46620369 0.43236573 0.46620369 0.43236573 -0.93240737 -0.86473146");

WL($"\nIf we've done newtonian gravitation dynamics right and set up the inital values correctly,");
WL($"then running the evolution for T={T_per} should return the state vector to same position");
WL($"That is positions and velocity vectors for all three particles");

(vector rT,int c) = driver_ab(three_body_problem,0,T_per,r0,0.001,acc,eps);

r0.print("        x1         y1         x2         y2         x3         y3       vx1         vy1        vx2        vy2        vx3        vy3\n");
rT.print();
test(vector.approx(rT,r0,acc*10,eps*10));

WL($"Running the evolution for T/3={T_per/3} should precisely rotate-permute the particles to take each others place");
WL($"keeping the data points and plotting them (three_body_2.svg) we see that is indeed the case.");

(tlist,rlist) = driver_ab_incr(three_body_problem,0,T_per/3,r0,0.001,1e-6,1e-6);


outstream=new System.IO.StreamWriter("three_body_data_2.txt");

outstream.WriteLine($"t\tx1\ty1\tx2\ty2\tx3\ty3");

for (int i=0;i<tlist.size;i++) {
	vector r = rlist[i];
	outstream.WriteLine($"{tlist[i]}\t{r[0]}\t{r[1]}\t{r[2]}\t{r[3]}\t{r[4]}\t{r[5]}");	
}
outstream.Close();


}

// To describe the three particle masses in 2D
// We need x,y,vx,vy for each of them, state vector with 12 components
static vector three_body_problem(double t,vector r) {


	// giving sensible names is redundant, but I'll go nuts if I don't…
	double x1 = r[0], y1 = r[1], vx1=r[6],  vy1=r[7];
	double x2 = r[2], y2 = r[3], vx2=r[8],  vy2=r[9];
	double x3 = r[4], y3 = r[5], vx3=r[10], vy3=r[11];
	

	// we need the distance cubed hence the 3/2 power
	double r12 = Pow((x2-x1)*(x2-x1)+(y2-y1)*(y2-y1),1.5);
	double r13 = Pow((x3-x1)*(x3-x1)+(y3-y1)*(y3-y1),1.5);
	double r23 = Pow((x3-x2)*(x3-x2)+(y3-y2)*(y3-y2),1.5);

	vector drdt = new vector(12);

	// Velocities are derivate of the position components, all six of them
	drdt[0]=vx1; drdt[1]=vy1;
	drdt[2]=vx2; drdt[3]=vy2;
	drdt[4]=vx3; drdt[5]=vy3;

	// The accerations (= the forces with unity masses) depend on the distances
	// We also take G to be unity.
	// By dividing the coordinate differences with distance cubed
	// We effectively get a 1/distance squared depence
	//  – multiplicated with unit vectors in the correct direction

	drdt[6]  = (x2-x1)/r12 + (x3-x1)/r13; drdt[7]  = (y2-y1)/r12 + (y3-y1)/r13; 
	drdt[8]  = (x1-x2)/r12 + (x3-x2)/r23; drdt[9]  = (y1-y2)/r12 + (y3-y2)/r23; 
	drdt[10] = (x1-x3)/r13 + (x2-x3)/r23; drdt[11] = (y1-y3)/r13 + (y2-y3)/r23; 

	return drdt;

}

static void test(bool condition) {
	if (condition) WL("PASSED");else WL("FAILED");
}


static void WL(string s="") {
	System.Console.WriteLine(s);

} // lazy write
} // class main