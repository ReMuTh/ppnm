// RENE 2022-04-02 additions to course matrix class
using System;
using static System.Math;

public partial class matrix{

	public bool is_lower_triangular(double acc=1e-6, double eps=1e-6){
		// First check whether square
		if(this.size1!=this.size2)return false;

		for(int i=0;i<size1-1;i++)
			for(int j=i+1;j<size1;j++)
				if(!approx(this[i,j],0,acc,eps))
					return false;
		return true;
	}

	public bool is_upper_triangular(double acc=1e-6, double eps=1e-6){
		return this.T.is_lower_triangular(acc,eps);
	}

	public vector diagonal() {
		int n = Min(this.size1, this.size2);
		vector tr = new vector(n);

		for(int i=0; i<n; i++) tr[i] = this[i,i];

		return tr;
	}

	public vector diag() {return this.diagonal();}

}