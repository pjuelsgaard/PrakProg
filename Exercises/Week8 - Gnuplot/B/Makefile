all: LnGamma.svg 

LnGamma.svg: lngamma.gpi out.txt out.test.txt
	gnuplot $<

out.txt : main.exe
	mono $< > $@ 

out.test.txt: lngamma.c
	tcc -run $< > $@

main.exe : main.cs math.dll
	mcs $< -out:$@ -reference:$(word 2, $^)



math.dll: ../gamma.cs ~/matlib/complex.cs ~/matlib/cmath.cs
	mcs -target:library -out:$@ $^


.PHONEY:clean
clean: 
	rm -f *.dll *.exe out* *.svg

