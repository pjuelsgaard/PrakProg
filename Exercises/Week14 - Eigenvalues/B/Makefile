all : B.svg out2.txt
	cat out2.txt

B.svg : B.gpi out.txt
	gnuplot $<

out.txt out2.txt: B.exe
	mono $< 1> out.txt 2> out2.txt

B.exe : B.cs matrix.dll 
	mcs $< -reference:$(word 2, $^)

matrix.dll : ../vector.cs ../matrix.cs JacDiag.cs ../fit.cs
	mcs $^ -target:library -out:$@
