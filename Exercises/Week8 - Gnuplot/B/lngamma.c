#include<math.h>
#include<stdio.h>
int main(){
for (double x=500; x<= 10000; x+=500){
	printf("%g %g\n", x, lgamma(x));}

return 0;
}
