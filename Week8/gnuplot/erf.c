#include<math.h>
#include<stdio.h>
int main(){
for (double x=-3; x<= 3; x+=1){
	printf("%g %g\n", x, erf(x));}

return 0;
}
