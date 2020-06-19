
all : A.svg

A.svg : A.gpi out.txt
	gnuplot $<

out.txt : A.exe
	mono $< > $@

A.exe : A.cs matrix.dll 
	mcs $< -reference:$(word 2, $^)

matrix.dll : ../vector.cs ../matrix.cs JacDiag.cs
	mcs $^ -target:library -out:$@
