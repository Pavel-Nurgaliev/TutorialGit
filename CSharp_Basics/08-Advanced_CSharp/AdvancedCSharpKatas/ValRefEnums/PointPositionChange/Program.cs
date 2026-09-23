
var a = new Point(1, 1);//instance of struc Point
var b = a;//copy value a to b
b.X = 99;//set the X member of point the 99 value
var value = a.X;// a.X = 1 because struc is value type and a and b stores 2 different instances. 1 because of ctor parameters (1,1)
object o = a;//it is implicit boxing operation. o is a var of reference type. When we use boxing operation, we reserve the memory in heap to store the value and metadata about its type. Therefore, it is perfomance expensive operation. The cost of boxing operation is O(1). When we loop it we create the pressure on the perfomance, because the GC needs to distribute memory more often
//var equals = a == b; struct doesn't support it by default
var c = a.Moved(2, 3);   // c == (3,4), a still (1,1)