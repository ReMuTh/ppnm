/*
 Rene Thalund QRGS class

 [i][j] addressing is column i row j
 [i,j] addressing is ROW i, column j (other way aroung)
 n is counting rows
 m is counting column
*/
public class QRGS{
	public matrix Q,R;
	public int n,m;

	public QRGS(matrix A){
		// Dimensions of A in shortform
		n = A.size1;
		m = A.size2;

		// copy A to Q
		Q = A.copy();
		// R should square same size as number of columns in A
		R = new matrix(m,m);
		
		for(int i=0;i<m;i++){
			// Q[i] pulls out the i'th column vector of Q 
			R[i,i] = Q[i].norm();
			// Normalize Q[i]
			Q[i] /= R[i,i];
			// make the remaining columns orthogonal
			for(int j=i+1;j<m;j++) {
				R[j][i] = Q[i] * Q[j];
				Q[j] -= Q[i]*R[i,j];
			}
		}

	} // Constructor

	public vector solve(vector b) {

		vector x = Q.T * b;
		
		// back-substitute using R
		for(int i=m-1;i>=0;i--) {
			double numerator = x[i];
			   for(int j=i+1;j<m;j++) numerator -= R[i,j]*x[j];
			x[i] = numerator / R[i,i];
		}
		
		return x;

	} // solve


	public matrix inverse() {
		// Return the inverse of the creator matrix A
		matrix Ainv = new matrix(n,m);
		matrix Id = new matrix(n,m);
		Id.setid();

		// Now harvest e unit vectors out of Id, and solve for them.
		// put the result as rows in Ainv (note says columns, but methinks it's rows)
		for(int i=0;i<m;i++) Ainv[i]=this.solve(Id[i]);
		return Ainv;

	}

} // QRGS