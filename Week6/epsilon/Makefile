CSFLAGS = -optimize+ -platform:anycpu
.PHONEY : all
all : out.txt
	cat out.txt

out.txt : epsilon.exe
	mono $< > out.txt

epsilon.exe : epsilon.cs cmath.dll BoolApprox.dll
	mcs $(CSFLAGS) -out:epsilon.exe -reference:cmath.dll -reference:BoolApprox.dll epsilon.cs

cmath.dll : ~/lib/complex.cs ~/lib/cmath.cs
	mcs $^ -target:library -out:$@
	cp ~/lib/cmath.dll .

BoolApprox.dll : BoolApprox.cs
	mcs $< -target:library -out:$@ -reference:cmath.dll


#.PHONEY: clean #If there is a risk that there is already a file called clean
clean: 
	rm -f out.txt epsilon.exe
