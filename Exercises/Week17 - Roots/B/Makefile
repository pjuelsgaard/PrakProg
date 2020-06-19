all: out.txt B.svg
	cat $<

B.svg : B.gpi out2.txt
	gnuplot $<

out.txt out2.txt: B.exe
	mono $< 1> out.txt 2>out2.txt

B.exe : B.cs matlib.dll
	mcs $< -reference:$(word 2,$^) -out:$@

matlib.dll : ../matrix.cs ../vector.cs ../root.cs ../rk.cs
	mcs -target:library -out:$@ $^

clean :
	rm -f *.dll *.txt
			    
