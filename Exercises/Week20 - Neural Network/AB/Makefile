all : A.svg B.svg

B.svg : B.gpi out.txt
	gnuplot $<

A.svg: A.gpi out.txt
	gnuplot $<

out.txt : AB.exe
	mono $< 2>$@

AB.exe : AB.cs matlib.dll
	mcs $< -reference:$(word 2, $^) -out:$@

matlib.dll : ../matrix.cs ../vector.cs ../ann.cs ../minimize.cs ../erf.cs
	mcs -target:library -out:$@ $^

clean :
	rm -f *.txt *.svg *.dll *.exe
