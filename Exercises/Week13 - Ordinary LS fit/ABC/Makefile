all: A.svg

A.svg: A.gpi out.txt
	gnuplot $<

DAT = "../data.txt"
out.txt : A.exe  matrix.dll
	mono $< $(DAT) 2> $@ 

A.exe : A.cs matrix.dll
	mcs $< -reference:matrix.dll

matrix.dll : ../matrix.cs ../vector.cs fit.cs
	mcs $^ -target:library -out:$@ 

clean:
	rm -f *.txt *.exe *.dll *.svg
