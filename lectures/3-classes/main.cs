using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
		int n=9;
		double[] a=new double[n];
		a[0]=7;
		WriteLine($"a[0]={a[0]}");
		for(int i=0;i<n;i++){a[i]=i;}
		for(int i=0;i<n;i++)Write($"a[{i}]={a[i]} ");
		WriteLine();

		int m=n;
		m=0;
		WriteLine($"m={m}, n=...{n}");

		double[] b=a;
		b[0]=999;
		WriteLine($"b[0]={b[0]}, a[0]=...{a[0]}");

		foreach(double ai in a)Write($"ai={ai} ");
		WriteLine();

		vec u=new vec(100,200,300);
		u.print("u=");
		vec v=new vec(1,2,3);
		v.print("v=");
		(u+v).print("u+v = ");
		(5*v).print("5*v=");
		var tmp=u*5;
		tmp.print("u*5 = ");
		var w=3*u-v;	
		w.print("w=");
		vec.print(w);
		WriteLine(vec.approx(w,3*u-v));
		WriteLine(w.approx(3*u-v));
		WriteLine(u.approx(v));
		WriteLine($"u.dot(v)={u.dot(v)}");
}

}
