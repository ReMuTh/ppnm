public class genlist<T>{
        public T[] data;
        public int len => data.Length;
        public genlist(){ data = new T[0]; }
        public void push(T item){
                T[] newdata = new T[len+1];
                for(int i=0;i<len;i++)newdata[i]=data[i];
                newdata[len]=item;
                data=newdata; // garbage collect disposes of old data pointer
        }
}
