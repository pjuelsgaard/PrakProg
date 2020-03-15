all: CGamma.svg 

CGamma.svg: cgamma.gpi out.txt # out.test.txt
	gnuplot $<

out.txt : main.exe
	mono $< > $@ 

#out.test.txt: cgamma.c
#	tcc -run $< > $@

main.exe : main.cs math.dll
	mcs $< -out:$@ -reference:$(word 2, $^)



cmath.dll: ~/matlib/complex.cs ~/matlib/cmath.cs
	mcs $^ -target:library -out:$@


.PHONEY:clean
clean: 
	rm -f *.dll *.exe out* *.svg

