using static System.Console;

public class genlist<T>{
        public T[] data;
        public int size=0,capacity=1;
        public genlist(){ data = new T[capacity]; }
        public void push(T item){
		if(size==capacity){
                	T[] newdata = new T[capacity*=2];
                	for(int i=0;i<size;i++)newdata[i]=data[i];
			data = newdata;
			}
                data[size]=item;
                size++;
        }
		public void remove(int i){
		
		if(i >=  0 & i < size) {
		
			// Descreasing size by one
			size--;
		
			// Moving all elements over remove item down by one
			for(int j=i;j<size;j++)data[j]=data[j+1];
		
			// If remove operation brings us over capacity threshold half the capacity
			if(size==capacity/2){
				T[] newdata = new T[capacity/=2];
					for(int j=0;j<size;j++)newdata[j]=data[j];
				data = newdata;
			}
		}
		}
		// implementing simple print method to spurt out content of list
		public void print(){
			for(int i=0;i<size;i++)Write($"{data[i]} ");
			Write("\n");
    	}
    	
    	public void status(){
    		WriteLine($"This genlist has {this.size} elements and capacity {this.capacity}");
    	}
    	
	}
