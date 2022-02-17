// expanding on vec class from lecture

using static System.Console;
using static System.Math;

public class vec{
	public double x,y,z;
	
	//constructors:
	public vec(double x,double y,double z){
		this.x=x;this.y=y;this.z=z;
		}	
		
	public vec(){this.x=this.y=this.z=0;}

	//print method: u.print("u=");
	public void print(string s){
		Write(s);WriteLine($"{this.x} {this.y} {this.z}");
		}
		
	public override string ToString(){
        return $"{this.x} {this.y} {this.z}";
    	}

	public void print(){this.print("");}

	public static void print(vec v){v.print("static method:");}

	public static vec operator*(vec u,double c){
		return new vec(c*u.x,c*u.y,c*u.z);
		}

	public static vec operator*(double c,vec u){
		return u*c;
		}

	public static vec operator+(vec u,vec v){
		return new vec(u.x+v.x,u.y+v.y,u.z+v.z);
		}
	public static vec operator+(vec u){return u;}
	
	public static vec operator-(vec u){return -1*u;}

	public static vec operator-(vec u,vec v){
		return new vec(u.x-v.x,u.y-v.y,u.z-v.z);
		}

	public static vec cross(vec u, vec v) {
		return new vec(u.y*v.z-u.z*v.y, u.z*v.x-u.x*v.z, u.x*v.y-u.y*v.x);
		}
		

static bool approx(double a,double b,double tau=1e-9,double eps=1e-9){
        if(Abs(a-b) < tau)return true;
        if(Abs(a-b)/(Abs(a)+Abs(b)) < eps)return true;
        return false;
        }

public bool approx(vec other){
        if(!approx(this.x,other.x))return false;
        if(!approx(this.y,other.y))return false;
        if(!approx(this.z,other.z))return false;
        return true;
        }

public static bool approx(vec u, vec v){ return u.approx(v); }

public double dot(vec other){return this.x*other.x+this.y*other.y+this.z*other.z;}
/* and/or */
public static double dot(vec v,vec w){return v.x*w.x+v.y*w.y+v.z*w.z;}

}
