public class node<T>{ 
	public T item;
	public node<T> next=null;
	public node(T item){this.item=item;}
}

public class genlist<T>{
        public int size=0;
        public node<T> first=null,current=null;
        public void push(T item){
                size++;
                if(first==null){
                        first=new node<T>(item);
                        current=first;
                }
                else{
                        current.next = new node<T>(item);
                        current=current.next;
                }
        }
        public void start(){ current=first; }
        public void next(){ current=current.next; }
}